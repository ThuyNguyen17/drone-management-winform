using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Drone.GUI.Controls
{
    public class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 20;
        public Color FillColor { get; set; } = Color.LightPink;
        public Color BorderColor { get; set; } = Color.Black;
        public float BorderWidth { get; set; } = 2;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = CornerRadius;
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(Width - radius - 1, 0, radius, radius, 270, 90);
                path.AddArc(Width - radius - 1, Height - radius - 1, radius, radius, 0, 90);
                path.AddArc(0, Height - radius - 1, radius, radius, 90, 90);
                path.CloseAllFigures();

                using (SolidBrush brush = new SolidBrush(FillColor))
                {
                    g.FillPath(brush, path);
                }

                if (BorderWidth > 0)
                {
                    using (Pen pen = new Pen(BorderColor, BorderWidth))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }
    }
}
