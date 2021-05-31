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
            this.softwareRenderer = new renderer.SoftwareRenderer(64, 32);
            this.SuspendLayout();
            // 
            // softwareRenderer
            // 
            this.softwareRenderer.BackColor = System.Drawing.Color.Fuchsia;
            this.softwareRenderer.Location = new System.Drawing.Point(12, 12);
            this.softwareRenderer.Name = "softwareRenderer";
            this.softwareRenderer.Size = new System.Drawing.Size(920, 478);
            this.softwareRenderer.TabIndex = 2;
            // 
            // CHIP8UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(944, 502);
            this.Controls.Add(this.softwareRenderer);
            this.Name = "CHIP8UI";
            this.Text = "CHIP8";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CHIP8UI_FormClosed);
            this.Load += new System.EventHandler(this.CHIP8UI_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CHIP8UI_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CHIP8UI_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion
        private renderer.SoftwareRenderer softwareRenderer;
    }
}