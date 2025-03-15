namespace project_management_v1.Application.DTOs.SalesAggregator
{
    public class SalesRecordDTo
    {
        public long Id { get; set; }
        public string? DateTime { get; set; }
        public long ChannelCode { get; set; }
        public  string? TransactionId { get; set; }
        public long LineItemId { get; set; }
        public double Volume { get; set; }
        public double SalesIncVatActual { get; set; }
        public double PriceIncVat { get; set; }
        public double PriceIncVatOriginal { get; set; }
        public long UpcCode { get; set; } 
        public  string? UpcName { get; set; }
        public  string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? BrandName { get; set; }
        public string? PackageName { get; set; }
        public string? SupplierName { get; set; }
        public long NumberOfTransactions { get; set; }
        public string? Gender { get; set; }
        public string? CustomerSegment { get; set; }
        public long TerminalCheckRegister { get; set; }
        public long ZipCode { get; set; }
        public string? ZipCodeExtend { get; set; }
        public string? ZipCodeTotal { get; set; }
        public string? Street { get; set; }
        public long NumOfEmployees { get; set; }
        public long M2 { get; set; }
        public string? StoreSegment { get; set; }
        public string? Region { get; set; }
        public double MarginOctober { get; set; }
        public double PriceIncVatOctober { get; set; }
        public double DiscountOctober { get; set; }
    }
}
