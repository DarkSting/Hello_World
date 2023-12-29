using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class ColorModel
    {
        [Key]
        public string ColorId { get; set; }

        public int Price { get; set; }
        public bool availability { get; set; }

        public string Color { get; set; }

        //navigation property

        public ICollection<CustomizationModel> customize { get; set; }
    }
}
