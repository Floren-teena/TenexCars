﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
    public interface ISubscriberRepository
    {
        Task<Subscriber?> GetSubscriberByUserId(string id);
        Task<Subscriber> AddSubscriberAsync(Subscriber subscriber);
        Task<Subscriber?> GetSubscriberByIdAsync(string Id);
        Task<Subscriber> UpdateSubscriberAsync(Subscriber subscriber);
        Subscriber? GetSubscriberById(string subscriberId);
        List<Subscription> GetSubscriptionsBySubscriberId(string subscriberId);
        int CalculateAge(DateOnly birthDate);
    }
}
