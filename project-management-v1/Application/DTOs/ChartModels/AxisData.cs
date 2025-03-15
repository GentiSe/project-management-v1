using System.Text.Json.Serialization;

namespace project_management_v1.Application.DTOs.ChartModels
{
    public class AxisData
    {
        public string? Type { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> Data { get; set; } = [];
    }
}
