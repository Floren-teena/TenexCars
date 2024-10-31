namespace TenexCars.Models.ViewModels
{
    public class OperatorDashboardViewModel
    {
        public string OperatorId { get; set; } = null!;
        public string OperatorLogo { get; set; } = null!;
        public string? CompanyLogo { get; set; }
        public string OperatorImage { get; set; } = null!;
        public int TotalNumberOfVehicles { get; set; }
        public int TotalNumberOfSubscribers { get; set; }
        public int TotalNumberOfActiveCars { get; set; }
        public int TotalNumberOfReservedCars { get; set; }
        public List<int> CurrentMonthStats { get; set; } = null!;
        public List<int> PastMonthStats { get; set; } = null!;
    }
}