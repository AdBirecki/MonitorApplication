using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MonitorApplication_Models.Interfaces;
using MonitorApplication_Models.OrderModel;
using MonitorApplication_Models.RareMinerals;
using MonitorApplication_Models.Units;
using MonitorApplication_Models.UserModels;

namespace MonitorApplication_USERS_DAL.Contexts
{
    public class OrdersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserOrders> UserOrders {get; set;}
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<AbstractOrder> AbstractOrders { get; set; }
        public DbSet<MineralPriceData> MineralPriceData { get; set; }

        public OrdersContext(DbContextOptions option) : base(option) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>(entity =>
            {
                entity
                    .HasKey(e => e.UserId);
                entity
                    .HasMany(e => e.UserOrders)
                    .WithOne(f => f.User);
            });

            modelBuilder.Entity<UserOrders>(entity =>
            {
                entity
                    .HasKey(e => e.OrderId);
                entity
                    .HasMany(e => e.PurchaseOrders)
                    .WithOne(f => f.UserOrders);
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity
                    .HasKey(e => e.PurchaseId);

                entity
                    .Property(p => p.Info)
                    .HasMaxLength(1024);
                    
                entity
                    .HasMany(e => e.OrderData)
                    .WithOne(f => f.PurchaseOrder);
            });

            modelBuilder.Entity<AbstractOrder>(entity =>
            {
                entity
                    .HasKey(e => e.OrderId);
                entity
                    .HasDiscriminator<CrudeMineralKind>("MineralType")
                    .HasValue<GoldOrder>(CrudeMineralKind.Gold)
                    .HasValue<SilverOrder>(CrudeMineralKind.Silver)
                    .HasValue<PalladiumOrder>(CrudeMineralKind.Palladium);
                entity
                    .HasOne(e => e.PurchaseOrder)
                    .WithMany(f => f.OrderData);
            });

            modelBuilder.Entity<GoldOrder>(entity => {
                entity.HasBaseType<AbstractOrder>();
            });

            modelBuilder.Entity<SilverOrder>(entity => {
                entity.HasBaseType<AbstractOrder>();
            });

            modelBuilder.Entity<PalladiumOrder>(entity => {
                entity.HasBaseType<AbstractOrder>();
            });

            modelBuilder.Entity<MineralPriceData>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity
                    .HasOne(e => e.Order)
                    .WithOne(f => f.PriceChange)
                    .HasForeignKey<AbstractOrder>(fk => fk.OrderId);
            });
        }
    }
}
