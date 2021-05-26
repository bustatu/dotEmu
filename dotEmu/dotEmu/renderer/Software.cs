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
        public new string Name => "Software";

        public int width;
        public int height;
        public Byte[] rawData;

        public SoftwareRenderer()
        {
            resize(64, 32);

            for (int i = 1; i <= 32; i++)
                setPixel(i-1, i-1, 0xFF, 0xFF, 0xFF);
        }

        public void resize(int w, int h)
        {
            width = w;
            height = h;
            rawData = new Byte[width * height * 4];
        }

        public void setPixel(int x, int y, Byte r, Byte g, Byte b)
        {
            rawData[y * width * 4 + x * 4] = 0xFF;
            rawData[y * width * 4 + x * 4 + 1] = r;
            rawData[y * width * 4 + x * 4 + 2] = g;
            rawData[y * width * 4 + x * 4 + 3] = b;

            Invalidate();
            Update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Default control OnPaint event
            base.OnPaint(e);

            // Get render target
            Graphics renderTarget = e.Graphics;
            renderTarget.CompositingMode = CompositingMode.SourceCopy;
            renderTarget.InterpolationMode = InterpolationMode.NearestNeighbor;
            Bitmap image = new Bitmap(width, height, width * 4,
                                PixelFormat.Format32bppPArgb, Marshal.UnsafeAddrOfPinnedArrayElement(rawData, 0));
            renderTarget.DrawImage(image, 0, 0, Size.Width, Size.Height);
        }
    }
}
