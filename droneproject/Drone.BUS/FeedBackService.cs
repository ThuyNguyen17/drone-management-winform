using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drone.DAL.Entities;

namespace Drone.BUS
{
    public class FeedBackService
    {
        DroneContextDB context = new DroneContextDB();
        public List<Feedback> GetAll()
        {
            return context.Feedbacks.ToList();
        }
        public void Delete(string feedbackId)
        {
           var feedback = context.Feedbacks.FirstOrDefault(f => f.FeedbackID == feedbackId);
           if (feedback != null)
           {
             context.Feedbacks.Remove(feedback);
             context.SaveChanges();
           }
           else
           {
              throw new Exception("Feedback không tồn tại.");
           }
        }

    }
}
