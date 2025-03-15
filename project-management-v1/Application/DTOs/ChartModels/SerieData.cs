using System.Text.Json.Serialization;

namespace project_management_v1.Application.DTOs.ChartModels
{
    public class SerieData
    {
        public string? Type { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }
        public List<Datum> Data { get; set; } = [];

    }
}
