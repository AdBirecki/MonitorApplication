using Autofac;
using MonitorApplication_Models.UserModels;
using MonitorApplication_USERS_DAL.Contexts;
using MonitorApplication_USERS_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApplication_USERS_DAL.DbFacades
{
    public class OrdersDbFacade : IDisposable, IOrdersDbFacade
    {
        private readonly OrdersContext _orderContext;
        public OrdersDbFacade(OrdersContext ordersContext)
        {
            _orderContext = ordersContext;
        }

        public IQueryable<User> Users
        {
            get { return _orderContext.Users; }
        }

        public IQueryable<UserOrders> UserOrders {
            get { return _orderContext.UserOrders;}
        }

        public IQueryable<PurchaseOrder> PurchaseOrders {
            get { return _orderContext.PurchaseOrders; }
        }

        public int SaveChanges() {
           return _orderContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _orderContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _orderContext.Dispose();
        }
    }
}
