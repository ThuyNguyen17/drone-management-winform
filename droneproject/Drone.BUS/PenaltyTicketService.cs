using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drone.DAL.Entities;

namespace Drone.BUS
{
    public class PenaltyTicketService
    {

        DroneContextDB context = new DroneContextDB();
        public List<PenaltyTicket> GetAll()
        {
            return context.PenaltyTickets.ToList();
        }
    }
}
