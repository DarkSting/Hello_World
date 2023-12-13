using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class AirCraftViewModel
    {
        public AirCraftModel model { get; set; }

        public string SelectedEngineType { get; set; }
        public string SelectedSeatType { get; set; }
        public IEnumerable<SeatModel> seats { get; set; }
        public IEnumerable<EngineModel> engines { get; set; }

        public IEnumerable<AirCraftModel> AirCrafts { get; set; }
    }
}
