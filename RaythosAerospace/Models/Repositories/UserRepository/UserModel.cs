using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RaythosAerospace.Models.Repositories.OrderRepository;
using RaythosAerospace.Models.Repositories.CartRepository;
using RaythosAerospace.Models.Repositories.ProductRepository;

namespace RaythosAerospace.Models.Repositories.UserRepository
{
    public class UserModel
    {
        [Key]
        [Required(ErrorMessage = "Please provide the NIC number")]
        [Display(Name = "NIC")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please provide the name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide the email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide the date of birth")]
        [Display(Name = "Date Of Birth")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please provide the address")]
        [Display(Name = "Date Of Birth")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please provide the address")]
        [Display(Name = "Password")]
        public string Password { get; set; }



        //navigate properties
        public ICollection<OrderModel> Orders { get; set; }
        public CartModel Cart { get; set; }

        public ICollection<ProductModel> Products { get; set; }
    }
}
