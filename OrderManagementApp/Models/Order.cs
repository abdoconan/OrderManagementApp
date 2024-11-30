using System.ComponentModel.DataAnnotations;

namespace OrderManagementApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
