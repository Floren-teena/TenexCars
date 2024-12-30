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

        public async Task<Subscriber?> GetSubscriberByIdAsync(string Id)
        {
            return await _context.Subscribers.FirstOrDefaultAsync(s => s.Id == Id);

        }

        public async Task<Subscriber> AddSubscriberAsync(Subscriber subscriber)
        {
            var newSubscriber = await _context.Subscribers.AddAsync(subscriber);
            await _context.SaveChangesAsync();
            return newSubscriber.Entity;
        }

        public async Task<Subscriber> UpdateSubscriberAsync(Subscriber subscriber)
        {
            var isSubscriber = await _context.Subscribers.FindAsync(subscriber.Id);

            if (isSubscriber != null)
            {
                _context.Subscribers.Update(isSubscriber);
                await _context.SaveChangesAsync();
                return isSubscriber;
            }
            else
            {
                return null!;
            }
        }

        public int CalculateAge(DateOnly birthDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Today); // Get today's date as DateOnly
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--; // Adjust for the current year's birth date not yet reached
            return age;
        }

    }
}
