using BuzzBuzzServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuzzBuzzServer.Repository
{
    public interface IProductRepository
    {
        Task<int> AddToProduct(Product product);
        Task<int> UpdateProductAsync(Product product);
        Task<int> DeleteProductAsync(int id);
        Product GetById(int id);
        List<Product> GetAllByCustomerId(int id);
        int GetProductCountByCustomerId(int id);

        IEnumerable<Product> GetAllWithFilter(int id, int skip, int take);

    }
}