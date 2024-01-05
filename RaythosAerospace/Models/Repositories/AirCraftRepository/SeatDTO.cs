using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class SeatDTO
    {
        public IList<SeatModel> existingSeats { get; set; }
        public IList<SeatModel> newSeats { get; set; }
    }
}
