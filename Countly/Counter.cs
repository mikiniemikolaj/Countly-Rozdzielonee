using System.ComponentModel;
using System.Xml.Serialization;

namespace Countly;

public class Counter : INotifyPropertyChanged
{
    private int _value;

    public string Name { get; set; } = string.Empty;
    public string ColorHex { get; set; } = CounterColors.DefaultHex;

    [XmlIgnore]
    public Color AccentColor => Color.FromArgb(ColorHex);

    public int Value
    {
        get => _value;
        set
        {
            if (_value == value) return;
            _value = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
