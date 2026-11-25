namespace Countly;

public sealed record CounterColor(string Name, string Hex);

public static class CounterColors
{
    public const string DefaultHex = "#6C5CE7";

    public static IReadOnlyList<CounterColor> All { get; } =
    [
        new("Fioletowy", DefaultHex),
        new("Niebieski", "#2563EB"),
        new("Turkusowy", "#0891B2"),
        new("Zielony", "#16A34A"),
        new("Limonkowy", "#65A30D"),
        new("Żółty", "#CA8A04"),
        new("Pomarańczowy", "#EA580C"),
        new("Czerwony", "#DC2626"),
        new("Różowy", "#DB2777"),
        new("Grafitowy", "#475569")
    ];
}
