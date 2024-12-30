using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<Subscription> AddSubscriptionAsync(Subscription subscription);
        Task<Subscription?> GetSubscriptionForVehicle(string vehicleId);
        Task<Subscription?> GetSubscriptionBySubcriber(string Id);
        Task<Subscription> UpdateSubscription(Subscription getExistingSubscription);
        Task<Subscription> GetSubscriptionForOperator(string operatorId);
        Task<List<Subscription>> GetAllSubscriptionsForOperator(string operatorId);
    }
}
