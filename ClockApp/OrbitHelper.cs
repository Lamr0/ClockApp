namespace ClockApp;

public static class OrbitHelper
{
    
    public static PointF OnCircle(float cx, float cy, float r, double fraction)
    {
        double a = fraction * 2 * Math.PI;
        return new PointF(cx + (float)(r * Math.Sin(a)), cy - (float)(r * Math.Cos(a)));
    }
    
    public static void Ring(ICanvas canvas, float cx, float cy, float r, Color color, float width)
    {
        canvas.StrokeColor = color;
        canvas.StrokeSize = width;
        canvas.DrawCircle(cx, cy, r);
    }
    
    public static void Ticks(ICanvas canvas, float cx, float cy, float r, int count, int bigEvery, float dotR, Color color)
    {
        canvas.FillColor = color;
        for (int i = 0; i < count; i++)
        {
            var p = OnCircle(cx, cy, r, (double)i / count);
            float rr = i % bigEvery == 0 ? dotR * 1.8f : dotR;
            canvas.FillCircle(p.X, p.Y, rr);
        }
    }
    
    public static void Planet(ICanvas canvas, float cx, float cy, float r, double fraction, Color color, float size)
    {
        const int tail = 14;
        for (int k = tail; k >= 1; k--)
        {
            float t = 1f - (float)k / (tail + 1);
            var p = OnCircle(cx, cy, r, fraction - k * 0.006);
            canvas.FillColor = color.WithAlpha(0.45f * t);
            canvas.FillCircle(p.X, p.Y, size * (0.35f + 0.5f * t));
        }

        var pos = OnCircle(cx, cy, r, fraction);
        canvas.FillColor = color.WithAlpha(0.25f);
        canvas.FillCircle(pos.X, pos.Y, size * 2.2f);
        canvas.FillColor = color;
        canvas.FillCircle(pos.X, pos.Y, size);
    }
}