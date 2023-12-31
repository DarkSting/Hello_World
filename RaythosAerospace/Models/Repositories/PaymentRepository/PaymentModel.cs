using RaythosAerospace.Models.Repositories.AirCraftRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
using RaythosAerospace.Models.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.PaymentRepository
{
    public class PaymentModel
    {
        public double Amount { get; set; }
        public string Token { get; set; }

        [Display(Name = "Shipping Type")]
        [Required(ErrorMessage = "Shipping type is required")]
        public string shippingId { get; set; }

        [Required(ErrorMessage ="Shipping address is required")]
        [Display(Name = "Shipping Address")]
        public string shippingAddress { get; set; }

        public string DeliveryType { get; set; }

        public string UserId { get; set; }

        public Dictionary<string,bool> HasErrors { get; set; }
        public IEnumerable<ProductModel> cartitems { get; set; }

        public Dictionary<string, CheckoutModel> aircrafts { get; set; }
    }
}
