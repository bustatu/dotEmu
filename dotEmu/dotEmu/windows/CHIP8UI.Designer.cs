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
            this.dissLabel = new System.Windows.Forms.Label();
            this.ILabel = new System.Windows.Forms.Label();
            this.RegisterLabel = new System.Windows.Forms.Label();
            this.stackLabel = new System.Windows.Forms.Label();
            this.softwareRenderer = new renderer.SoftwareRenderer(64, 32);
            this.SuspendLayout();
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
            // ILabel
            // 
            this.ILabel.AutoSize = true;
            this.ILabel.Location = new System.Drawing.Point(13, 101);
            this.ILabel.Name = "ILabel";
            this.ILabel.Size = new System.Drawing.Size(13, 13);
            this.ILabel.TabIndex = 3;
            this.ILabel.Text = "I:";
            // 
            // RegisterLabel
            // 
            this.RegisterLabel.AutoSize = true;
            this.RegisterLabel.Location = new System.Drawing.Point(711, 240);
            this.RegisterLabel.Name = "RegisterLabel";
            this.RegisterLabel.Size = new System.Drawing.Size(54, 13);
            this.RegisterLabel.TabIndex = 4;
            this.RegisterLabel.Text = "Registers:";
            // 
            // stackLabel
            // 
            this.stackLabel.AutoSize = true;
            this.stackLabel.Location = new System.Drawing.Point(827, 240);
            this.stackLabel.Name = "stackLabel";
            this.stackLabel.Size = new System.Drawing.Size(38, 13);
            this.stackLabel.TabIndex = 5;
            this.stackLabel.Text = "Stack:";
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
            this.Controls.Add(this.stackLabel);
            this.Controls.Add(this.RegisterLabel);
            this.Controls.Add(this.ILabel);
            this.Controls.Add(this.softwareRenderer);
            this.Controls.Add(this.dissLabel);
            this.Name = "CHIP8UI";
            this.Text = "CHIP8";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dissLabel;
        private renderer.SoftwareRenderer softwareRenderer;
        private System.Windows.Forms.Label ILabel;
        private System.Windows.Forms.Label RegisterLabel;
        private System.Windows.Forms.Label stackLabel;
    }
}