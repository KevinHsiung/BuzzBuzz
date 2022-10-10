using BuzzBuzzServer.Context;
using BuzzBuzzServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace BuzzBuzzServer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IApplicationDbContext _context;
        public ProductRepository(IApplicationDbContext context)
        {
            _context = context;
        }        

        public void AddToProduct(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
        }
        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public void DeleteProduct(int id)
        {
            var product = GetById(id);
            if(product != null)
            {
                product.Status = ProductStatus.Deleted;
                _context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            var dbProduct = GetById(product.Id);
            if(dbProduct != null)
            {
                dbProduct.Price = product.Price;
                dbProduct.Name = product.Name;
                _context.SaveChanges();
            }
        }

        public List<Product> GetAllByCustomerId(int id)
        {
            return _context.Products.Where(x => x.CustomerId == id).ToList();
        }

        public int GetProductCountByCustomerId(int id)
        {
            return _context.Products.Count(x => x.CustomerId == id);
        }

        public IEnumerable<Product> GetAllWithFilter(int id, int skip, int take)
        {
            return _context.Products.Where(x => x.CustomerId == id).Skip(skip).Take(take);
        }
    }
}
