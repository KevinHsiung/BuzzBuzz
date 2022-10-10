using BuzzBuzzServer.Models;
using System.Collections.Generic;

namespace BuzzBuzzServer.Repository
{
    public interface IProductRepository
    {
        void AddToProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        Product GetById(int id);
        List<Product> GetAllByCustomerId(int id);
        int GetProductCountByCustomerId(int id);

        IEnumerable<Product> GetAllWithFilter(int id, int skip, int take);

    }
}