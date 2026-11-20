namespace ClockApp;

public class ClockView : ContentView
{
    private readonly OrbitDrawable _drawable = new();
    private readonly GraphicsView _graphics;
    private IDispatcherTimer? _timer;
    
    public TimeZoneInfo Zone { get; set; } = TimeZoneInfo.Local;

    public ClockView()
    {
        _graphics = new GraphicsView { Drawable = _drawable };
        Content = _graphics;

        Loaded += (_, _) => Start();
        Unloaded += (_, _) => _timer?.Stop();
    }

    private void Start()
    {
        Update();

        if (_timer == null)
        {
            _timer = Dispatcher.CreateTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(33);
            _timer.Tick += (_, _) => Update();
        }
        _timer.Start();
    }

    private void Update()
    {
        _drawable.Time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Zone);
        _graphics.Invalidate();
    }
}