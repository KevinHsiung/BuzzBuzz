using BuzzBuzzServer.Context;
using BuzzBuzzServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BuzzBuzzServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IApplicationDbContext _context;
        public ProductController(IApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Product product)
        {
            try
            {
                var customer = _context.Customers.Include(x=>x.Products).FirstOrDefault(x => x.Id == product.CustomerId);
                if(customer != null)
                {
                    customer.Products.Add(product);
                    await  _context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(int id, bool delete)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x=>x.Id == id);
                if(product != null && delete)
                {
                    product.Status = ProductStatus.Deleted;
                    await _context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Product product)
        {
            try
            {
                var item = _context.Products.FirstOrDefault(x => x.Id == product.Id);
                if (item != null)
                {
                    item.Price = product.Price;
                    item.Name = product.Name;
                    _context.Products.Update(item);
                    await _context.SaveChanges();
                    return Ok();
                }
                return BadRequest();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Get(int? id)
        {
            if (id.HasValue)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == id.Value);
                return Ok(product);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAllByCustomerId/{id}")]
        public ActionResult GetAllByCustomerId([FromRoute] int id)
        {
            var product = _context.Products.Where(x => x.CustomerId == id).ToList() ;
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductWithFilter")]
        public ActionResult GetProductWithFilter(int id, int skip, int take, bool isAsc = true, string column = "id")
        {
            var total = _context.Products.Count(x => x.CustomerId == id);
            var products = _context.Products.Where(x => x.CustomerId == id).Skip(skip).Take(take);
            switch (column)
            {
                case "name":
                    products = isAsc ? products.OrderBy(x => x.Name) : products.OrderByDescending(x => x.Name);
                    break;
                case "price":
                    products = isAsc ? products.OrderBy(x => x.Price) : products.OrderByDescending(x => x.Price);
                    break;
                default:
                    products = isAsc ? products.OrderBy(x => x.Id) : products.OrderByDescending(x => x.Id);
                    break;
            }

            return Ok(new { total, products = products.ToList()});
        }

    }
}
