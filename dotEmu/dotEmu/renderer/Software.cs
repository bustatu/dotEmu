using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace dotEmu.renderer
{
    class SoftwareRenderer : Control, IRenderer
    {
        public string rendererName => "Software";

        public int width;
        public int height;
        public uint[] rawData;


        // Constructor sets default sizes
        public SoftwareRenderer(int defaultX = 64, int defaultY = 32)
        {
            resize(defaultX, defaultY);

            DoubleBuffered = true;

            update();
        }

        // Resizes the raw memory
        public void resize(int w, int h)
        {
            width = w;
            height = h;
            rawData = new uint[(width + 1) * (height + 1)];
        }

        // Sets pixel in raw memory
        public void setPixel(int x, int y, uint r, uint g, uint b)
        {
            rawData[y * width + x] = 0xFF000000 | (r << 16) | (g << 8) | (b << 0);
        }

        // Updates the control
        public void update()
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Default control OnPaint event
            base.OnPaint(e);

            // Get render target
            Graphics renderTarget = e.Graphics;
            renderTarget.CompositingMode = CompositingMode.SourceCopy;
            renderTarget.InterpolationMode = InterpolationMode.NearestNeighbor;
            renderTarget.DrawImage(new Bitmap(width, height, width * 4,
                                PixelFormat.Format32bppPArgb, Marshal.UnsafeAddrOfPinnedArrayElement(rawData, 0)), 0, 0, Size.Width, Size.Height);
        }
    }
}
