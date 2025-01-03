﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> SetInitialPasswordAsync(string userId, string newPassword);
    }
}
