using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.CartRepository;

namespace RaythosAerospace.Models.Repositories.UserRepository
{
    public class UserModel
    {
        [Key]
        [Required(ErrorMessage = "Please provide the User ID")]
        [Display(Name = "Invoice Status")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please provide the name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide the email")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        //navigate properties
        public ICollection<OrderModel> Orders { get; set; }
        public CartModel Cart { get; set; }
    }
}
