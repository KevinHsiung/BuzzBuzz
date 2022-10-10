using BuzzBuzzServer.Models;
using System.Collections.Generic;

namespace BuzzBuzzServer.Repository
{
    public interface ICustomerRepository
    {
        public List<Customer> GetAll();
        public Customer GetById(int id);
    }
}
