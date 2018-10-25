﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonitorApplication_Models.Units;
using MonitorApplication_USERS_DAL.Contexts;

namespace MonitorApplication_USERS_DAL.Migrations
{
    [DbContext(typeof(OrdersContext))]
    [Migration("20181025064129_AddOrderInfoField")]
    partial class AddOrderInfoField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MonitorApplication_Models.Interfaces.AbstractOrder", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("Currencies");

                    b.Property<int>("MineralType");

                    b.Property<double>("PricePerUnit");

                    b.Property<int?>("PurchaseOrderPurchaseId");

                    b.Property<int>("Quantity");

                    b.Property<int>("Units");

                    b.HasKey("OrderId");

                    b.HasIndex("PurchaseOrderPurchaseId");

                    b.ToTable("AbstractOrders");

                    b.HasDiscriminator<int>("MineralType");
                });

            modelBuilder.Entity("MonitorApplication_Models.OrderModel.MineralPriceData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Currency");

                    b.Property<double>("GoldChange");

                    b.Property<double>("GoldClose");

                    b.Property<double>("GoldPcExchange");

                    b.Property<double>("GoldPrice");

                    b.Property<double>("SilverChange");

                    b.Property<double>("SilverClose");

                    b.Property<double>("SilverPcExchange");

                    b.Property<double>("SilverPrice");

                    b.Property<DateTime>("dateTime");

                    b.Property<long>("timestamp");

                    b.HasKey("Id");

                    b.ToTable("MineralPriceData");
                });

            modelBuilder.Entity("MonitorApplication_Models.UserModels.PurchaseOrder", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Info")
                        .HasMaxLength(1024);

                    b.Property<long>("TimeStamp");

                    b.Property<int?>("UserOrdersOrderId");

                    b.Property<DateTime>("dateTime");

                    b.HasKey("PurchaseId");

                    b.HasIndex("UserOrdersOrderId");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("MonitorApplication_Models.UserModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Surname");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MonitorApplication_Models.UserModels.UserOrders", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OrderInfo")
                        .HasMaxLength(1024);

                    b.Property<int?>("UserId");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOrders");
                });

            modelBuilder.Entity("MonitorApplication_Models.OrderModel.PalladiumOrder", b =>
                {
                    b.HasBaseType("MonitorApplication_Models.Interfaces.AbstractOrder");

                    b.Property<double>("PalladiumFiness");

                    b.ToTable("PalladiumOrder");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("MonitorApplication_Models.RareMinerals.GoldOrder", b =>
                {
                    b.HasBaseType("MonitorApplication_Models.Interfaces.AbstractOrder");

                    b.Property<int>("Carat");

                    b.Property<double>("GoldFiness");

                    b.ToTable("GoldOrder");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("MonitorApplication_Models.RareMinerals.SilverOrder", b =>
                {
                    b.HasBaseType("MonitorApplication_Models.Interfaces.AbstractOrder");

                    b.Property<double>("GoldFiness")
                        .HasColumnName("SilverOrder_GoldFiness");

                    b.ToTable("SilverOrder");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("MonitorApplication_Models.Interfaces.AbstractOrder", b =>
                {
                    b.HasOne("MonitorApplication_Models.OrderModel.MineralPriceData", "PriceChange")
                        .WithOne("Order")
                        .HasForeignKey("MonitorApplication_Models.Interfaces.AbstractOrder", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MonitorApplication_Models.UserModels.PurchaseOrder", "PurchaseOrder")
                        .WithMany("OrderData")
                        .HasForeignKey("PurchaseOrderPurchaseId");
                });

            modelBuilder.Entity("MonitorApplication_Models.UserModels.PurchaseOrder", b =>
                {
                    b.HasOne("MonitorApplication_Models.UserModels.UserOrders", "UserOrders")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("UserOrdersOrderId");
                });

            modelBuilder.Entity("MonitorApplication_Models.UserModels.UserOrders", b =>
                {
                    b.HasOne("MonitorApplication_Models.UserModels.User", "User")
                        .WithMany("UserOrders")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
