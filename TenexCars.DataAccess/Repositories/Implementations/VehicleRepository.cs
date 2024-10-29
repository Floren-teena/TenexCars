using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;

namespace TenexCars.DataAccess.Repositories.Implementations
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            var newVehicle = await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return newVehicle.Entity;
        }

        public async Task<Vehicle?> GetVehicleById(string Id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
