namespace ClockApp;

public class RingDrawable : IDrawable
{
    public string Text { get; set; } = "00:00.00";
    public double Fraction { get; set; }

    private readonly DotDrawable _digits = new()
    {
        OnColor = Color.FromArgb("#B6FF3B"),
        OffColor = Color.FromArgb("#1B2A12")
    };

    private static readonly Color Bg = Color.FromArgb("#05070D");
    private static readonly Color RingColor = Color.FromArgb("#1B2A45");
    private static readonly Color Accent = Color.FromArgb("#B6FF3B");

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Bg;
        canvas.FillRectangle(dirtyRect);

        float cx = dirtyRect.X + dirtyRect.Width / 2;
        float cy = dirtyRect.Y + dirtyRect.Height / 2;
        float R = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2 * 0.94f;
        if (R <= 0) return;

        OrbitHelper.Ring(canvas, cx, cy, R, RingColor, Math.Max(1f, R * 0.01f));
        OrbitHelper.Ticks(canvas, cx, cy, R, 60, 5, R * 0.008f, RingColor);
        OrbitHelper.Planet(canvas, cx, cy, R, Fraction, Accent, R * 0.04f);

        _digits.Text = Text;
        _digits.DrawText(canvas, new RectF(cx - R * 0.65f, cy - R * 0.22f, R * 1.3f, R * 0.44f));
    }
}