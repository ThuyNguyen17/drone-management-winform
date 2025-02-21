using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedTextBox : TextBox
{
    private int cornerRadius = 15;
    private string placeholderText = "Enter text here...";

    [Description("Gets or sets the placeholder text."), Category("Appearance")]
    public string PlaceholderText
    {
        get { return placeholderText; }
        set { placeholderText = value; Invalidate(); }
    }

    public RoundedTextBox()
    {
        this.BorderStyle = BorderStyle.None;
        this.BackColor = Color.WhiteSmoke;
        this.Padding = new Padding(10, 5, 10, 5);
        this.SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics graphics = e.Graphics;
        Rectangle rect = this.ClientRectangle;

        using (GraphicsPath path = new GraphicsPath())
        {
            path.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y, cornerRadius, cornerRadius, 270, 90);
            path.AddArc(rect.X + rect.Width - cornerRadius, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawPath(Pens.Gray, path);
        }

        if (string.IsNullOrEmpty(this.Text) && !this.Focused)
        {
            using (Brush brush = new SolidBrush(Color.Gray))
            {
                e.Graphics.DrawString(placeholderText, this.Font, brush, new PointF(Padding.Left, Padding.Top));
            }
        }
    }

    protected override void OnGotFocus(EventArgs e)
    {
        base.OnGotFocus(e);
        Invalidate(); // Redraw to remove placeholder text
    }

    protected override void OnLostFocus(EventArgs e)
    {
        base.OnLostFocus(e);
        Invalidate(); // Redraw to show placeholder text if needed
    }
}