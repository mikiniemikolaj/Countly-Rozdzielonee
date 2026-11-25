using System.Collections.ObjectModel;

namespace Countly;

public partial class MainPage : ContentPage
{
    private readonly ObservableCollection<Counter> _counters = new();
    private readonly CounterStorage _storage = new();

    public MainPage()
    {
        InitializeComponent();
        foreach (var counter in _storage.Load()) _counters.Add(counter);
        CountersList.ItemsSource = _counters;
    }

    private void Save() => _storage.Save(_counters);

    private void OnPlus(object? sender, EventArgs e)
    {
        if (GetCounter(sender) is not { } counter) return;
        counter.Value++;
        Save();
    }

    private void OnMinus(object? sender, EventArgs e)
    {
        if (GetCounter(sender) is not { } counter) return;
        counter.Value--;
        Save();
    }

    private async void OnAdd(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Nowy licznik", "Nazwa:");
        if (string.IsNullOrWhiteSpace(name)) return;

        var start = await DisplayPromptAsync("Nowy licznik", "Wartość początkowa:",
            initialValue: "0", keyboard: Keyboard.Numeric);
        int.TryParse(start, out var value);

        var selectedColor = await DisplayActionSheet("Kolor licznika", "Anuluj", null,
            CounterColors.All.Select(color => color.Name).ToArray());
        var color = CounterColors.All.FirstOrDefault(item => item.Name == selectedColor);
        if (color is null) return;

        _counters.Add(new Counter { Name = name.Trim(), Value = value, ColorHex = color.Hex });
        Save();
    }

    private void OnDelete(object sender, EventArgs e)
    {
        if (GetCounter(sender) is not { } counter) return;
        _counters.Remove(counter);
        Save();
    }

    private static Counter? GetCounter(object? sender) =>
        (sender as Button)?.BindingContext as Counter;
}
