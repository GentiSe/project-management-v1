using System.Text.Json;

namespace project_management_v1.Services
{
    public class JsonSerializerService : IJsonSerializerService
    {
        private static readonly JsonSerializerOptions _writeOptions = new()
        {
            WriteIndented = true
        };

        private static readonly JsonSerializerOptions _readOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        public T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _readOptions);
        }

        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, _writeOptions);
        }
    }
}
