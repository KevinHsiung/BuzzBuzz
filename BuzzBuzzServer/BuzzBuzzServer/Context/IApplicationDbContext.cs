using BuzzBuzzServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BuzzBuzzServer.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Product> Products { get; set; }

        Task<int> SaveChanges();
    }
}