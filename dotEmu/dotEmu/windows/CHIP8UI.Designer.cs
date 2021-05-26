namespace dotEmu.windows
{
    partial class CHIP8UI
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
            this.button1 = new System.Windows.Forms.Button();
            this.dissLabel = new System.Windows.Forms.Label();
            this.softwareRenderer = new dotEmu.renderer.SoftwareRenderer();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Do Step";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dissLabel
            // 
            this.dissLabel.AutoSize = true;
            this.dissLabel.Location = new System.Drawing.Point(711, 22);
            this.dissLabel.Name = "dissLabel";
            this.dissLabel.Size = new System.Drawing.Size(68, 13);
            this.dissLabel.TabIndex = 1;
            this.dissLabel.Text = "Dissasembly:";
            // 
            // softwareRenderer
            // 
            this.softwareRenderer.BackColor = System.Drawing.Color.Fuchsia;
            this.softwareRenderer.Location = new System.Drawing.Point(12, 120);
            this.softwareRenderer.Size = new System.Drawing.Size(687, 370);
            this.softwareRenderer.TabIndex = 2;
            // 
            // CHIP8UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(944, 502);
            this.Controls.Add(this.softwareRenderer);
            this.Controls.Add(this.dissLabel);
            this.Controls.Add(this.button1);
            this.Name = "CHIP8UI";
            this.Text = "CHIP8";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label dissLabel;
        private renderer.SoftwareRenderer softwareRenderer;
    }
}