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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DroneController));
            this.dronesLb = new System.Windows.Forms.ListBox();
            this.addDroneBtn = new System.Windows.Forms.Button();
            this.removeDroneBtn = new System.Windows.Forms.Button();
            this.upBtn = new System.Windows.Forms.Button();
            this.downBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.numberTb = new System.Windows.Forms.TextBox();
            this.flyBtn = new System.Windows.Forms.Button();
            this.landBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ipTb = new System.Windows.Forms.TextBox();
            this.rightBtn = new System.Windows.Forms.Button();
            this.backwardsBtn = new System.Windows.Forms.Button();
            this.leftBtn = new System.Windows.Forms.Button();
            this.forwardBtn = new System.Windows.Forms.Button();
            this.fieldPnl = new BufferedPanel();
            this.SuspendLayout();
            // 
            // dronesLb
            // 
            this.dronesLb.FormattingEnabled = true;
            this.dronesLb.Location = new System.Drawing.Point(12, 61);
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
            // upBtn
            // 
            this.upBtn.Location = new System.Drawing.Point(107, 194);
            this.upBtn.Name = "upBtn";
            this.upBtn.Size = new System.Drawing.Size(23, 23);
            this.upBtn.TabIndex = 7;
            this.upBtn.Text = "U";
            this.upBtn.UseVisualStyleBackColor = true;
            this.upBtn.Click += new System.EventHandler(this.upBtn_Click);
            // 
            // downBtn
            // 
            this.downBtn.Location = new System.Drawing.Point(107, 223);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(23, 23);
            this.downBtn.TabIndex = 8;
            this.downBtn.Text = "D";
            this.downBtn.UseVisualStyleBackColor = true;
            this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(107, 251);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(89, 23);
            this.stopBtn.TabIndex = 9;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // numberTb
            // 
            this.numberTb.Location = new System.Drawing.Point(12, 252);
            this.numberTb.Name = "numberTb";
            this.numberTb.Size = new System.Drawing.Size(89, 20);
            this.numberTb.TabIndex = 10;
            this.numberTb.Text = "1";
            // 
            // flyBtn
            // 
            this.flyBtn.Location = new System.Drawing.Point(12, 162);
            this.flyBtn.Name = "flyBtn";
            this.flyBtn.Size = new System.Drawing.Size(89, 23);
            this.flyBtn.TabIndex = 11;
            this.flyBtn.Text = "Fly Drone";
            this.flyBtn.UseVisualStyleBackColor = true;
            this.flyBtn.Click += new System.EventHandler(this.flyBtn_Click);
            // 
            // landBtn
            // 
            this.landBtn.Location = new System.Drawing.Point(107, 162);
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
            // ipTb
            // 
            this.ipTb.Location = new System.Drawing.Point(12, 32);
            this.ipTb.Name = "ipTb";
            this.ipTb.Size = new System.Drawing.Size(184, 20);
            this.ipTb.TabIndex = 14;
            this.ipTb.Text = "192.168.1.";
            // 
            // rightBtn
            // 
            this.rightBtn.Image = ((System.Drawing.Image)(resources.GetObject("rightBtn.Image")));
            this.rightBtn.Location = new System.Drawing.Point(75, 223);
            this.rightBtn.Name = "rightBtn";
            this.rightBtn.Size = new System.Drawing.Size(23, 23);
            this.rightBtn.TabIndex = 6;
            this.rightBtn.UseVisualStyleBackColor = true;
            this.rightBtn.Click += new System.EventHandler(this.rightBtn_Click);
            // 
            // backwardsBtn
            // 
            this.backwardsBtn.Image = ((System.Drawing.Image)(resources.GetObject("backwardsBtn.Image")));
            this.backwardsBtn.Location = new System.Drawing.Point(46, 223);
            this.backwardsBtn.Name = "backwardsBtn";
            this.backwardsBtn.Size = new System.Drawing.Size(23, 23);
            this.backwardsBtn.TabIndex = 5;
            this.backwardsBtn.UseVisualStyleBackColor = true;
            this.backwardsBtn.Click += new System.EventHandler(this.backwardsBtn_Click);
            // 
            // leftBtn
            // 
            this.leftBtn.Image = ((System.Drawing.Image)(resources.GetObject("leftBtn.Image")));
            this.leftBtn.Location = new System.Drawing.Point(17, 223);
            this.leftBtn.Name = "leftBtn";
            this.leftBtn.Size = new System.Drawing.Size(23, 23);
            this.leftBtn.TabIndex = 4;
            this.leftBtn.UseVisualStyleBackColor = true;
            this.leftBtn.Click += new System.EventHandler(this.leftBtn_Click);
            // 
            // forwardBtn
            // 
            this.forwardBtn.BackColor = System.Drawing.SystemColors.Control;
            this.forwardBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.forwardBtn.Image = ((System.Drawing.Image)(resources.GetObject("forwardBtn.Image")));
            this.forwardBtn.Location = new System.Drawing.Point(46, 194);
            this.forwardBtn.Name = "forwardBtn";
            this.forwardBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.forwardBtn.Size = new System.Drawing.Size(23, 23);
            this.forwardBtn.TabIndex = 3;
            this.forwardBtn.UseVisualStyleBackColor = false;
            this.forwardBtn.Click += new System.EventHandler(this.forwardBtn_Click);
            // 
            // fieldPnl
            // 
            this.fieldPnl.BackColor = System.Drawing.Color.White;
            this.fieldPnl.Location = new System.Drawing.Point(202, 3);
            this.fieldPnl.Name = "fieldPnl";
            this.fieldPnl.Size = new System.Drawing.Size(254, 269);
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
            this.ClientSize = new System.Drawing.Size(462, 280);
            this.Controls.Add(this.fieldPnl);
            this.Controls.Add(this.ipTb);
            this.Controls.Add(this.landBtn);
            this.Controls.Add(this.flyBtn);
            this.Controls.Add(this.numberTb);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.downBtn);
            this.Controls.Add(this.upBtn);
            this.Controls.Add(this.rightBtn);
            this.Controls.Add(this.backwardsBtn);
            this.Controls.Add(this.leftBtn);
            this.Controls.Add(this.forwardBtn);
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
        private System.Windows.Forms.Button forwardBtn;
        private System.Windows.Forms.Button leftBtn;
        private System.Windows.Forms.Button backwardsBtn;
        private System.Windows.Forms.Button rightBtn;
        private System.Windows.Forms.Button upBtn;
        private System.Windows.Forms.Button downBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.TextBox numberTb;
        private System.Windows.Forms.Button flyBtn;
        private System.Windows.Forms.Button landBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox ipTb;
        private BufferedPanel fieldPnl;
    }
}

