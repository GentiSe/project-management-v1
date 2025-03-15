using project_management_v1.Application.DTOs.ChartModels;

namespace project_management_v1.Infrastructure.Services
{
    public interface ISalesDataService
    {
        ChartResponse GetGroupedDataByCategoryAndBrand();
        SerieData? TopSoldByCategory();
        SerieData? GroupBySalesRange();
    }
}
