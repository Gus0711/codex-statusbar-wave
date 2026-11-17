using System.Drawing.Drawing2D;

namespace CodexStatusbar;

/// <summary>A low-contrast travelling colour wave behind the status text.</summary>
internal static class WaveBackground
{
    public static void Paint(Graphics graphics, GraphicsPath outline, Rectangle bounds, double seconds)
    {
        if (bounds.Width <= 0 || bounds.Height <= 0) return;
        var phase = seconds * Math.PI * 2 / 9;
        var colours = new Color[17];
        var positions = new float[colours.Length];
        for (var i = 0; i < colours.Length; i++)
        {
            var position = i / (float)(colours.Length - 1);
            var angle = position * Math.PI * 2 - phase;
            // Indigo, plum and teal stay dark enough for the light status text.
            colours[i] = Color.FromArgb(
                255,
                (int)Math.Round(43 + 13 * Math.Sin(angle)),
                (int)Math.Round(43 + 17 * Math.Sin(angle + 2.1)),
                (int)Math.Round(83 + 22 * Math.Sin(angle + 0.7)));
            positions[i] = position;
        }
        using var gradient = new LinearGradientBrush(bounds, Color.Black, Color.Black, 0f);
        gradient.InterpolationColors = new ColorBlend
        {
            Colors = colours,
            Positions = positions
        };
        graphics.FillPath(gradient, outline);

        var saved = graphics.Save();
        try
        {
            graphics.SetClip(outline, CombineMode.Intersect);
            using var wave = new GraphicsPath();
            var points = new List<PointF>();
            for (var x = 0; x <= bounds.Width + 4; x += 4)
            {
                var y = bounds.Top + bounds.Height *
                    (0.66 + 0.17 * Math.Sin(x / (double)bounds.Width * Math.PI * 2 - phase));
                points.Add(new PointF(bounds.Left + x, (float)y));
            }
            wave.AddLines(points.ToArray());
            wave.AddLine(points[^1], new PointF(bounds.Right + 4, bounds.Bottom + 1));
            wave.AddLine(new PointF(bounds.Right + 4, bounds.Bottom + 1), new PointF(bounds.Left, bounds.Bottom + 1));
            wave.CloseFigure();
            using var glow = new SolidBrush(Color.FromArgb(18, 105, 190, 220));
            graphics.FillPath(glow, wave);
        }
        finally
        {
            graphics.Restore(saved);
        }
        using var border = new Pen(Color.FromArgb(45, 175, 190, 240), 1f);
        graphics.DrawPath(border, outline);
    }
}
