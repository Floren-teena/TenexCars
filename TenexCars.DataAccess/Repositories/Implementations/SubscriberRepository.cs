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
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriberRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<Subscriber?> GetSubscriberByUserId(string id)
        {
            return await _context.Subscribers.FirstOrDefaultAsync(s => s.AppUserId == id);
        }

        public async Task<Subscriber> AddSubscriberAsync(Subscriber subscriber)
        {
            var newSubscriber = await _context.Subscribers.AddAsync(subscriber);
            await _context.SaveChangesAsync();
            return newSubscriber.Entity;
        }
    }
}
