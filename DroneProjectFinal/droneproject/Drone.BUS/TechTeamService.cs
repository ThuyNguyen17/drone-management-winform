using Drone.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Drone.BUS
{
    public class TechTeamService
    {
        DroneContextDB context = new DroneContextDB();
        public List<TechnicianTeam> GetAll()
        {
            return context.TechnicianTeams.ToList();
        }
    }
}

