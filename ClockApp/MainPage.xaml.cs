namespace ClockApp;

public partial class MainPage : ContentPage
{
    private bool? _wide;

    public MainPage()
    {
        InitializeComponent();
    }
    
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width <= 0 || height <= 0) return;

        bool wide = width > height;
        if (wide == _wide) return;
        _wide = wide;

        if (wide)
        {
            Root.RowDefinitions = new RowDefinitionCollection(new RowDefinition(GridLength.Star));
            Root.ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition(GridLength.Star), new ColumnDefinition(GridLength.Star));

            Grid.SetRow(ClockDisplay, 0);    Grid.SetColumn(ClockDisplay, 0);
            Grid.SetRow(StopwatchPanel, 0);  Grid.SetColumn(StopwatchPanel, 1);
        }
        else
        {
            Root.ColumnDefinitions = new ColumnDefinitionCollection(new ColumnDefinition(GridLength.Star));
            Root.RowDefinitions = new RowDefinitionCollection(
                new RowDefinition(GridLength.Star), new RowDefinition(GridLength.Star));

            Grid.SetRow(ClockDisplay, 0);    Grid.SetColumn(ClockDisplay, 0);
            Grid.SetRow(StopwatchPanel, 1);  Grid.SetColumn(StopwatchPanel, 0);
        }
    }
    
    private void OnStartStopClicked(object? sender, EventArgs e)
    {
        if (StopwatchDisplay.IsRunning)
        {
            StopwatchDisplay.Stop();
            StartStopBtn.Text = "Старт";
        }
        else
        {
            StopwatchDisplay.Start();
            StartStopBtn.Text = "Стоп";
        }
    }
    
    private void OnResetClicked(object? sender, EventArgs e)
    {
        StopwatchDisplay.Reset();
        StartStopBtn.Text = "Старт";
    }
}