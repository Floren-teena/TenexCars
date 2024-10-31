using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Enums;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;

namespace TenexCars.DataAccess.Repositories.Implementations
{
    public class OperatorRepository : IOperatorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OperatorRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Operator> AddOperatorAsync(Operator member)
        {
            var newlyAdded = await _context.Operators.AddAsync(member);
            await _context.SaveChangesAsync();

            return newlyAdded.Entity;
        }

        public async Task<Operator?> GetOperatorByUserId(string Id)
        {
            return await _context.Operators.FirstOrDefaultAsync(x => x.AppUserId == Id);
        }

        public async Task<Operator?> GetOperatorById(string Id)
        {
            return await _context.Operators.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> GetTotalNumberOfCars(string operatorId)
        {
            var cars = await _context.Vehicles.Where(v => v.OperatorId == operatorId).ToListAsync();
            return cars.Count();
        }

        public async Task<int> GetTotalNumberOfSubscribers(string operatorId)
        {
            return await _context.Subscriptions
                .Where(x => x.OperatorId == operatorId)
                .Select(x => x.SubscriberId)
                .Distinct()
                .CountAsync();
        }

        public async Task<int> GetTotalNumberOfReservedCars(string operatorId)
        {


            var vehicleIds = await _context.Vehicles
                                      .Where(v => v.OperatorId == operatorId)
                                      .Select(v => v.Id)
                                      .ToListAsync();

            return await _context.VehicleRequests
                                .CountAsync(vr => vehicleIds.Contains(vr.VehicleId!));
        }

        public async Task<int> GetTotalNumberOfActiveCars(string operatorId)
        {
            return await _context.Subscriptions
                .Where(x => x.OperatorId == operatorId && x.SubscriptionStatus == SubscriptionStatus.Active)
                .CountAsync();
        }

        public async Task<IEnumerable<OperatorMember>> GetAllMembersForOperatorAsync(string operatorId)
        {
            return await _context.OperatorMembers.Where(o => o.OperatorId == operatorId).ToListAsync();
        }

        public async Task<OperatorMember?> GetOperatorMemberByUserId(string Id)
        {
            return await _context.OperatorMembers.FirstOrDefaultAsync(x => x.AppUserId == Id);
        }
    }
}
