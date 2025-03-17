using Microsoft.EntityFrameworkCore;
using project_management_v1.Application.DTOs;
using project_management_v1.Application.DTOs.ChartModels;
using project_management_v1.Application.DTOs.SalesAggregator;
using System.Text.Json;

namespace project_management_v1.Services
{
    public class SalesDataService(IWebHostEnvironment _env
        , IJsonSerializerService jsonService) : ISalesDataService
    {
        public Result<ChartResponse> GetGroupedDataByCategoryAndBrand()
        {
            try
            {
                var records = LoadSalesDataFromFile();
                if (records.Count == 0)
                {
                    return Result<ChartResponse>.Failure("Couldn't load data.");
                }

                var groupedData = records
                    .GroupBy(p => new { p.CategoryName, p.BrandName })
                    .Select(g => new
                    {
                        g.Key.CategoryName,
                        g.Key.BrandName,
                        TotalSales = g.Sum(p => p.SalesIncVatActual)
                    })
                    .OrderBy(p => p.CategoryName) // Order by categoryName ascending
                    .ToList();

                // select all categories distinct.
                var xAxisResult = groupedData
                    .Select(x => x.CategoryName)
                    .Distinct()
                    .ToList();

                var seriesData = groupedData
                     .GroupBy(g => g.BrandName)
                     .Select(g => new SerieData
                     {
                         Type = "bar",
                         Name = g.Key,  // BrandName
                         Data = xAxisResult.Select(category => new Datum
                         {
                             Value = Math.Round(
                                 g.Where(x => x.CategoryName == category)
                                  .Sum(x => x.TotalSales), 2),
                             Name = category
                         }).ToList()
                     })
                     .ToList();

                var response = new ChartResponse
                {
                    XAxis = new AxisData { Type = "category", Data = xAxisResult },
                    YAxis = new AxisData { Type = "value" },
                    Series = seriesData
                };
                return Result<ChartResponse>.Success(response);

            }
            catch (Exception e)
            {
                // Can add logging here...
                return Result<ChartResponse>.Failure($"Error while fetching sales data by category and brand: {e.Message}");
            }

            // Note: I had some confusion regarding the responses provided for Task 1.
            // The xAxis data seemed to have duplicates, which could cause mismatches when aligning it with the series data.
            // but please review to ensure it's in line with expectations.
            // This solution might not be fully accurate, and I may have overlooked something.
        }

        public Result<SerieData?> GetTopSoldProductsByCategory()
        {
            try
            {
                var records = LoadSalesDataFromFile();

                if (records.Count == 0)
                {
                    return Result<SerieData?>.Failure("Error loading data.");
                }

                var groupedData = records
                    .GroupBy(x => x.CategoryName)
                    .Select(g => new
                    {
                        g.Key,
                        TotalSales = g.Sum(x => x.SalesIncVatActual)
                    })
                    .OrderByDescending(x => x.TotalSales)
                    .Take(4)
                    .ToList();

                var totalSales = groupedData.Sum(x => x.TotalSales); // get the total of sales.

                var serieData = new SerieData
                {
                    Type = "pie",
                    Data = groupedData.Select(g => new Datum
                    {
                        Value = g.TotalSales,
                        Name = g.Key,
                        ItemStyle = new ItemStyle
                        {
                            Normal = new ItemStyle.ColorModel
                            {
                                Color = GetColorBasedOnAlpha(g.TotalSales / totalSales)
                            },
                            Emphasis = new ItemStyle.ColorModel
                            {
                                Color = GetColorBasedOnAlpha(Math.Min(1, g.TotalSales / totalSales + 0.2))
                            }
                        }
                    }).ToList()
                };

                return Result<SerieData?>.Success(serieData);
            }
            catch (Exception e)
            {
                // we can add logging here...
                return Result<SerieData?>.Failure($"Error while fetching top products by category: {e.Message}");
            }
        }

        private List<SalesRecordDTo> LoadSalesDataFromFile()
        {
            var filePath = Path.Combine(_env.ContentRootPath, "sales_dummy_data.json");
            var content = File.ReadAllText(filePath);
            var salesData = jsonService.Deserialize<List<SalesRecordDTo>>(content);
            return salesData ?? [];
        }
        private string GetColorBasedOnAlpha(double percentage)
        {
            double alpha = Math.Max(0.2, percentage);
            return $"rgb(60, 185, 226, {alpha})";
        }

        public Result<SerieData?> GetSalesGroupedByRange()
        {
            try
            {
                var records = LoadSalesDataFromFile();

                if (records == null || records.Count == 0)
                {
                    return Result<SerieData?>.Failure("Couldn't load records");
                }

                var ranges = GetRanges();

                var salesByRange = ranges.Select(range => new
                {
                    Range = range.Label,
                    TotalSales = records
                          .Where(x => x.SalesIncVatActual >= range.Min 
                             && x.SalesIncVatActual <= range.Max)
                        .Sum(x => x.SalesIncVatActual)
                }).ToList();

                var serieData = new SerieData
                {
                    Type = "treemap",
                    Data = salesByRange.Select(range => new Datum
                    {
                        Value = range.TotalSales,
                        Name = range.Range
                    }).ToList()
                };

                return Result<SerieData?>.Success(serieData);
            }
            catch (Exception e)
            {
                // We can add logging here...
                return Result<SerieData?>.Failure($"Error while fetching Sales by group range: {e.Message}");
            }
        }

        private List<(double Min, double Max, string Label)> GetRanges()
        {
            var ranges = new List<(double Min, double Max, string Label)>
            {
                (0,10,"0-10"),
                (10,100,"10-100"),
                (100,double.MaxValue, "100+")
            };

            return ranges;
        }
    }
}
