using Drone.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;

namespace Drone.BUS
{
    public class ContractDetailService
    {
        DroneContextDB context = new DroneContextDB();
        public List<ContractDetail> GetAll()
        {
            
            return context.ContractDetails.ToList();
        }
        public int? CalculateTotalDaysAndPrice(string droneId, DateTime startDate, DateTime endDate)
        {
            int rentalDays = (endDate - startDate).Days;
            var drone = context.Drones.FirstOrDefault(d => d.DroneID == droneId);

            if (drone != null)
            {
                return rentalDays * drone.RentalPrice;
            }

            return 0;
        }
        public ContractDetail FindById(string contractId)
        {
            return context.ContractDetails.FirstOrDefault(cd => cd.ContractID == contractId);
        }

        public void Delete(string contractId)
        {
            var contractDetailToRemove = context.ContractDetails.FirstOrDefault(cd => cd.ContractID == contractId);
            if (contractDetailToRemove != null)
            {
                context.ContractDetails.Remove(contractDetailToRemove);
                context.SaveChanges();
            }
        }
        public void InsertUpdate(ContractDetail contractDetail)
        {
            context.ContractDetails.AddOrUpdate(contractDetail);
            context.SaveChanges();
        }
        public List<ContractDetail> GetByContractId(string contractId)
        {
            return context.ContractDetails.Where(cd => cd.ContractID == contractId).ToList();
        }
    }
}
