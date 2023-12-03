using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class SeatModel
    {
        [Key]
        public string SeatID { get; set; }

        [Required(ErrorMessage = "Please provide seat price")]
        public int UnitPrice { get; set; }

        [Required(ErrorMessage ="Please provide seat description")]
        public string SeatType { get; set; }
        public int SeatCount { get; set; }


        //navigate properties
        public ICollection<AirCraftModel> AirCraftModels { get; set; }
    }
}
