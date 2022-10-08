using BuzzBuzzServer.Context;
using BuzzBuzzServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuzzBuzzServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IApplicationDbContext _context;

        public CustomerController(IApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Customer> Get()
        {
            return _context.Customers.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Customer Get([FromRoute]int id)
        {
            return _context.Customers.Include(x => x.Products).FirstOrDefault(x=>x.Id == id);
        }


    }
}
