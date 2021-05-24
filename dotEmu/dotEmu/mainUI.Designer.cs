
namespace dotEmu
{
    partial class mainUI
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
            this.clockLabel = new System.Windows.Forms.Label();
            this.doStepButton = new System.Windows.Forms.Button();
            this.NLabel = new System.Windows.Forms.Label();
            this.VLabel = new System.Windows.Forms.Label();
            this.ULabel = new System.Windows.Forms.Label();
            this.BLabel = new System.Windows.Forms.Label();
            this.DLabel = new System.Windows.Forms.Label();
            this.ILabel = new System.Windows.Forms.Label();
            this.ZLabel = new System.Windows.Forms.Label();
            this.CLabel = new System.Windows.Forms.Label();
            this.PCLabel = new System.Windows.Forms.Label();
            this.SPLabel = new System.Windows.Forms.Label();
            this.ALabel = new System.Windows.Forms.Label();
            this.XLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.dissLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clockLabel
            // 
            this.clockLabel.AutoSize = true;
            this.clockLabel.Location = new System.Drawing.Point(10, 8);
            this.clockLabel.Name = "clockLabel";
            this.clockLabel.Size = new System.Drawing.Size(49, 13);
            this.clockLabel.TabIndex = 0;
            this.clockLabel.Text = "Clock: -1";
            // 
            // doStepButton
            // 
            this.doStepButton.Location = new System.Drawing.Point(10, 404);
            this.doStepButton.Name = "doStepButton";
            this.doStepButton.Size = new System.Drawing.Size(64, 20);
            this.doStepButton.TabIndex = 1;
            this.doStepButton.Text = "Do Step";
            this.doStepButton.UseVisualStyleBackColor = true;
            this.doStepButton.Click += new System.EventHandler(this.doStepButton_Click);
            // 
            // NLabel
            // 
            this.NLabel.AutoSize = true;
            this.NLabel.Location = new System.Drawing.Point(10, 30);
            this.NLabel.Name = "NLabel";
            this.NLabel.Size = new System.Drawing.Size(15, 13);
            this.NLabel.TabIndex = 2;
            this.NLabel.Text = "N";
            // 
            // VLabel
            // 
            this.VLabel.AutoSize = true;
            this.VLabel.Location = new System.Drawing.Point(29, 30);
            this.VLabel.Name = "VLabel";
            this.VLabel.Size = new System.Drawing.Size(14, 13);
            this.VLabel.TabIndex = 3;
            this.VLabel.Text = "V";
            // 
            // ULabel
            // 
            this.ULabel.AutoSize = true;
            this.ULabel.Location = new System.Drawing.Point(48, 30);
            this.ULabel.Name = "ULabel";
            this.ULabel.Size = new System.Drawing.Size(15, 13);
            this.ULabel.TabIndex = 4;
            this.ULabel.Text = "U";
            // 
            // BLabel
            // 
            this.BLabel.AutoSize = true;
            this.BLabel.Location = new System.Drawing.Point(67, 30);
            this.BLabel.Name = "BLabel";
            this.BLabel.Size = new System.Drawing.Size(14, 13);
            this.BLabel.TabIndex = 5;
            this.BLabel.Text = "B";
            // 
            // DLabel
            // 
            this.DLabel.AutoSize = true;
            this.DLabel.Location = new System.Drawing.Point(86, 30);
            this.DLabel.Name = "DLabel";
            this.DLabel.Size = new System.Drawing.Size(15, 13);
            this.DLabel.TabIndex = 6;
            this.DLabel.Text = "D";
            // 
            // ILabel
            // 
            this.ILabel.AutoSize = true;
            this.ILabel.Location = new System.Drawing.Point(105, 30);
            this.ILabel.Name = "ILabel";
            this.ILabel.Size = new System.Drawing.Size(10, 13);
            this.ILabel.TabIndex = 7;
            this.ILabel.Text = "I";
            // 
            // ZLabel
            // 
            this.ZLabel.AutoSize = true;
            this.ZLabel.Location = new System.Drawing.Point(123, 30);
            this.ZLabel.Name = "ZLabel";
            this.ZLabel.Size = new System.Drawing.Size(14, 13);
            this.ZLabel.TabIndex = 8;
            this.ZLabel.Text = "Z";
            // 
            // CLabel
            // 
            this.CLabel.AutoSize = true;
            this.CLabel.Location = new System.Drawing.Point(142, 30);
            this.CLabel.Name = "CLabel";
            this.CLabel.Size = new System.Drawing.Size(14, 13);
            this.CLabel.TabIndex = 9;
            this.CLabel.Text = "C";
            // 
            // PCLabel
            // 
            this.PCLabel.AutoSize = true;
            this.PCLabel.Location = new System.Drawing.Point(62, 8);
            this.PCLabel.Name = "PCLabel";
            this.PCLabel.Size = new System.Drawing.Size(36, 13);
            this.PCLabel.TabIndex = 10;
            this.PCLabel.Text = "PC: -1";
            // 
            // SPLabel
            // 
            this.SPLabel.AutoSize = true;
            this.SPLabel.Location = new System.Drawing.Point(142, 53);
            this.SPLabel.Name = "SPLabel";
            this.SPLabel.Size = new System.Drawing.Size(36, 13);
            this.SPLabel.TabIndex = 11;
            this.SPLabel.Text = "SP: -1";
            // 
            // ALabel
            // 
            this.ALabel.AutoSize = true;
            this.ALabel.Location = new System.Drawing.Point(10, 53);
            this.ALabel.Name = "ALabel";
            this.ALabel.Size = new System.Drawing.Size(29, 13);
            this.ALabel.TabIndex = 12;
            this.ALabel.Text = "A: -1";
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Location = new System.Drawing.Point(52, 53);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(29, 13);
            this.XLabel.TabIndex = 13;
            this.XLabel.Text = "X: -1";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Location = new System.Drawing.Point(96, 53);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(29, 13);
            this.YLabel.TabIndex = 14;
            this.YLabel.Text = "Y: -1";
            // 
            // dissLabel
            // 
            this.dissLabel.AutoSize = true;
            this.dissLabel.Location = new System.Drawing.Point(460, 8);
            this.dissLabel.Name = "dissLabel";
            this.dissLabel.Size = new System.Drawing.Size(68, 13);
            this.dissLabel.TabIndex = 15;
            this.dissLabel.Text = "Dissasembly:";
            // 
            // mainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(809, 434);
            this.Controls.Add(this.dissLabel);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.ALabel);
            this.Controls.Add(this.SPLabel);
            this.Controls.Add(this.PCLabel);
            this.Controls.Add(this.CLabel);
            this.Controls.Add(this.ZLabel);
            this.Controls.Add(this.ILabel);
            this.Controls.Add(this.DLabel);
            this.Controls.Add(this.BLabel);
            this.Controls.Add(this.ULabel);
            this.Controls.Add(this.VLabel);
            this.Controls.Add(this.NLabel);
            this.Controls.Add(this.doStepButton);
            this.Controls.Add(this.clockLabel);
            this.ForeColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "mainUI";
            this.Text = "Emulator Engine";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clockLabel;
        private System.Windows.Forms.Button doStepButton;
        private System.Windows.Forms.Label NLabel;
        private System.Windows.Forms.Label BLabel;
        private System.Windows.Forms.Label DLabel;
        private System.Windows.Forms.Label ZLabel;
        private System.Windows.Forms.Label CLabel;
        private System.Windows.Forms.Label VLabel;
        private System.Windows.Forms.Label ULabel;
        private System.Windows.Forms.Label ILabel;
        private System.Windows.Forms.Label PCLabel;
        private System.Windows.Forms.Label SPLabel;
        private System.Windows.Forms.Label ALabel;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.Label dissLabel;
    }
}