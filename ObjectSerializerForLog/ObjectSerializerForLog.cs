using System.Text.Json;

namespace ObjectSerializerForLog
{
    public class ObjectSerializerForLog
    {
        public static string Log<T>(T obj, bool indented = true) => JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = indented }) ?? "Error serializing object!";
    }
}