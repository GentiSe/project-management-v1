using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_management_v1.Services;

namespace project_management_v1.Controllers
{
    [Route("api/v1/sales")]
    [ApiController]
    public class SalesAggregationController(ISalesDataService _salesService) : ControllerBase
    {

        [HttpGet("group-by-category")]
        [AllowAnonymous]
        public IActionResult GetGroupedByCategory()
        {
            var result = _salesService.GetGroupedDataByCategoryAndBrand();
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpGet("top-sold-by-category")]
        [AllowAnonymous]
        public IActionResult GetTopSoldByCategory()
        {
            var result = _salesService.GetTopSoldProductsByCategory();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpGet("group-by-sales-range")]
        [AllowAnonymous]
        public IActionResult GroupBySalesRange()
        {
            var result = _salesService.GetSalesGroupedByRange();

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
    