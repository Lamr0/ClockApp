using System.Diagnostics;

namespace ClockApp;


public class StopwatchView : ContentView
{
    private readonly RingDrawable _drawable = new();
    private readonly GraphicsView _graphics;
    private readonly Stopwatch _sw = new();
    private IDispatcherTimer? _timer;
    private string _last = "";

    public bool IsRunning => _sw.IsRunning;

    public StopwatchView()
    {
        _graphics = new GraphicsView { Drawable = _drawable };
        Content = _graphics;

        Unloaded += (_, _) => _timer?.Stop();
    }

    public void Start()
    {
        _sw.Start();

        if (_timer == null)
        {
            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(30);
            _timer.Tick += (_, _) => Update();
        }
        _timer.Start();
    }

    public void Stop()
    {
        _sw.Stop();
        _timer?.Stop();
        Update();
    }

    public void Reset()
    {
        _sw.Reset();
        _timer?.Stop();
        Update();
    }

    private void Update()
    {
        var e = _sw.Elapsed;
        var text = $"{(int)e.TotalMinutes:00}:{e.Seconds:00}.{e.Milliseconds / 10:00}";

        if (text == _last) return;

        _last = text;
        _drawable.Text = text;
        _drawable.Fraction = (e.TotalSeconds % 60) / 60.0;
        _graphics.Invalidate();
    }
}