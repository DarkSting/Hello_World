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
           return _context.AirCrafts.FirstOrDefault(a => a.AircraftId == id);
        }

        //get all aircrafts
        public IList<AirCraftModel> GetAirCrafts()
        {
            return _context.AirCrafts.ToList();
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
        public AirCraftModel UpdateSeats(SeatModel seat, string id)
        {
            throw new NotImplementedException();
        }

        //gets all engines
        public IEnumerable<EngineModel> GetEngineTypes()
        {
            return _context.Engines.ToList();

           
        }

        //gets all seats
        public IEnumerable<SeatModel> GetSeatTypes()
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
            _context.Customization.Remove(foundCustomization);
            _context.SaveChanges();
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

      
    }
}
