namespace ClockApp;

public class DotDrawable : IDrawable
{
    public string Text { get; set; } = "00:00:00";

    public Color Background { get; set; } = Colors.Black;
    public Color OnColor { get; set; } = Color.FromArgb("#33E6FF");
    public Color OffColor { get; set; } = Color.FromArgb("#12333A");

    private const int Gap = 1;
    
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Background;
        canvas.FillRectangle(dirtyRect);
        DrawText(canvas, dirtyRect);
    }
    
    public void DrawText(ICanvas canvas, RectF area)
    {
        if (Text.Length == 0) return;

        int totalCols = Text.Sum(DotFont.Width) + Gap * (Text.Length - 1);
        
        float cell = Math.Min(area.Width / totalCols, area.Height / DotFont.Rows) * 0.9f;
        if (cell <= 0) return;

        float x = area.X + (area.Width - totalCols * cell) / 2;
        float y0 = area.Y + (area.Height - DotFont.Rows * cell) / 2;

        float dot = cell * 0.78f;
        float pad = (cell - dot) / 2;
        float radius = dot * 0.2f;

        foreach (char ch in Text)
        {
            var glyph = DotFont.Get(ch);
            int w = glyph[0].Length;

            for (int r = 0; r < DotFont.Rows; r++)
            {
                for (int c = 0; c < w; c++)
                {
                    canvas.FillColor = glyph[r][c] == '#' ? OnColor : OffColor;
                    canvas.FillRoundedRectangle(x + c * cell + pad, y0 + r * cell + pad, dot, dot, radius);
                }
            }

            x += (w + Gap) * cell;
        }
    }
}