using System;
using iRoVisionCore;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using iRoVisionCore.Communication;
using HalconDotNet;
using Zivid.NET.Calibration;
using Zivid.NET;
using System.IO;
using System.Data;
using System.Linq;

namespace server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Logger.Initialize();
        }

        SocketController sc = new SocketController();

        public void Form1_Load(object sender, EventArgs e)
        {
            btnStartServer.Tag = false;
            HPose pose = new HPose(0.1, 0.1, 0.1, 0.0, 0.0, 0.0, "Rp+T", "gba", "point");
            HTuple tuple = new HTuple();
            HTuple tuple2 = new HTuple(0, 0, 0, 1);

            int index = 0;

            sc.ConnectedClient += (client) =>
            {
                if (client == null)
                    return;

                if (!client.Info.Initialized)
                {
                    client.Connected += (info, state) =>
                    {
                        this.Invoke(new Action(() => SetColor(state)));
                    };

                    client.Received += (info, message) =>
                    {
                        this.Invoke(new Action(() => SetText(lblReceived, message.Message)));
                        for (int i = 0; i < message.Pose.Count; i++)
                        {
                            pose[i] = message.Pose[i];
                        }

                        HOperatorSet.PoseToHomMat3d(pose, out tuple);
                        tuple.Append(tuple2);
                        index++;
                        File.WriteAllText("C:/Users/vedatacar/OneDrive - Robsen Robotics/Desktop/calibration/14/calibration_robot/" + index.ToString() + ".yaml", string.Join(" ", tuple) + Environment.NewLine);

                        var respons = new SocketArgs("0", "0", ProcessStatus.OK, new List<string>(), new List<double>() { 0.0, 0.0, 0.0, 0.0, 0.0 }, ProcessType.MATCHING);


                        client.SendResult(respons);

                        // For sended Message
                        this.Invoke(new Action(() => SetText(lblSended, respons.Message)));
                    };

                    client.Info.Initialized = true;

                }
            };

        }

        private void SetColor(bool state)
        {
            txtClientIp.BackColor = state ? Color.Green : Color.White;
        }

        private void SetText(Label box, string text)
        {
            box.Text = text;
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (txtClientIp.Text == string.Empty)
                txtClientIp.Text = sc.GetClientByID(0).Info.IPAddress;

            if (!(bool)btn.Tag)
            {
                if (sc.GetClients().Count == 1)
                    sc.RegisterClient(new ClientInfo() { IPAddress = txtClientIp.Text, Name = "Robot" });

                sc.StartServer(null);
                btn.Tag = true;
                btn.Text = "Stop Server";
            }
            else
            {
                sc.StopServer();
                btn.Tag = false;
                btn.Text = "Start Server";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sc.SaveConfig();
            sc.StopServer();
            System.Windows.Forms.Application.Exit();
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            try
            {

                HTuple mat3D = new HTuple();
                HTuple pose = new HTuple();

                var parameters = new CameraIntrinsics.DistortionGroup();
                double[] matrixGroup = { parameters.K1, parameters.K3, parameters.K3, parameters.P1, parameters.P2, };
                
                //Console.WriteLine("Connecting to camera");
                //var camera = zivid.ConnectCamera();
                var zivid = new Zivid.NET.Application();
                var inputs = ReadInputs();

                var calibrationResult = Calibrator.CalibrateEyeToHand(inputs);
                txtConsole.Text = calibrationResult.ToString();

                if (calibrationResult)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if ((4 * i + j) == 3 || (4 * i + j) == 7 || (4 * i + j) == 11)
                            {
                                calibrationResult.Transform()[i, j] = (calibrationResult.Transform()[i, j] / 1000);
                            }
                            mat3D[(4 * i + j)] = calibrationResult.Transform()[i, j];


                        }
                    }

                    //File.AppendAllText("C:/Users/vedatacar/OneDrive - Robsen Robotics/Desktop/calibration/6/result.yaml", Hommat3D2.ToString() + Environment.NewLine);

                    HOperatorSet.HomMat3dToPose(mat3D, out pose);
                    File.WriteAllText("C:/Users/vedatacar/OneDrive - Robsen Robotics/Desktop/calibration/14/result.yaml", pose.ToString() + Environment.NewLine);
                    File.AppendAllText("C:/Users/vedatacar/OneDrive - Robsen Robotics/Desktop/calibration/14/result.yaml", calibrationResult.ToString());

                }
                else
                {
                    Environment.ExitCode = 1;
                }
            }
            catch (Exception ex)
            {
                Environment.ExitCode = 1;
                txtConsole.Text = ex.Message;
            }
        }

        public List<HandEyeInput> ReadInputs()
        {
            var input = new List<HandEyeInput>();
            Interaction.ExtendInputBuffer(2048);
            int[] files = { 5, 4, 3, 2, 1 };

            foreach (int index in files)
            {


                try
                {

                    var robotPose = Interaction.ReadRobotPose(index);


                    using (var frame = Interaction.ReadZDFFromFile(index))
                    {


                        var result = Detector.DetectFeaturePoints(frame.PointCloud);

                        if (result)
                        {

                            input.Add(new HandEyeInput(robotPose, result));
                            this.Invoke(new Action(() =>
                            {
                                txtConsole2.Text = index.ToString() + ".pose  " + "Başarılı bir şekilde tespit edildi. ";
                                txtConsole2.Refresh();
                            }));

                        }
                        else
                        {
                            txtConsole2.Text = index.ToString() + "bulunamadı.";
                            txtConsole2.Refresh();

                        }

                    }
                }

                catch (Exception ex)
                {
                    txtConsole.Text = ex.Message;
                    continue;
                }
            }

            return input;
        }

       
    }

    class Interaction
    {

        // Console.ReadLine only supports reading 256 characters, by default. This limit is modified
        // by calling ExtendInputBuffer with the maximum length of characters to be read.
        public static void ExtendInputBuffer(int size)
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(), Console.InputEncoding, false, size));
        }


        public static Zivid.NET.Calibration.Pose ReadRobotPose(int index)
        {

            var input = File.ReadAllText("C:/Users/vedatacar" +
                "/OneDrive - Robsen Robotics/Desktop/calibration/14/calibration_robot/" + index.ToString() + ".yaml");


            var elements = input.Split().Where(x => !string.IsNullOrEmpty(x.Trim())).Select(x => float.Parse(x)).ToArray();

            var robotPose = new Zivid.NET.Calibration.Pose(elements);
            return robotPose;
        }


        public static Zivid.NET.Frame ReadZDFFromFile(int index)
        {
            var dataFile =
     ("C:/Users/vedatacar" + "/OneDrive - Robsen Robotics/Desktop/calibration/14/calibration_zivid/" + index.ToString() + ".zdf");


            var frame = new Zivid.NET.Frame(dataFile);


            return frame;
        }
    }
}
