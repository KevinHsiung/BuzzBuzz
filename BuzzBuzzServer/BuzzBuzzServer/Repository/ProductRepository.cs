using BuzzBuzzServer.Context;
using BuzzBuzzServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuzzBuzzServer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private IApplicationDbContext _context;
        public ProductRepository(IApplicationDbContext context)
        {
            _context = context;
        }        

        public async Task<int> AddToProduct(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            var product = GetById(id);
            if(product != null)
            {
                product.Status = ProductStatus.Deleted;
                _context.Products.Update(product);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            try
            {
                var dbProduct = _context.Products.FirstOrDefault(x => x.Id == product.Id);
                if (dbProduct != null)
                {
                    dbProduct.Price = product.Price;
                    dbProduct.Name = product.Name;
                    _context.Products.Update(dbProduct);
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (System.Exception ex)
            {

                throw ex;                
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
