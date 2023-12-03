using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public interface IAirCraftRepository
    {
        AirCraftModel Delete(string id);
        AirCraftModel Find(string id);

        AirCraftModel Insert(AirCraftModel model);
        AirCraftModel Update(AirCraftModel model);

        IEnumerable<EngineModel> GetEngineTypes();
        IEnumerable<SeatModel> GetSeatTypes();

        IList<AirCraftModel> GetAirCrafts();

        SeatModel GetSeat(string id);
        EngineModel GetEngine(string id);


        IList<AirCraftModel> GetAirCraftsByType(string type);

        AirCraftModel UpdateEngine(EngineModel engine,string id);

        AirCraftModel UpdateSeats(SeatModel seat, string id);




    }
}
