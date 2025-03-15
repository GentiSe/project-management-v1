using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_management_v1.Infrastructure.Services;

namespace project_management_v1.Controllers
{
    [Route("api/v1/sales")]
    [ApiController]
    public class SalesAggregationController(ISalesDataService _salesService) : ControllerBase
    {

        [HttpGet("grouped-by-category")]
        [AllowAnonymous]
        public IActionResult GetGroupedByCategory()
        {
            var res = _salesService.GetGroupedDataByCategoryAndBrand();
            return Ok(res);
        }

        [HttpGet("top-sold-by-category")]
        [AllowAnonymous]
        public IActionResult GetTopSoldByCategory()
        {
            var result = _salesService.TopSoldByCategory();

            if (result == null) return BadRequest("Failed loading sales data from file.");

            return Ok(result);
        }

        [HttpGet("group-by-sales-range")]
        [AllowAnonymous]
        public IActionResult GroupBySalesRange()
        {
            var result = _salesService.GroupBySalesRange();

            if (result == null) return BadRequest("Failed loading sales data from file.");

            return Ok(result);
        }
    }
}
    