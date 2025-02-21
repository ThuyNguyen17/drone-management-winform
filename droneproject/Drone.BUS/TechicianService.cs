using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drone.DAL.Entities;

namespace Drone.BUS
{
    public class TechicianService
    {
        DroneContextDB context = new DroneContextDB();
        public List<Technician> GetAll()
        {
            return context.Technicians.ToList();
        }
        public Technician FindById(string id)
        {
            return context.Technicians.FirstOrDefault(p => p.TechnicianID == id);
        }
        public void Delete(string id)
        {
            var DetailToRemove = context.Technicians.FirstOrDefault(cd => cd.TechnicianID == id);
            if (DetailToRemove != null)
            {
                context.Technicians.Remove(DetailToRemove);
                context.SaveChanges();
            }
        }
        public void InsertUpdate(Technician tech)
        {
            context.Technicians.AddOrUpdate(tech);
            context.SaveChanges();
        }

    }
}
