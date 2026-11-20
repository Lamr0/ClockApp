namespace ClockApp;

public class OrbitDrawable : IDrawable
{
    public DateTime Time { get; set; } = DateTime.Now;

    private readonly DotDrawable _digits = new()
    {
        OnColor = Color.FromArgb("#E8F1FF"),
        OffColor = Color.FromArgb("#111A2B")
    };

    private static readonly Color Bg = Color.FromArgb("#05070D");
    private static readonly Color RingColor = Color.FromArgb("#1B2A45");
    private static readonly Color HourColor = Color.FromArgb("#FFB347");
    private static readonly Color MinuteColor = Color.FromArgb("#9B7BFF");
    private static readonly Color SecondColor = Color.FromArgb("#33E6FF");

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Bg;
        canvas.FillRectangle(dirtyRect);

        float cx = dirtyRect.X + dirtyRect.Width / 2;
        float cy = dirtyRect.Y + dirtyRect.Height / 2;
        float R = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2 * 0.94f;
        if (R <= 0) return;
        
        double sec = Time.Second + Time.Millisecond / 1000.0;
        double min = Time.Minute + sec / 60;
        double hour = Time.Hour % 12 + min / 60;

        float rs = R;
        float rm = R * 0.82f;
        float rh = R * 0.66f;
        float line = Math.Max(1f, R * 0.01f);

        OrbitHelper.Ring(canvas, cx, cy, rs, RingColor, line);
        OrbitHelper.Ring(canvas, cx, cy, rm, RingColor, line);
        OrbitHelper.Ring(canvas, cx, cy, rh, RingColor, line);

        OrbitHelper.Ticks(canvas, cx, cy, rs, 60, 5, R * 0.008f, RingColor);
        OrbitHelper.Ticks(canvas, cx, cy, rm, 12, 3, R * 0.01f, RingColor);
        OrbitHelper.Ticks(canvas, cx, cy, rh, 12, 3, R * 0.01f, RingColor);

        OrbitHelper.Planet(canvas, cx, cy, rh, hour / 12, HourColor, R * 0.055f);
        OrbitHelper.Planet(canvas, cx, cy, rm, min / 60, MinuteColor, R * 0.045f);
        OrbitHelper.Planet(canvas, cx, cy, rs, sec / 60, SecondColor, R * 0.035f);
        
        _digits.Text = Time.ToString("HH:mm:ss");
        _digits.DrawText(canvas, new RectF(cx - R * 0.5f, cy - R * 0.17f, R * 1.0f, R * 0.34f));
    }
}