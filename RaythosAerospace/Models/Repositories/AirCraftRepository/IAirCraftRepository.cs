using RaythosAerospace.Models.Repositories.ProductRepository;
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

        void RemoveCustomization(string customizationid);

        AirCraftModel Insert(AirCraftModel model);
        AirCraftModel Update(AirCraftModel model);

        void UploadPhoto(AirCraftPhoto photo);
        IEnumerable<EngineModel> GetEngineTypes();
        IEnumerable<SeatModel> GetSeatTypes();

        IList<AirCraftModel> GetAirCrafts();

        SeatModel GetSeat(string id);
        EngineModel GetEngine(string id);


        IList<AirCraftModel> GetAirCraftsByType(string type);

        AirCraftModel UpdateEngine(EngineModel engine,string id);

        AirCraftModel UpdateSeats(SeatModel seat, string id);

        IList<ColorModel> GetAllColors();

        ColorModel GetColor(string colorId);


        void AddCustomization(CustomizationModel custom);
        CustomizationModel GetCustomization(string customizeId);

        double CalculateTotalPriceForAirCraft(ProductModel product);
        void UpdateCustomization(RemoveElementDTO item);




    }
}
