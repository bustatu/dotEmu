using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace dotEmu.renderer
{
    class SoftwareRenderer : Control, IRenderer
    {
        public new string Name => "Software";

        protected override void OnPaint(PaintEventArgs e)
        {
            // Default control OnPaint event
            base.OnPaint(e);

            // Get render target
            Graphics renderTarget = e.Graphics;
            renderTarget.CompositingMode = CompositingMode.SourceCopy;
            renderTarget.InterpolationMode = InterpolationMode.NearestNeighbor;
            //renderTarget.DrawImage(0, 0);
        }
    }
}
