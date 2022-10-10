using BuzzBuzzServer.Models;
using BuzzBuzzServer.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BuzzBuzzServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public List<Customer> Get()
        {
            return _customerRepository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Customer Get([FromRoute] int id)
        {
            return _customerRepository.GetById(id);
        }


    }
}
