using BuzzBuzzServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BuzzBuzzServer.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Product> Products { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}