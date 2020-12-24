using System;
using System.Collections.Generic;

namespace SmartOrder.API.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
        public DateTime? DateCreated { get; set; }
        public int Status { get; set; }
        public int Store { get; set; }
        public decimal TotalPrice { get; set; }
    }
}