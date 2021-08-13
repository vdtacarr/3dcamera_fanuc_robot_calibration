
namespace server
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.lblReceived = new System.Windows.Forms.Label();
            this.lblSended = new System.Windows.Forms.Label();
            this.txtClientIp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.txtConsole2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(253, 17);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(94, 29);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(247, 297);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(94, 29);
            this.btnCalibrate.TabIndex = 1;
            this.btnCalibrate.Text = "Calibrate";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            // 
            // lblReceived
            // 
            this.lblReceived.AutoSize = true;
            this.lblReceived.Location = new System.Drawing.Point(122, 105);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(219, 20);
            this.lblReceived.TabIndex = 2;
            this.lblReceived.Text = "-----------------------------------";
            // 
            // lblSended
            // 
            this.lblSended.AutoSize = true;
            this.lblSended.Location = new System.Drawing.Point(122, 146);
            this.lblSended.Name = "lblSended";
            this.lblSended.Size = new System.Drawing.Size(225, 20);
            this.lblSended.TabIndex = 4;
            this.lblSended.Text = "------------------------------------";
            // 
            // txtClientIp
            // 
            this.txtClientIp.Location = new System.Drawing.Point(95, 19);
            this.txtClientIp.Name = "txtClientIp";
            this.txtClientIp.Size = new System.Drawing.Size(133, 27);
            this.txtClientIp.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "robot IP:";
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(438, 272);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(194, 91);
            this.txtConsole.TabIndex = 7;
            // 
            // txtConsole2
            // 
            this.txtConsole2.Location = new System.Drawing.Point(438, 207);
            this.txtConsole2.Name = "txtConsole2";
            this.txtConsole2.Size = new System.Drawing.Size(194, 27);
            this.txtConsole2.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtConsole2);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtClientIp);
            this.Controls.Add(this.lblSended);
            this.Controls.Add(this.lblReceived);
            this.Controls.Add(this.btnCalibrate);
            this.Controls.Add(this.btnStartServer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSended;
        private System.Windows.Forms.TextBox txtClientIp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblReceived;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.TextBox txtConsole2;
    }
}

