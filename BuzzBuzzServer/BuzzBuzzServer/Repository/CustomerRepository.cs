using BuzzBuzzServer.Context;
using BuzzBuzzServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuzzBuzzServer.Repository
{
        
    public class CustomerRepository : ICustomerRepository
    {
        private IApplicationDbContext _context;
        public CustomerRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public Customer GetById(int id)
        {
            return _context.Customers.Include(x => x.Products).FirstOrDefault(x => x.Id == id);
        }
        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }
    }
}
