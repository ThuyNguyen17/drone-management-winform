using Drone.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Drone.BUS
{
    public class TypeService
    {

        DroneContextDB context = new DroneContextDB();
        public List<Type_> GetAll()
        {
            return context.Types.ToList();
        }
        public List<Type_> GetAllWithDrones()
        {
            return context.Types.Include("Drone").ToList();
        }

        public Type_ GetById(string typeId)
        {
            return context.Types.FirstOrDefault(t => t.TypeID == typeId);
        }

        public void DeleteType(string typeId)
        {
            var type = context.Types.FirstOrDefault(t => t.TypeID == typeId);
            if (type != null && !type.Drone.Any())
            {
                context.Types.Remove(type);
                context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Không thể xóa loại vì nó có drone liên kết.");
            }
        }

    }
}
