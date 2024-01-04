﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RaythosAerospace.Models.Repositories.AdminRepository;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
using RaythosAerospace.Models.Repositories.InvoiceRepository;
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.ProductRepository;
using RaythosAerospace.Models.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories
{
    public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }


        //tables that has been created
        public DbSet<AirCraftModel> AirCrafts { get; set; }
        // DbSet<UserModel> Users { get; set; }

        public DbSet<EngineModel> Engines { get; set; }

        public DbSet<SeatModel> Seats { get; set; }

        public DbSet<InvoiceModel> Invoices { get; set; }

        public DbSet<CartModel> Carts { get; set; }

        public DbSet<CartItemModel> CartItems { get; set; }

        public DbSet<OrderModel> Orders { get; set; }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ShippingModel> Shippings { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<AdminModel> Admins { get; set; }

        public DbSet<AirCraftPhoto> AirCraftPhoto { get; set; }

        public DbSet<ColorModel> Colors { get; set; }

        public DbSet<CustomizationModel> Customization { get; set; }



        //populating data when creation of the table 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //////////////////// building relationships /////////////////////////
            /////////////////// air craft table ////////////////////////////////
            modelBuilder.Entity<AirCraftModel>()
                .HasOne(e=> e.EngineType)
                .WithMany(a => a.AirCraftModels)
                .HasForeignKey(f => f.EngineId);

            //one seat type refering to many aircrafts || this creation is one-to-many
            modelBuilder.Entity<AirCraftModel>()
                .HasOne(s => s.Seat)
                .WithMany(a => a.AirCraftModels)
                .HasForeignKey(f => f.SeatID);

            modelBuilder.Entity<AirCraftPhoto>()
               .HasOne(e => e.airCraft)
               .WithMany(a => a.Photos)
               .HasForeignKey(f => f.AirCraftID);

            /////////////////// customization table ////////////////////////////////
            modelBuilder.Entity<CustomizationModel>()
                 .HasOne(s => s.Seat)
                 .WithMany(a => a.customize)
                 .HasForeignKey(f => f.SeatId);

            modelBuilder.Entity<CustomizationModel>()
                 .HasOne(s => s.Engine)
                 .WithMany(a => a.customize)
                 .HasForeignKey(f => f.EngineId);


            /////////////////// product table ////////////////////////////////
            modelBuilder.Entity<ProductModel>()
                .HasOne(u => u.User)
                .WithMany(p => p.Products)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<ProductModel>()
                .HasOne(u => u.Cart)
                .WithMany(p => p.Products)
                .HasForeignKey(f => f.CartId);

            modelBuilder.Entity<ProductModel>()
               .HasOne(u => u.Order)
               .WithMany(p => p.Products)
               .HasForeignKey(f => f.OrderId);

            modelBuilder.Entity<ProductModel>()
              .HasOne(a => a.AirCraft)
              .WithMany(p => p.Products)
              .HasForeignKey(o => o.AirCraftId);

            modelBuilder.Entity<ProductModel>()
               .HasOne(o => o.Customize)
               .WithOne(i => i.Product)
               .HasForeignKey<ProductModel>(o => o.CustomizationId);

            ////////////////////////// Customization table ///////////////////////


            ////////////////////////// OrderAircraft table ///////////////////////

            //defining composite key || this creation is many-to-many
            modelBuilder.Entity<OrderAircraftModel>()
                .HasKey(pk => new { pk.AirCraftId, pk.OrderId });

            modelBuilder.Entity<OrderAircraftModel>()
                .HasOne(o => o.Order)
                .WithMany(p => p.OrderAirCraft)
                .HasForeignKey(f => f.OrderId);

            modelBuilder.Entity<OrderAircraftModel>()
                .HasOne(o => o.AirCraft)
                .WithMany(p => p.OrderAirCraft)
                .HasForeignKey(f => f.AirCraftId);

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.User)
                .WithMany(p => p.Orders)
                .HasForeignKey(f => f.UserId);


            /////////////////////////////////////////////////////////
            /////////////////////// invoice table //////////////////

            //only one invoice has a reference to a single order
            modelBuilder.Entity<InvoiceModel>()
               .HasOne(o => o.Order)
               .WithOne(i => i.Invoice)
               .HasForeignKey<InvoiceModel>(o => o.OrderId);



            ////////////////////////////////////////////////////////////////////

            /////////////////// cart item table //////////////////////////////
            modelBuilder.Entity<CartItemModel>()
            .HasKey(e => e.CartItemId)
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<CartItemModel>()
              .HasOne(o => o.Cart)
              .WithMany(i => i.CartItems)
              .HasForeignKey(o => o.CartId);

            modelBuilder.Entity<CartItemModel>()
             .HasOne(o => o.AirCraft)
             .WithMany(i => i.CartItems)
             .HasForeignKey(o => o.AirCraftId);

            //////////////////// cart table ////////////////////////////////

            modelBuilder.Entity<CartModel>()
              .HasOne(o => o.User)
              .WithOne(i => i.Cart)
              .HasForeignKey<CartModel>(o => o.UseId);

            ////////////////////// shipping table //////////////////////////

            modelBuilder.Entity<ShippingModel>()
              .HasMany(o => o.Orders)
              .WithOne(i => i.Shipping)
              .HasForeignKey(o => o.ShippingId);

            /////////////////  populate data ///////////////////////////////////
            string[] engineTypes = {
                "Turbojet",
                "Turbofan",
                "Turboprop",
                "Turboshaft",
                "Piston",
                "Ramjet",
                "HybridElectric",
                "Other"
            };

            string[] seatTypes =
            {
                "Economy",
                "PremiumEconomy",
                "Business",
                "FirstClass",
                "Other"
            };

            // shipping

            string[] shipTypes =
            {
                "Standard Shipping",
                "Expedited Shipping",
                "Express Shipping",
                "Free Shipping"
            };

            string[] shipdesc =
            {
                "most basic and typically the least expensive shipping method.",
                "Faster than standard shipping, this method ensures quicker delivery",
                "The fastest shipping method available, promising delivery with the specified time",
                "hipping cost is covered by the seller instead of the buyer. It might be standard shipping or occasionally expedited, but the cost is absorbed by the seller"

            };

            string[] paints =
            {

                "LIGHT GREEN",
                "LIGHT BLUE",
                "LIGHT PURPLE",
                "LIGHT PINK",
                "LIGHT GREEN",
                "BLACK",
                "RED",
            };

            ////////////////////////

            ShippingModel[] shipping = new ShippingModel[4];
            EngineModel[] engines = new EngineModel[8];
            SeatModel[] seats = new SeatModel[5];
            ColorModel[] colors = new ColorModel[paints.Length];
            AirCraftModel[] airCrafts = new AirCraftModel[5];




            for (int i = 0; i < engines.Length; i++)
            {
                EngineModel current = new EngineModel
                {
                    EngineId = "E000" + i.ToString(),
                    EngineType = engineTypes[i],
                    UnitPrice = 1000,
                    UnitCount = 6

                };

                engines[i] = current;
            }

            for(int i = 0; i < seats.Length; i++)
            {
                SeatModel current = new SeatModel
                {
                    SeatCount = 10,
                    SeatID = "S000" + i.ToString(),
                    SeatType = seatTypes[i],
                    UnitPrice = 200
                };

                seats[i] = current;
            }

            for (int i = 0; i < paints.Length; i++)
            {
                ColorModel current = new ColorModel
                {
                    availability = true,
                    Price = 3,
                    ColorId = "C000" + i.ToString(),
                    Color=paints[i]
                };

                colors[i] = current;
            }

            for (int i = 0; i < shipping.Length; i++)
            {
                ShippingModel current = new ShippingModel
                {
                    ShippingId = "SHIP00" + i.ToString(),
                    ShippingDesc = shipdesc[i],
                    ShippingType = shipTypes[i]
                };

                shipping[i] = current;
            }

            ////// populating 
            modelBuilder.Entity<EngineModel>()
                .HasData(engines);
            modelBuilder.Entity<SeatModel>()
                .HasData(seats);
            modelBuilder.Entity<ShippingModel>()
                .HasData(shipping);
            modelBuilder.Entity<ColorModel>()
                .HasData(colors);

        }

       
    }
}
