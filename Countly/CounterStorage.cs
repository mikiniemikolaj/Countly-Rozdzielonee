using System.Xml.Serialization;

namespace Countly;

public sealed class CounterStorage
{
    private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.xml");

    public List<Counter> Load()
    {
        if (!File.Exists(_filePath)) return [];

        var serializer = new XmlSerializer(typeof(List<Counter>));
        using var stream = File.OpenRead(_filePath);
        return serializer.Deserialize(stream) as List<Counter> ?? [];
    }

    public void Save(IEnumerable<Counter> counters)
    {
        var serializer = new XmlSerializer(typeof(List<Counter>));
        using var stream = File.Create(_filePath);
        serializer.Serialize(stream, counters.ToList());
    }
}
