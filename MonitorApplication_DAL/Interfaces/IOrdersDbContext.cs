using Microsoft.EntityFrameworkCore;
using MonitorApplication_Models.Interfaces;
using MonitorApplication_Models.OrderModel;
using MonitorApplication_Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorApplication_DAL.Interfaces
{
    public interface IOrdersDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserOrders> UserOrders { get; set; }
        DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        DbSet<AbstractOrder> AbstractOrders { get; set; }
        DbSet<MineralPriceData> MineralPriceData { get; set; }
    }
}
