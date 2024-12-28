using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.ViewModels;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<Vehicle?> GetVehicleById(string Id);
        Task<IEnumerable<Vehicle>> GetAllVehicles();
        Task<List<Vehicle>> GetAllInActiveVehicles(IEnumerable<Vehicle> vehicles);
        Task<IEnumerable<Vehicle>> GetAll(QueryObject query);
        Task<List<Vehicle>> GetVehiclesByOperator(string operatorId);
        void UpdateVehicle(Vehicle vehicle);
        bool VehicleExists(string id);
    }
}
