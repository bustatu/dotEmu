
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
            this.CHIP8Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CHIP8Button
            // 
            this.CHIP8Button.Location = new System.Drawing.Point(13, 13);
            this.CHIP8Button.Name = "CHIP8Button";
            this.CHIP8Button.Size = new System.Drawing.Size(919, 477);
            this.CHIP8Button.TabIndex = 0;
            this.CHIP8Button.Text = "CHIP8";
            this.CHIP8Button.UseVisualStyleBackColor = true;
            this.CHIP8Button.Click += new System.EventHandler(this.CHIP8Button_Click);
            // 
            // mainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(944, 502);
            this.Controls.Add(this.CHIP8Button);
            this.ForeColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "mainUI";
            this.Text = "Emulator Engine";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CHIP8Button;
    }
}