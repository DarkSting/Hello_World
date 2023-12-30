using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class RemoveElementDTO
    {
        public string customtype { get; set; }

        public string elementid { get; set; }

        public string customizationid { get; set; }
    }
}
