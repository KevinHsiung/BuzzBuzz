using BuzzBuzzServer.Models;
using BuzzBuzzServer.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BuzzBuzzServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private IProductRepository _productRepository;

        public ProductController(ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Product product)
        {
            try
            {
                var customer = _customerRepository.GetById(product.CustomerId);
                if (customer != null)
                {
                    _productRepository.AddToProduct(product);
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
                if (delete)
                {
                    _productRepository.DeleteProduct(id);
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
                _productRepository.UpdateProduct(product);
                return Ok();
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
                var product = _productRepository.GetById(id.Value);
                return Ok(product);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAllByCustomerId/{id}")]
        public ActionResult GetAllByCustomerId([FromRoute] int id)
        {
            var product = _productRepository.GetAllByCustomerId(id);
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductWithFilter")]
        public ActionResult GetProductWithFilter(int id, int skip, int take, bool isAsc = true, string column = "id")
        {
            var total = _productRepository.GetProductCountByCustomerId(id);
            var products = _productRepository.GetAllWithFilter(id, skip, take);
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
