using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            return _context.Seats.FirstOrDefault(s=>s.SeatID==id);
        }


        //returns an engine based on the primary key
        public EngineModel GetEngine(string id)
        {
            return _context.Engines.Find(id);
        }
    }
}
