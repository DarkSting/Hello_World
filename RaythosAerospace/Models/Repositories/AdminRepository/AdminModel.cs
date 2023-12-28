using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AdminRepository
{
    public class AdminModel
    {
       
        [Key]
        [Display(Name ="NIC number")]
        public string NIC { get; set; }

        [Required(ErrorMessage ="Please provide the name")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please provide the email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please provide the password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please provide the Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

    }
}
