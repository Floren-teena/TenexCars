using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenexCars.DataAccess.Models;

namespace TenexCars.DataAccess.Repositories.Interfaces
{
    public interface IOperatorRepository
    {
        Task<Operator> AddOperatorAsync(Operator member);
        Task<Operator?> GetOperatorByUserId(string Id);
        Task<Operator?> GetOperatorById(string Id);
        Task<int> GetTotalNumberOfCars(string operatorId);
        Task<int> GetTotalNumberOfSubscribers(string operatorId);
        Task<int> GetTotalNumberOfReservedCars(string operatorId);
        Task<int> GetTotalNumberOfActiveCars(string operatorId);
    }
}
