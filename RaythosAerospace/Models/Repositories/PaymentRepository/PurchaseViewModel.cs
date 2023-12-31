using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.PaymentRepository
{
    public class PurchaseViewModel
    {

        public PurchaseViewModel()
        {
            Products = new List<Product>();
        }
        public string UserId { get; set; }
        public List<Product> Products { get; set; }
        public int totalPrice { get; set; }

        public int Customizations { get; set; }

        public string shippingId { get; set; }

        public string shippingAddress { get; set; }

        public string DeliveryType { get; set; }
    }

   public enum Delivery
    {
        TransportByShipping,
        FlightDelivery
    }

    public class Product
    {
        public string ProductID { get; set; }

        public string AirCraftId { get; set; }
        public int Count { get; set; }

        public int UnitPrice { get; set; }
    }
}
