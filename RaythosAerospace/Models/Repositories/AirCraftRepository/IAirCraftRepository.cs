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

        void DeleteSeat(string seatid);

      
        void UpdateSeats(IList<SeatModel> existingSeats);

        IList<SeatModel> GetAllSeats();
        SeatModel GetSeat(string id);


        IList<SeatModel> GetSeatTypes();

        AirCraftModel Insert(AirCraftModel model);
        AirCraftModel Update(AirCraftModel model);

        void UploadPhoto(AirCraftPhoto photo);

        IList<AirCraftPhoto> GetAllImages(string aircraftId);
        IList<EngineModel> GetEngineTypes();


        IList<AirCraftModel> GetAirCrafts();

      
        EngineModel GetEngine(string id);
        void DeleteEngine(string engineId);


        IList<AirCraftModel> GetAirCraftsByType(string type);

        AirCraftModel UpdateEngine(EngineModel engine,string id);

        void UpdateEngines(IList<EngineModel> engines);

        SeatModel UpdateSeats(SeatModel seat, string id);

        IList<ColorModel> GetAllColors();

        ColorModel GetColor(string colorId);

        void DeleteColor(string colorId);

        void UpdateColors(IList<ColorModel> colors);

        void UpdateColor(ColorModel color);


        void AddCustomization(CustomizationModel custom);
        CustomizationModel GetCustomization(string customizeId);

        double CalculateTotalPriceForAirCraft(ProductModel product);
        void UpdateCustomization(RemoveElementDTO item);




    }
}
