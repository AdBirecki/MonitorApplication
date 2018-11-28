using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MonitorApplication_Models.FileModels;
using MonitorApplication_Models.Interfaces;
using MonitorApplication_Models.OrderModel;
using MonitorApplication_Models.PicturesModels;
using MonitorApplication_Models.RareMinerals;
using MonitorApplication_Models.Units;
using MonitorApplication_Models.UserModels;

namespace MonitorApplication_USERS_DAL.Contexts
{
    public class OrdersDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserOrders> UserOrders {get; set;}
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<AbstractOrder> AbstractOrders { get; set; }
        public virtual DbSet<MineralPriceData> MineralPriceData { get; set; }

        public OrdersDbContext() {
        }

        public OrdersDbContext(DbContextOptions option) : base(option) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<User>(entity =>
            {
            entity
                .HasKey(e =>  e.UserId);

            entity
                .HasIndex(u => u.Username)
                .IsUnique();

            entity
                    .HasMany(e => e.UserOrders)
                    .WithOne(f => f.User);
            });

            #region files many to many relationship
            modelBuilder.Entity<UserAppFile>(entity => {
                entity.HasKey(e => new { e.FileId , e.UserId});

                entity
                    .HasOne(fk => fk.User)
                    .WithMany(u => u.UserAppFiles)
                    .HasForeignKey(fk => fk.UserId);

                entity
                    .HasOne(fk => fk.UploadedFile)
                    .WithMany(f => f.UserAppFiles)
                    .HasForeignKey(fk => fk.FileId);
            });

            modelBuilder.Entity<AppFile>(entity =>
            {
                entity.HasKey(e => e.AppFileId);
            });
            #endregion

            modelBuilder.Entity<UserOrders>(entity =>
            {
                entity
                    .HasKey(e => e.OrderId);
                entity
                    .HasMany(e => e.PurchaseOrders)
                    .WithOne(f => f.UserOrders);


                entity.Property(e => e.OrderInfo).HasMaxLength(1024);
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
