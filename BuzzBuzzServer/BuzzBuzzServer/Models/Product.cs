using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BuzzBuzzServer.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public double Price { get; set; }
        public ProductStatus Status { get; set; }
    }
}
