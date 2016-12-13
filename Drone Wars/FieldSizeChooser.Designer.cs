namespace Drone_Wars
{
    partial class FieldSizeChooser
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
            this.xLbl = new System.Windows.Forms.Label();
            this.yLbl = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.xTb = new System.Windows.Forms.TextBox();
            this.yTb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // xLbl
            // 
            this.xLbl.AutoSize = true;
            this.xLbl.Location = new System.Drawing.Point(17, 15);
            this.xLbl.Name = "xLbl";
            this.xLbl.Size = new System.Drawing.Size(35, 13);
            this.xLbl.TabIndex = 0;
            this.xLbl.Text = "Width";
            // 
            // yLbl
            // 
            this.yLbl.AutoSize = true;
            this.yLbl.Location = new System.Drawing.Point(12, 41);
            this.yLbl.Name = "yLbl";
            this.yLbl.Size = new System.Drawing.Size(40, 13);
            this.yLbl.TabIndex = 1;
            this.yLbl.Text = "Length";
            // 
            // okBtn
            // 
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Location = new System.Drawing.Point(58, 64);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // xTb
            // 
            this.xTb.Location = new System.Drawing.Point(58, 12);
            this.xTb.Name = "xTb";
            this.xTb.Size = new System.Drawing.Size(100, 20);
            this.xTb.TabIndex = 3;
            // 
            // yTb
            // 
            this.yTb.Location = new System.Drawing.Point(58, 38);
            this.yTb.Name = "yTb";
            this.yTb.Size = new System.Drawing.Size(100, 20);
            this.yTb.TabIndex = 4;
            // 
            // FieldSizeChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 93);
            this.Controls.Add(this.yTb);
            this.Controls.Add(this.xTb);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.yLbl);
            this.Controls.Add(this.xLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FieldSizeChooser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Set a field size...";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label xLbl;
        private System.Windows.Forms.Label yLbl;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.TextBox xTb;
        private System.Windows.Forms.TextBox yTb;
    }
}