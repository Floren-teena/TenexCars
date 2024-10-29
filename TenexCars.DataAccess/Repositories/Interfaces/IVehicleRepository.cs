using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<Vehicle?> GetVehicleById(string Id);
    }
}
