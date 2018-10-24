using MonitorApplication_Models.UserModels;
using MonitorApplication_USERS_DAL.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MonitorApplication_USERS_DAL.DbFacades
{
    public class OrdersDbFacade : IDisposable
    {
        private readonly OrdersContext _orderContext;
        private readonly Autofac.IContainer _container;
        public OrdersDbFacade(OrdersContext ordersContext, Autofac.IContainer container)
        {
            _orderContext = ordersContext;
            _container = container;
        }

        public IQueryable<User> User {
            get { return _orderContext.Users; }
        }

        public IQueryable<UserOrders> UserOrders {
            get { return _orderContext.UserOrders;}
        }

        public IQueryable<PurchaseOrder> PurchaseOrders {
            get { return _orderContext.PurchaseOrders; }
        }



        public void Dispose()
        {
            _orderContext.Dispose();
        }
    }
}
