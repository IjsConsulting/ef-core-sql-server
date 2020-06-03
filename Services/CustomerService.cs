using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ef_core_sql_server.Models;
using Microsoft.EntityFrameworkCore;

namespace ef_core_sql_server
{
    /// <summary>
    /// Customer Service
    /// </summary>
    public class CustomerService : ICustomerService
    {
        int max_count = 100;

        /// <summary>
        /// Finds a customer by customerId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customers> FindAsync(string id)
        {
            using (var db = new NorthwindContext())
            {
                return await db.Customers.
                  FindAsync(id);
            }
        }

        /// <summary>
        /// Gets customers - limited to 100
        /// </summary>
        /// <returns></returns>
        public async Task<List<Customers>> GetCustomersAsync()
        {
            using (var db = new NorthwindContext())
            {
                return await db.Customers.
                    Take(max_count).
                    ToListAsync();
            }
        }

        /// <summary>
        /// Inserts a new customer if none found
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public async Task<Customers> InsertAsync(Customers customers)
        {
            using (var db = new NorthwindContext())
            {
                var customer = await FindAsync(customers.CustomerId);
                if (customer != null)
                {
                    throw new Exception("Customer already exists.");
                }

                try
                {
                    var result = await db.Customers.AddAsync(customers);
                    await db.SaveChangesAsync();

                    return customers;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    throw e;
                }
            }
        }

        /// <summary>
        /// Updates a customer if located
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public async Task<Customers> UpdateAsync(Customers customers)
        {
            using (var db = new NorthwindContext())
            {
                var customer = await FindAsync(customers.CustomerId);
                if (customer == null)
                {
                    throw new Exception("Customer not found");
                }

                try
                {
                    db.Entry(customers).State = EntityState.Modified;

                    var result = await db.SaveChangesAsync();
                    return customer;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    throw e;
                }
            }
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>    
        public async Task<bool> DeleteAsync(string id)
        {
            using (var db = new NorthwindContext())
            {
                var customer = await FindAsync(id);
                if (customer == null)
                {
                    return true;
                }
                try
                {
                    db.Customers.Remove(customer);
                    var result = await db.SaveChangesAsync();
                    return (result == 1);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    return false;
                }
            }
        }
    }
}