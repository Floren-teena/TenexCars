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
    }
}
