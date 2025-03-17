using project_management_v1.Application.DTOs;
using project_management_v1.Application.DTOs.ChartModels;

namespace project_management_v1.Services
{     /// <summary>
      /// Defines the contract for retrieving sales data.
      /// Decided to use this approach for maintainability and testability benefits.
      /// However, using ChartTypes could improve extensibility in the future.
      /// </summary>
    public interface ISalesDataService
    {
        Result<ChartResponse> GetGroupedDataByCategoryAndBrand();
        Result<SerieData?> GetTopSoldProductsByCategory();
        Result<SerieData?> GetSalesGroupedByRange();
    }
}
