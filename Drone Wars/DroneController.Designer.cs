namespace Drone_Wars
{
    partial class DroneController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dronesLb = new System.Windows.Forms.ListBox();
            this.addDroneBtn = new System.Windows.Forms.Button();
            this.removeDroneBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.numberTb = new System.Windows.Forms.TextBox();
            this.flyBtn = new System.Windows.Forms.Button();
            this.landBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.thetaTb = new System.Windows.Forms.TextBox();
            this.moveBtn = new System.Windows.Forms.Button();
            this.fieldPnl = new BufferedPanel();
            this.SuspendLayout();
            // 
            // dronesLb
            // 
            this.dronesLb.FormattingEnabled = true;
            this.dronesLb.Location = new System.Drawing.Point(12, 32);
            this.dronesLb.Name = "dronesLb";
            this.dronesLb.Size = new System.Drawing.Size(184, 95);
            this.dronesLb.TabIndex = 0;
            // 
            // addDroneBtn
            // 
            this.addDroneBtn.Location = new System.Drawing.Point(12, 3);
            this.addDroneBtn.Name = "addDroneBtn";
            this.addDroneBtn.Size = new System.Drawing.Size(89, 23);
            this.addDroneBtn.TabIndex = 1;
            this.addDroneBtn.Text = "Add Drone";
            this.addDroneBtn.UseVisualStyleBackColor = true;
            this.addDroneBtn.Click += new System.EventHandler(this.addDroneBtn_Click);
            // 
            // removeDroneBtn
            // 
            this.removeDroneBtn.Location = new System.Drawing.Point(107, 3);
            this.removeDroneBtn.Name = "removeDroneBtn";
            this.removeDroneBtn.Size = new System.Drawing.Size(89, 23);
            this.removeDroneBtn.TabIndex = 2;
            this.removeDroneBtn.Text = "Remove Drone";
            this.removeDroneBtn.UseVisualStyleBackColor = true;
            this.removeDroneBtn.Click += new System.EventHandler(this.removeDroneBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(107, 191);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(89, 23);
            this.stopBtn.TabIndex = 9;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // numberTb
            // 
            this.numberTb.Location = new System.Drawing.Point(12, 193);
            this.numberTb.Name = "numberTb";
            this.numberTb.Size = new System.Drawing.Size(89, 20);
            this.numberTb.TabIndex = 10;
            this.numberTb.Text = "1";
            // 
            // flyBtn
            // 
            this.flyBtn.Location = new System.Drawing.Point(12, 133);
            this.flyBtn.Name = "flyBtn";
            this.flyBtn.Size = new System.Drawing.Size(89, 23);
            this.flyBtn.TabIndex = 11;
            this.flyBtn.Text = "Fly Drone";
            this.flyBtn.UseVisualStyleBackColor = true;
            this.flyBtn.Click += new System.EventHandler(this.flyBtn_Click);
            // 
            // landBtn
            // 
            this.landBtn.Location = new System.Drawing.Point(107, 133);
            this.landBtn.Name = "landBtn";
            this.landBtn.Size = new System.Drawing.Size(89, 23);
            this.landBtn.TabIndex = 12;
            this.landBtn.Text = "Land Drone";
            this.landBtn.UseVisualStyleBackColor = true;
            this.landBtn.Click += new System.EventHandler(this.landBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // thetaTb
            // 
            this.thetaTb.Location = new System.Drawing.Point(12, 164);
            this.thetaTb.Name = "thetaTb";
            this.thetaTb.Size = new System.Drawing.Size(89, 20);
            this.thetaTb.TabIndex = 17;
            this.thetaTb.Text = "1";
            // 
            // moveBtn
            // 
            this.moveBtn.Location = new System.Drawing.Point(107, 162);
            this.moveBtn.Name = "moveBtn";
            this.moveBtn.Size = new System.Drawing.Size(89, 23);
            this.moveBtn.TabIndex = 18;
            this.moveBtn.Text = "Move";
            this.moveBtn.UseVisualStyleBackColor = true;
            this.moveBtn.Click += new System.EventHandler(this.moveBtn_Click);
            // 
            // fieldPnl
            // 
            this.fieldPnl.BackColor = System.Drawing.Color.White;
            this.fieldPnl.Location = new System.Drawing.Point(202, 3);
            this.fieldPnl.Name = "fieldPnl";
            this.fieldPnl.Size = new System.Drawing.Size(212, 210);
            this.fieldPnl.TabIndex = 16;
            this.fieldPnl.Paint += new System.Windows.Forms.PaintEventHandler(this.fieldPnl_Paint);
            // 
            // DroneController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(422, 221);
            this.Controls.Add(this.moveBtn);
            this.Controls.Add(this.thetaTb);
            this.Controls.Add(this.fieldPnl);
            this.Controls.Add(this.landBtn);
            this.Controls.Add(this.flyBtn);
            this.Controls.Add(this.numberTb);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.removeDroneBtn);
            this.Controls.Add(this.addDroneBtn);
            this.Controls.Add(this.dronesLb);
            this.Name = "DroneController";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drone Wars";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DroneController_FormClosed);
            this.Load += new System.EventHandler(this.DroneController_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox dronesLb;
        private System.Windows.Forms.Button addDroneBtn;
        private System.Windows.Forms.Button removeDroneBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.TextBox numberTb;
        private System.Windows.Forms.Button flyBtn;
        private System.Windows.Forms.Button landBtn;
        private System.Windows.Forms.Timer timer1;
        private BufferedPanel fieldPnl;
        private System.Windows.Forms.TextBox thetaTb;
        private System.Windows.Forms.Button moveBtn;
    }
}

