using MonitorApplication_Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApplication_USERS_DAL.Interfaces
{
    public interface IOrdersDbFacade
    {
        IQueryable<User> Users { get; } 

        IQueryable<UserOrders> UserOrders { get; }

        IQueryable<PurchaseOrder> PurchaseOrders { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

    }
}
