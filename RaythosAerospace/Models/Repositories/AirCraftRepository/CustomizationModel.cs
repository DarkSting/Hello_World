using RaythosAerospace.Models.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class CustomizationModel
    {
        [Key]
        public string CustomId { get; set; }

        public double Scale { get; set; }

        public string ExteriorColorId { get; set; }

        public string InteriorColorId { get; set; }
        public string EngineId { get; set; }
        public string SeatId { get; set; }

        public string ExtraModifications { get; set; }


        //navigation properties

        public SeatModel Seat { get; set; }
        public EngineModel Engine { get; set; }
        public ColorModel Color { get; set; }

        public ProductModel Product { get; set; }
    }
}
