using Microsoft.AspNetCore.Identity;
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

    }
}
