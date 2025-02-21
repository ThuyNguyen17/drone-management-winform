using Drone.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Drone.BUS
{
    public class DroneService
    {
        DroneContextDB context = new DroneContextDB();
        public List<Drone_> GetAll()
        {
            return context.Drones.ToList();
        }
        public List<Drone_> GetAllHasNo()
        {
            return context.Drones.Where(d => d.DroneID == null).ToList();
        }

        public List<Drone_> GetAllHasNo(string droneID)
        {
            return context.Drones.Where(d => d.DroneID == droneID && d.DroneID == null).ToList();
        }

        public Drone_ FindById(string droneId)
        {
            return context.Drones.FirstOrDefault(p => p.DroneID == droneId);
        }


        public void Delete(string droneId)
        {
            var droneToRemove = context.Drones.FirstOrDefault(d => d.DroneID == droneId);
            if (droneToRemove != null)
            {
                context.Drones.Remove(droneToRemove);
                context.SaveChanges();
            }
        }
        public void InsertUpdate(Drone_ d)
        {
            context.Drones.AddOrUpdate(d);
            context.SaveChanges();
        }

    }
}
