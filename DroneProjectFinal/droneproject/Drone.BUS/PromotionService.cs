using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drone.DAL.Entities;

namespace Drone.BUS
{
    public class PromotionService
    {
        DroneContextDB context = new DroneContextDB();
        public List<Promotion> GetAll()
        {
            return context.Promotions.ToList();
        }
        public Promotion FindById(string promotionId)
        {
            return context.Promotions.FirstOrDefault(p => p.PromotionID == promotionId);
        }
    }
}
