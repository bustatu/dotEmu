using dotEmu.renderer;

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
            this.softwareRenderer = new dotEmu.renderer.SoftwareRenderer();
            this.SuspendLayout();
            // 
            // softwareRenderer
            // 
            this.softwareRenderer.AllowDrop = true;
            this.softwareRenderer.BackColor = System.Drawing.Color.Fuchsia;
            this.softwareRenderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.softwareRenderer.Location = new System.Drawing.Point(0, 0);
            this.softwareRenderer.Name = "softwareRenderer";
            this.softwareRenderer.Size = new System.Drawing.Size(944, 502);
            this.softwareRenderer.TabIndex = 2;
            this.softwareRenderer.DragDrop += new System.Windows.Forms.DragEventHandler(this.softwareRenderer_DragDrop);
            this.softwareRenderer.DragEnter += new System.Windows.Forms.DragEventHandler(this.softwareRenderer_DragEnter);
            // 
            // CHIP8UI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(944, 502);
            this.Controls.Add(this.softwareRenderer);
            this.Name = "CHIP8UI";
            this.ShowIcon = false;
            this.Text = "CHIP8";
            this.ResumeLayout(false);

        }

        #endregion
        private SoftwareRenderer softwareRenderer;
    }
}