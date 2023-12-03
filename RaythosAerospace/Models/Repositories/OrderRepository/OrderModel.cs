using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RaythosAerospace.Models.Repositories.UserRepository;
using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.InvoiceRepository;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public class OrderModel
    {
        [Key]
        public string OrderId { get; set; }

        [Required]
        public DateTime OrderDateTime { get; set; }

        // Customer Information
        public UserModel User { get; set; }

        [Required]
        public string UserId { get; set; }

        public string CustomerEmail { get; set; }

        // Addresses
        [Required]
        public string ShippingAddress { get; set; }

        public string AirCraftId { get; set; }


        // Payment Information
        [Required]
        public string PaymentMethod { get; set; }

        // Shipping Information
        [Required]
        public string ShippingMethod { get; set; }

        public string ShippingId { get; set; }
        public ShippingModel Shipping { get; set; }

        public double ShippingCost { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        // Order Summary
        [Required]
        public double Subtotal { get; set; }


        public double Discounts { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        // Order Status
        [Required]
        public string OrderStatus { get; set; }

        //navigation properties
        public ICollection<OrderAircraftModel> OrderAirCraft { get; set; }

        public InvoiceModel Invoice { get; set; }


    }
}
