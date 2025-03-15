namespace project_management_v1.Application.DTOs.ChartModels
{
    public class ChartResponse
    {
        public AxisData XAxis { get; set; }
        public AxisData YAxis { get; set; }
        public List<SerieData> Series { get; set; }
    }
}
