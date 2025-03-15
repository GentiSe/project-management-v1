using System.Text.Json.Serialization;

namespace project_management_v1.Application.DTOs.ChartModels
{
    public class Datum
    {
        public double Value { get; set; }
        public string? Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ItemStyle ItemStyle { get; set; }
    }

    public class ItemStyle
    {
        public ColorModel Normal { get; set; }
        public ColorModel Emphasis { get; set; }
        public class ColorModel
        {
            public string Color { get; set; }
        }
    }
}
