using System.Collections.Generic;
using System.Threading.Tasks;
using ef_core_sql_server.Models;

namespace ef_core_sql_server
{
    public interface ICustomerService
    {
        Task<List<Customers>> GetCustomersAsync();
        Task<Customers> FindAsync(string id);
        Task<Customers> UpdateAsync(Customers customers);
        Task<Customers> InsertAsync(Customers customers);
        Task<bool> DeleteAsync(string id);
    }
}