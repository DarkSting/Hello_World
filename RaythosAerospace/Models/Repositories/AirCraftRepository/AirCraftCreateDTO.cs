using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class AirCraftCreateDTO
    {   
        public AirCraftModel AirCraftDet { get; set; }

        public IList<AirCraftModel> airCrafts { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
