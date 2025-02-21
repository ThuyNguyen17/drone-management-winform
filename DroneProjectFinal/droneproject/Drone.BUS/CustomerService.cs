using Drone.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Drone.BUS
{
    public class CustomerService
    {
        DroneContextDB context = new DroneContextDB();
        public List<Customer> GetAll()
        {
            return context.Customers.ToList();
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            return context.Customers
                .Where(c => c.CustomerID.ToLower().Contains(keyword) ||
                            c.Name.ToLower().Contains(keyword) ||
                            c.Type.ToLower().Contains(keyword) ||
                            c.Address.ToLower().Contains(keyword) ||
                            c.Email.ToLower().Contains(keyword) ||
                            c.Phone.ToLower().Contains(keyword))
                .ToList();
        }

        public void InsertUpdate(Customer customer)
        {
            var existingCustomer = context.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Type = customer.Type;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Email = customer.Email;
                existingCustomer.Address = customer.Address;
            }
            else
            {
                context.Customers.Add(customer);
            }
            context.SaveChanges();

        }

        public Customer FindById(string customerId)
        {
            return context.Customers.FirstOrDefault(c => c.CustomerID == customerId);

        }
        public void Delete(string customerId)
        {
            var customer = context.Customers.FirstOrDefault(c => c.CustomerID == customerId);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
        }
    }
}