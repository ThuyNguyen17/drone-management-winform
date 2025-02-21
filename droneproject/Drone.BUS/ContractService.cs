using Drone.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Drone.BUS
{
    public class ContractService
    {
        DroneContextDB context = new DroneContextDB();
        public List<Contract_> GetAll()
        {
            return context.Contract_s.ToList();
        }
        public Contract_ FindById(string contractId)
        {
            return context.Contract_s.FirstOrDefault(cd => cd.ContractID == contractId);
        }
        public List<String> GetRequest() 
        {
            List<Contract_>  list_CT = context.Contract_s.ToList();
            List<String> ret = new List<String>();
            for (int i = 0; i < list_CT.LongCount(); i++)
            {
                ret.Add(list_CT[i].Request);

           
            }
            return ret; 
        }
        public void Delete(string contractId)
        {
            var contractToRemove = context.Contract_s.FirstOrDefault(c => c.ContractID == contractId);
            if (contractToRemove != null)
            {
                context.Entry(contractToRemove).State = EntityState.Deleted;
                context.SaveChanges();
            }
            else
            {
                throw new Exception($"Không tìm thấy hợp đồng với ID: {contractId}");
            }
        }


        public void InsertUpdate(Contract_ contract)
        {
            context.Contract_s.AddOrUpdate(contract); 
            context.SaveChanges();
        }
        public List<MonthlyContractData> GetMonthlyData(DateTime startDate, DateTime endDate)
        {
            var contracts = context.Contract_s.ToList();

            var filteredContracts = contracts
                .Where(c => c.NgayLap.HasValue && c.NgayLap.Value >= startDate && c.NgayLap.Value <= endDate)
                .GroupBy(c => new { c.NgayLap.Value.Month, c.NgayLap.Value.Year })
                .Select(g => new MonthlyContractData
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    ContractCount = g.Count(),
                    TotalMoney = g.Sum(c => (decimal)(c.TotalValue ?? 0)),
                });

            return filteredContracts.ToList();
        }
        public decimal GetTotalContractMoney()
        {
            return context.Contract_s
                          .Where(contract => contract.TotalValue.HasValue)
                          .Sum(contract => contract.TotalValue ?? 0);
        }
        public int GetTotalContractsCount()
        {
            return context.Contract_s.Count();
        }
        public class MonthlyContractData
        {
            public int Month { get; set; }
            public int Year { get; set; }
            public int ContractCount { get; set; }
            public decimal TotalMoney { get; set; }
        }
    }
}
