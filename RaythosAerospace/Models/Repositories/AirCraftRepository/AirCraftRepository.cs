using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RaythosAerospace.Models.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AirCraftRepository
{
    public class AirCraftRepository : IAirCraftRepository
    {
        private readonly AppDBContext _context;
        public AirCraftRepository(AppDBContext context)
        {
            _context = context;
        }
        public AirCraftModel Delete(string id)
        {
            throw new NotImplementedException();
        }


        public AirCraftModel Find(string id)
        {

           AirCraftModel foundAirCraft = _context.AirCrafts.FirstOrDefault(a => a.AircraftId == id);

            foundAirCraft.Photos = GetAllImages(id);
            foundAirCraft.EngineType = GetEngine(foundAirCraft.AircraftId);

            return foundAirCraft;
        }

        public IList<SeatModel> GetAllSeats()
        {
            return _context.Seats.ToList();
        }

        public void UpdateSeats(IList<SeatModel> existingSeats)
        {
            foreach (SeatModel seat in existingSeats)
            {
                if (seat.SeatType==null && seat.UnitPrice==0)
                {
                    continue;
                }
                else if (seat.SeatType != string.Empty && seat.UnitPrice != 0 && seat.SeatID==null)
                {
                    seat.SeatID = "ST-" + Guid.NewGuid().ToString();
                    _context.Seats.Add(seat);
                }
                else
                {

                    _context.Seats.Update(seat);
                }
                
            }

            _context.SaveChanges();
        }

        //get all aircrafts
        public IList<AirCraftModel> GetAirCrafts()
        {
            IList<AirCraftModel> airCrafts = _context.AirCrafts.ToList();

            foreach(AirCraftModel current in airCrafts)
            {
                current.Photos = GetAllImages(current.AircraftId);
               
            }

            return airCrafts;
        }

        

        //get air crafts by type
        public IList<AirCraftModel> GetAirCraftsByType(string type)
        {

            SeatModel foundSeat = _context.Seats.FirstOrDefault(s => s.SeatType==type);

            if (foundSeat==null)
            {
                return null;
            }

            IList<AirCraftModel> foundList = _context.AirCrafts.Where(a => a.SeatID == foundSeat.SeatID).ToList();

            return foundList;
        }

      
        //Inserts aircrafts
        public AirCraftModel Insert(AirCraftModel model)
        {
            _context.AirCrafts.Add(model);
            _context.SaveChanges();

            return model;
        }

        //update an aircraft
        public AirCraftModel Update(AirCraftModel model)
        {
            EntityEntry changed = _context.AirCrafts.Attach(model);
            changed.State = EntityState.Modified;

            _context.SaveChanges();

            return model;
        }

        //update engine of an aircraft
        public AirCraftModel UpdateEngine(EngineModel engine, string id)
        {
            bool exits = _context.AirCrafts.Any(a => a.AircraftId == id);

            if (exits)
            {
                //getting the target model to be changed
                AirCraftModel foundCraft = _context.AirCrafts.FirstOrDefault(a => a.AircraftId == id);
                foundCraft.EngineType = engine;

                //applying changes
                EntityEntry changed = _context.AirCrafts.Attach(foundCraft);
                changed.State = EntityState.Modified;
               _context.SaveChanges();

                return foundCraft;

            }

            return null;
        }

        //update seats of an aircraft
        public SeatModel UpdateSeats(SeatModel seat, string id)
        {
            throw new NotImplementedException();
        }

        //gets all engines
        public IList<EngineModel> GetEngineTypes()
        {
            return _context.Engines.ToList();

           
        }

        //gets all seats
        public IList<SeatModel> GetSeatTypes()
        {
             return _context.Seats.ToList();

        }

        //returns a seat based on the primary key
        public SeatModel GetSeat(string id)
        {

            SeatModel foundSeatModel = null;

            try
            {

                foundSeatModel = _context.Seats.FirstOrDefault(s => s.SeatID == id);

            }
            catch(Exception e)
            {
                foundSeatModel = null;
            }

            return foundSeatModel;
        }


        //returns an engine based on the primary key
        public EngineModel GetEngine(string id)
        {
            return _context.Engines.Find(id);
        }

        public IList<ColorModel> GetAllColors()
        {
            return _context.Colors.ToList();
        }

        public void AddCustomization(CustomizationModel custom)
        {
            _context.Customization.Add(custom);
            _context.SaveChanges();
        }

        public void RemoveCustomization(string customizationid)
        {
            CustomizationModel foundCustomization = GetCustomization(customizationid);

            try
            {
                _context.Customization.Remove(foundCustomization);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return;
            }
            
        }

        public void UpdateCustomization(RemoveElementDTO item)
        {
            if (item.customtype == "seat")
            {
                CustomizationModel foundCustomizationModel = _context.Customization.Find(item.customizationid);
                foundCustomizationModel.SeatId = null;
                EntityEntry changed = _context.Customization.Attach(foundCustomizationModel);
                changed.State = EntityState.Modified;
                _context.SaveChanges();


            }
            else if(item.customtype== "exterior")
            {
                CustomizationModel foundCustomizationModel = _context.Customization.Find(item.customizationid);
                foundCustomizationModel.ExteriorColorId = null;
                EntityEntry changed = _context.Customization.Attach(foundCustomizationModel);
                changed.State = EntityState.Modified;
                _context.SaveChanges();
            }
            else if(item.customtype== "interior")
            {
                CustomizationModel foundCustomizationModel = _context.Customization.Find(item.customizationid);
                foundCustomizationModel.InteriorColorId = null;
                EntityEntry changed = _context.Customization.Attach(foundCustomizationModel);
                changed.State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
        public CustomizationModel GetCustomization(string customizeId)
        {
            CustomizationModel returnedVal = null;

            try {
                
                returnedVal = _context.Customization.Find(customizeId);
            }
            catch(Exception e)
            {
                returnedVal = null;
            }

            return returnedVal;
        }

        public double CalculateTotalPriceForAirCraft(ProductModel product)
        {
            if (product.CustomizationId == null)
            {
                return _context.AirCrafts.Find(product.AirCraftId).AirCraftPrice;
            }
            else
            {
                CustomizationModel customizationModel = GetCustomization(product.CustomizationId);

                double aircraftPrice = _context.AirCrafts.Find(product.AirCraftId).AirCraftPrice;

                var properties = typeof(CustomizationModel).GetProperties();

                //iterating the customization model and avoiding the null values
                foreach (var property in properties)
                {
                    var value = property.GetValue(customizationModel);
                    string propname = property.Name;

                    if (value != null && propname == "ExteriorColorId")
                    {

                        ColorModel foundColor = _context.Colors.Find(value as string);
                        aircraftPrice = aircraftPrice + (aircraftPrice * foundColor.Price) / 100;

                    }
                    else if (value != null && propname == "InteriorColorId")
                    {
                        ColorModel foundColor = _context.Colors.Find(value as string);
                        aircraftPrice = aircraftPrice + (aircraftPrice * foundColor.Price) / 100;
                    }
                    else if (value != null && propname == "SeatId")
                    {
                        SeatModel foundSeat = _context.Seats.Find(value as string);
                        aircraftPrice = aircraftPrice + (aircraftPrice * foundSeat.UnitPrice) / 100;
                    }
                    else if (value != null && propname == "ExtraModifications")
                    {
                        continue;
                    }
                }

                return aircraftPrice;
            }

            
        }

        public ColorModel GetColor(string colorId)
        {
            ColorModel foundColor = null;
            try
            {
                foundColor = _context.Colors.Find(colorId);
            }
            catch(Exception e)
            {
                foundColor = null;
            }

            return foundColor;
            
        }

        public void UploadPhoto(AirCraftPhoto photo)
        {
            
            try
            {
                _context.AirCraftPhoto.Add(photo);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return;
            }

        }

        public IList<AirCraftPhoto> GetAllImages(string aircraftId)
        {
            return _context.AirCraftPhoto.Where(p => p.AirCraftID == aircraftId).ToList();
        }

        public void DeleteSeat(string seatid)
        {
            SeatModel seatModel = _context.Seats.Find(seatid);
            _context.Seats.Remove(seatModel);
            _context.SaveChanges();
        }

        public void DeleteEngine(string engineId)
        {
            EngineModel engineModel = _context.Engines.Find(engineId);
            _context.Engines.Remove(engineModel);
            _context.SaveChanges();
        }

        public void DeleteColor(string colorId)
        {
            ColorModel colorModel = _context.Colors.Find(colorId);
            _context.Colors.Remove(colorModel);
            _context.SaveChanges();
        }

        public void UpdateColor(ColorModel color)
        {
            throw new NotImplementedException();
        }

        public void UpdateEngines(IList<EngineModel> engines)
        {
            foreach (EngineModel engine in engines)
            {
                if (engine.EngineType == null && engine.UnitPrice == 0)
                {
                    continue;
                }
                else if (engine.EngineType != string.Empty && engine.UnitPrice != 0 && engine.EngineId == null)
                {
                    engine.EngineId = "ENG-" + Guid.NewGuid().ToString();
                    _context.Engines.Add(engine);
                }
                else
                {

                    _context.Engines.Update(engine);
                }

            }

            _context.SaveChanges();
        }

        public void UpdateColors(IList<ColorModel> colors)
        {
            foreach (ColorModel color in colors)
            {
                if (color.Color == null && color.Price == 0)
                {
                    continue;
                }
                else if (color.Color != string.Empty && color.Price != 0 && color.ColorId == null)
                {
                    color.ColorId = "CLR-" + Guid.NewGuid().ToString();
                    _context.Colors.Add(color);
                }
                else
                {

                    _context.Colors.Update(color);
                }

            }

            _context.SaveChanges();
        }
    }
}
