using System;
using System.Collections.Generic;

namespace SmartOrder.API.Model
{
    public partial class Orders
    {
        public Orders()
        {
            OrderItems = new List<OrderItems>();
        }

        public int Id { get; set; }
        public int Status { get; set; }
        public int? EstimatedMinutes { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Stores Store { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
