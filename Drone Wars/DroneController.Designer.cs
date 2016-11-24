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
            this.forwardBtn = new System.Windows.Forms.Button();
            this.leftBtn = new System.Windows.Forms.Button();
            this.backwardsBtn = new System.Windows.Forms.Button();
            this.rightBtn = new System.Windows.Forms.Button();
            this.upBtn = new System.Windows.Forms.Button();
            this.downBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.numberTb = new System.Windows.Forms.TextBox();
            this.flyBtn = new System.Windows.Forms.Button();
            this.landBtn = new System.Windows.Forms.Button();
            this.mapGv = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mapGv)).BeginInit();
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
            // forwardBtn
            // 
            this.forwardBtn.Location = new System.Drawing.Point(41, 165);
            this.forwardBtn.Name = "forwardBtn";
            this.forwardBtn.Size = new System.Drawing.Size(23, 23);
            this.forwardBtn.TabIndex = 3;
            this.forwardBtn.Text = "F";
            this.forwardBtn.UseVisualStyleBackColor = true;
            this.forwardBtn.Click += new System.EventHandler(this.forwardBtn_Click);
            // 
            // leftBtn
            // 
            this.leftBtn.Location = new System.Drawing.Point(12, 194);
            this.leftBtn.Name = "leftBtn";
            this.leftBtn.Size = new System.Drawing.Size(23, 23);
            this.leftBtn.TabIndex = 4;
            this.leftBtn.Text = "L";
            this.leftBtn.UseVisualStyleBackColor = true;
            this.leftBtn.Click += new System.EventHandler(this.leftBtn_Click);
            // 
            // backwardsBtn
            // 
            this.backwardsBtn.Location = new System.Drawing.Point(41, 194);
            this.backwardsBtn.Name = "backwardsBtn";
            this.backwardsBtn.Size = new System.Drawing.Size(23, 23);
            this.backwardsBtn.TabIndex = 5;
            this.backwardsBtn.Text = "B";
            this.backwardsBtn.UseVisualStyleBackColor = true;
            this.backwardsBtn.Click += new System.EventHandler(this.backwardsBtn_Click);
            // 
            // rightBtn
            // 
            this.rightBtn.Location = new System.Drawing.Point(70, 194);
            this.rightBtn.Name = "rightBtn";
            this.rightBtn.Size = new System.Drawing.Size(23, 23);
            this.rightBtn.TabIndex = 6;
            this.rightBtn.Text = "R";
            this.rightBtn.UseVisualStyleBackColor = true;
            this.rightBtn.Click += new System.EventHandler(this.rightBtn_Click);
            // 
            // upBtn
            // 
            this.upBtn.Location = new System.Drawing.Point(99, 165);
            this.upBtn.Name = "upBtn";
            this.upBtn.Size = new System.Drawing.Size(23, 23);
            this.upBtn.TabIndex = 7;
            this.upBtn.Text = "U";
            this.upBtn.UseVisualStyleBackColor = true;
            this.upBtn.Click += new System.EventHandler(this.upBtn_Click);
            // 
            // downBtn
            // 
            this.downBtn.Location = new System.Drawing.Point(99, 194);
            this.downBtn.Name = "downBtn";
            this.downBtn.Size = new System.Drawing.Size(23, 23);
            this.downBtn.TabIndex = 8;
            this.downBtn.Text = "D";
            this.downBtn.UseVisualStyleBackColor = true;
            this.downBtn.Click += new System.EventHandler(this.downBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(128, 194);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(40, 23);
            this.stopBtn.TabIndex = 9;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // numberTb
            // 
            this.numberTb.Location = new System.Drawing.Point(12, 223);
            this.numberTb.Name = "numberTb";
            this.numberTb.Size = new System.Drawing.Size(81, 20);
            this.numberTb.TabIndex = 10;
            // 
            // flyBtn
            // 
            this.flyBtn.Location = new System.Drawing.Point(12, 133);
            this.flyBtn.Name = "flyBtn";
            this.flyBtn.Size = new System.Drawing.Size(75, 23);
            this.flyBtn.TabIndex = 11;
            this.flyBtn.Text = "Fly Drone";
            this.flyBtn.UseVisualStyleBackColor = true;
            this.flyBtn.Click += new System.EventHandler(this.flyBtn_Click);
            // 
            // landBtn
            // 
            this.landBtn.Location = new System.Drawing.Point(121, 133);
            this.landBtn.Name = "landBtn";
            this.landBtn.Size = new System.Drawing.Size(75, 23);
            this.landBtn.TabIndex = 12;
            this.landBtn.Text = "Land Drone";
            this.landBtn.UseVisualStyleBackColor = true;
            this.landBtn.Click += new System.EventHandler(this.landBtn_Click);
            // 
            // mapGv
            // 
            this.mapGv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.mapGv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.mapGv.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.mapGv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mapGv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mapGv.ColumnHeadersVisible = false;
            this.mapGv.Location = new System.Drawing.Point(202, 3);
            this.mapGv.Name = "mapGv";
            this.mapGv.RowHeadersVisible = false;
            this.mapGv.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mapGv.Size = new System.Drawing.Size(179, 153);
            this.mapGv.TabIndex = 13;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DroneController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(390, 257);
            this.Controls.Add(this.mapGv);
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
            this.Text = "Drone Wars";
            this.Load += new System.EventHandler(this.DroneController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapGv)).EndInit();
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
        private System.Windows.Forms.DataGridView mapGv;
        private System.Windows.Forms.Timer timer1;
    }
}

