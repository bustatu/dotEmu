using System.Windows.Forms;

namespace dotEmu.renderer
{
    class SoftwareRenderer : Control, IRenderer
    {
        public string Name => "Software";

        protected override void OnPaint(PaintEventArgs e)
        {
            // Default control OnPaint event
            base.OnPaint(e);
        }
    }
}
