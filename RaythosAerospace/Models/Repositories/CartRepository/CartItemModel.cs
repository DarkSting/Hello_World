using RaythosAerospace.Models.Repositories.AirCraftRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.CartRepository
{
    public class CartItemModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CartItemId { get; set; }
        public string CartId { get; set; }
        public CartModel Cart { get; set; }
        public DateTime ItemAddedDate { get; set; }
        public double UnitPrice { get; set; }
        public string AirCraftId { get; set; }
        public AirCraftModel AirCraft { get; set; }
    }
}
