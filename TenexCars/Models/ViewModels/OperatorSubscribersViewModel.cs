using TenexCars.DataAccess.Models;

namespace TenexCars.Models.ViewModels
{
    public class OperatorSubscribersViewModel
    {
        public Operator? Operator { get; set; }
        public IEnumerable<Subscription>? Subscriptions { get; set; }
    }
}
