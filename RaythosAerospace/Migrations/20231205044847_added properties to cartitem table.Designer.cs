﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RaythosAerospace.Models.Repositories;

namespace RaythosAerospace.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20231205044847_added properties to cartitem table")]
    partial class addedpropertiestocartitemtable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.AirCraftRepository.AirCraftModel", b =>
                {
                    b.Property<string>("AircraftId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("AirCraftPrice")
                        .HasColumnType("float");

                    b.Property<string>("AircraftType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EngineId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("FuelCapacity")
                        .HasColumnType("float");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<int>("ItemCount")
                        .HasColumnType("int");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<DateTime>("ManfacturedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxAllowedEngines")
                        .HasColumnType("int");

                    b.Property<int>("MaxSeatesAllowed")
                        .HasColumnType("int");

                    b.Property<int>("MaxWingSpan")
                        .HasColumnType("int");

                    b.Property<double>("MaximumRange")
                        .HasColumnType("float");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeatID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SeatingCapacity")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.Property<int>("YearOfManufacture")
                        .HasColumnType("int");

                    b.HasKey("AircraftId");

                    b.HasIndex("EngineId");

                    b.HasIndex("SeatID");

                    b.ToTable("AirCrafts");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.AirCraftRepository.EngineModel", b =>
                {
                    b.Property<string>("EngineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EngineType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitCount")
                        .HasColumnType("int");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.HasKey("EngineId");

                    b.ToTable("Engines");

                    b.HasData(
                        new
                        {
                            EngineId = "E0000",
                            EngineType = "Turbojet",
                            UnitCount = 6,
                            UnitPrice = 1000
                        },
                        new
                        {
                            EngineId = "E0001",
                            EngineType = "Turbofan",
                            UnitCount = 6,
                            UnitPrice = 1000
                        },
                        new
                        {
                            EngineId = "E0002",
                            EngineType = "Turboprop",
                            UnitCount = 6,
                            UnitPrice = 1000
                        },
                        new
                        {
                            EngineId = "E0003",
                            EngineType = "Turboshaft",
                            UnitCount = 6,
                            UnitPrice = 1000
                        },
                        new
                        {
                            EngineId = "E0004",
                            EngineType = "Piston",
                            UnitCount = 6,
                            UnitPrice = 1000
                        },
                        new
                        {
                            EngineId = "E0005",
                            EngineType = "Ramjet",
                            UnitCount = 6,
                            UnitPrice = 1000
                        },
                        new
                        {
                            EngineId = "E0006",
                            EngineType = "HybridElectric",
                            UnitCount = 6,
                            UnitPrice = 1000
                        },
                        new
                        {
                            EngineId = "E0007",
                            EngineType = "Other",
                            UnitCount = 6,
                            UnitPrice = 1000
                        });
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.AirCraftRepository.SeatModel", b =>
                {
                    b.Property<string>("SeatID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SeatCount")
                        .HasColumnType("int");

                    b.Property<string>("SeatType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.HasKey("SeatID");

                    b.ToTable("Seats");

                    b.HasData(
                        new
                        {
                            SeatID = "S0000",
                            SeatCount = 10,
                            SeatType = "Economy",
                            UnitPrice = 200
                        },
                        new
                        {
                            SeatID = "S0001",
                            SeatCount = 10,
                            SeatType = "PremiumEconomy",
                            UnitPrice = 200
                        },
                        new
                        {
                            SeatID = "S0002",
                            SeatCount = 10,
                            SeatType = "Business",
                            UnitPrice = 200
                        },
                        new
                        {
                            SeatID = "S0003",
                            SeatCount = 10,
                            SeatType = "FirstClass",
                            UnitPrice = 200
                        },
                        new
                        {
                            SeatID = "S0004",
                            SeatCount = 10,
                            SeatType = "Other",
                            UnitPrice = 200
                        });
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.CartRepository.CartItemModel", b =>
                {
                    b.Property<string>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AirCraftId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CartId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ItemAddedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("CartItemId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasIndex("AirCraftId");

                    b.HasIndex("CartId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.CartRepository.CartModel", b =>
                {
                    b.Property<string>("CartNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UseId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CartNumber");

                    b.HasIndex("UseId")
                        .IsUnique()
                        .HasFilter("[UseId] IS NOT NULL");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.InvoiceRepository.InvoiceModel", b =>
                {
                    b.Property<string>("InvoiceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BillingAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Discounts")
                        .HasColumnType("float");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Subtotal")
                        .HasColumnType("float");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId")
                        .IsUnique()
                        .HasFilter("[OrderId] IS NOT NULL");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.OrderRepository.OrderAircraftModel", b =>
                {
                    b.Property<string>("AirCraftId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AirCraftId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderAircraftModel");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.OrderRepository.OrderModel", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AirCraftId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Discounts")
                        .HasColumnType("float");

                    b.Property<DateTime>("EstimatedDeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ShippingCost")
                        .HasColumnType("float");

                    b.Property<string>("ShippingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShippingMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Subtotal")
                        .HasColumnType("float");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OrderId");

                    b.HasIndex("ShippingId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.OrderRepository.ShippingModel", b =>
                {
                    b.Property<string>("ShippingId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ShippingCost")
                        .HasColumnType("int");

                    b.Property<string>("ShippingDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShippingType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ShippingId");

                    b.ToTable("Shippings");

                    b.HasData(
                        new
                        {
                            ShippingId = "SHIP000",
                            ShippingCost = 0,
                            ShippingDesc = "most basic and typically the least expensive shipping method.",
                            ShippingType = "Standard Shipping"
                        },
                        new
                        {
                            ShippingId = "SHIP001",
                            ShippingCost = 0,
                            ShippingDesc = "Faster than standard shipping, this method ensures quicker delivery",
                            ShippingType = "Expedited Shipping"
                        },
                        new
                        {
                            ShippingId = "SHIP002",
                            ShippingCost = 0,
                            ShippingDesc = "The fastest shipping method available, promising delivery with the specified time",
                            ShippingType = "Express Shipping"
                        },
                        new
                        {
                            ShippingId = "SHIP003",
                            ShippingCost = 0,
                            ShippingDesc = "hipping cost is covered by the seller instead of the buyer. It might be standard shipping or occasionally expedited, but the cost is absorbed by the seller",
                            ShippingType = "Free Shipping"
                        });
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.UserRepository.UserModel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.AirCraftRepository.AirCraftModel", b =>
                {
                    b.HasOne("RaythosAerospace.Models.Repositories.AirCraftRepository.EngineModel", "EngineType")
                        .WithMany("AirCraftModels")
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RaythosAerospace.Models.Repositories.AirCraftRepository.SeatModel", "Seat")
                        .WithMany("AirCraftModels")
                        .HasForeignKey("SeatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EngineType");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.CartRepository.CartItemModel", b =>
                {
                    b.HasOne("RaythosAerospace.Models.Repositories.AirCraftRepository.AirCraftModel", "AirCraft")
                        .WithMany("CartItems")
                        .HasForeignKey("AirCraftId");

                    b.HasOne("RaythosAerospace.Models.Repositories.CartRepository.CartModel", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartId");

                    b.Navigation("AirCraft");

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.CartRepository.CartModel", b =>
                {
                    b.HasOne("RaythosAerospace.Models.Repositories.UserRepository.UserModel", "User")
                        .WithOne("Cart")
                        .HasForeignKey("RaythosAerospace.Models.Repositories.CartRepository.CartModel", "UseId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.InvoiceRepository.InvoiceModel", b =>
                {
                    b.HasOne("RaythosAerospace.Models.Repositories.UserRepository.UserModel", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("RaythosAerospace.Models.Repositories.OrderRepository.OrderModel", "Order")
                        .WithOne("Invoice")
                        .HasForeignKey("RaythosAerospace.Models.Repositories.InvoiceRepository.InvoiceModel", "OrderId");

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.OrderRepository.OrderAircraftModel", b =>
                {
                    b.HasOne("RaythosAerospace.Models.Repositories.AirCraftRepository.AirCraftModel", "AirCraft")
                        .WithMany("OrderAirCraft")
                        .HasForeignKey("AirCraftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RaythosAerospace.Models.Repositories.OrderRepository.OrderModel", "Order")
                        .WithMany("OrderAirCraft")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AirCraft");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.OrderRepository.OrderModel", b =>
                {
                    b.HasOne("RaythosAerospace.Models.Repositories.OrderRepository.ShippingModel", "Shipping")
                        .WithMany("Orders")
                        .HasForeignKey("ShippingId");

                    b.HasOne("RaythosAerospace.Models.Repositories.UserRepository.UserModel", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shipping");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.AirCraftRepository.AirCraftModel", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("OrderAirCraft");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.AirCraftRepository.EngineModel", b =>
                {
                    b.Navigation("AirCraftModels");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.AirCraftRepository.SeatModel", b =>
                {
                    b.Navigation("AirCraftModels");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.CartRepository.CartModel", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.OrderRepository.OrderModel", b =>
                {
                    b.Navigation("Invoice");

                    b.Navigation("OrderAirCraft");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.OrderRepository.ShippingModel", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RaythosAerospace.Models.Repositories.UserRepository.UserModel", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
