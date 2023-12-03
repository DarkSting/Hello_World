using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class EngineModel
    {
        [Key]
        public string EngineId { get; set; }

        [Required(ErrorMessage = "Please provide engine type")]

        public string EngineType { get; set; }

        [Required(ErrorMessage = "Please provide engine price")]
        public int UnitPrice { get; set; }

        public int UnitCount { get; set; }

        //navigate properties
        public ICollection<AirCraftModel> AirCraftModels { get; set; }
    }
}
