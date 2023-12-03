using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.OrderRepository
{
    public class ShippingModel
    {
        [Key]
        public string ShippingId { get; set; }

        [Required]
        public string ShippingType { get; set; }

        [Required]
        public string ShippingDesc { get; set; }

        public int ShippingCost { get; set; }

        public ICollection<OrderModel> Orders { get; set; }

    }
}
