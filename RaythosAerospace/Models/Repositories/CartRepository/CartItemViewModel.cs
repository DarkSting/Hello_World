using RaythosAerospace.Models.Repositories.AirCraftRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RaythosAerospace.Models.Repositories.CartRepository
{
    public class CartItemViewModel
    {
       [Display(Name ="Amount")]
       public AirCraftModel aircraft { get; set; }
       public int count { get; set; }

       public string  other { get; set; }

    }
}
