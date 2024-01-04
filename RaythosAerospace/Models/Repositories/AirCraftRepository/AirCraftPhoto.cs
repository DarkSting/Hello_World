using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class AirCraftPhoto
    {
        [Key]
        public string PhotoID { get; set; }

        [Required(ErrorMessage ="File path is required")]
        public string FilePath { get; set; }
        public DateTime Date { get; set; }

        public string AirCraftID { get; set; }

        public string fileName { get; set; }
        //navigation
        public AirCraftModel airCraft { get; set; }
    }
}
