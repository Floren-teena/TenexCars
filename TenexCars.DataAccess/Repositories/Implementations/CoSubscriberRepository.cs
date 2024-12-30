﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;
using TenexCars.DataAccess.Repositories.Interfaces;

namespace TenexCars.DataAccess.Repositories.Implementations
{
    public class CoSubscriberRepository : ICoSubscriberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CoSubscriberRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Co_SubscriberInvitee> AddInvitee(Co_SubscriberInvitee invitee)
        {
            var newInvitee = await _dbContext.Co_SubscriberInvitees.AddAsync(invitee);
            await _dbContext.SaveChangesAsync();
            return newInvitee.Entity;
        }

        public async Task<Co_SubscriberInvitee?> GetCoSubscriberByUserId(string userId)
        {
            return await _dbContext.Co_SubscriberInvitees.FirstOrDefaultAsync(c => c.AppUserId == userId);
        }

        public async Task<Co_SubscriberInvitee> UpdateInvitee(Co_SubscriberInvitee invitee)
        {
            var isSubscriber = await _dbContext.Co_SubscriberInvitees.FindAsync(invitee.Id);

            if (isSubscriber != null)
            {
                _dbContext.Co_SubscriberInvitees.Update(isSubscriber);
                await _dbContext.SaveChangesAsync();
                return isSubscriber;
            }
            else
            {
                return null!;
            }
        }
    }
}