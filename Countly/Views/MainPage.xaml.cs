using System.Collections.ObjectModel;
using Countly.Models;
using Countly.Services;

namespace Countly.Views;

public partial class MainPage : ContentPage
{
    private readonly ObservableCollection<Counter> _counters = new();
    private readonly CounterStorage _storage = new();

    public MainPage()
    {
        InitializeComponent();

        foreach (var counter in _storage.Load())
            _counters.Add(counter);

        CountersList.ItemsSource = _counters;
    }

    private void OnPlus(object? sender, EventArgs e)
    {
        var counter = (Counter)((Button)sender!).BindingContext;
        counter.Value++;
        _storage.Save(_counters);
    }

    private void OnMinus(object? sender, EventArgs e)
    {
        var counter = (Counter)((Button)sender!).BindingContext;
        counter.Value--;
        _storage.Save(_counters);
    }

    private async void OnAdd(object sender, EventArgs e)
    {
        string? name = await DisplayPromptAsync("Nowy licznik", "Nazwa:");
        if (string.IsNullOrWhiteSpace(name)) return;

        string? start = await DisplayPromptAsync("Nowy licznik", "Wartość początkowa:",
            initialValue: "0", keyboard: Keyboard.Numeric);
        int.TryParse(start, out int value);

        string? selectedColor = await DisplayActionSheet(
            "Kolor licznika",
            "Anuluj",
            null,
            CounterColors.All.Select(c => c.Name).ToArray());

        if (selectedColor is null or "Anuluj") return;

        string colorHex = CounterColors.All.First(c => c.Name == selectedColor).Hex;

        _counters.Add(new Counter { Name = name, Value = value, ColorHex = colorHex });
        _storage.Save(_counters);
    }

    private void OnDelete(object sender, EventArgs e)
    {
        var counter = (Counter)((Button)sender).BindingContext;
        _counters.Remove(counter);
        _storage.Save(_counters);
    }
}
