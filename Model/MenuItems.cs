using System;
using System.Collections.Generic;

namespace SmartOrder.API.Model
{
    public partial class MenuItems
    {
        public MenuItems()
        {
            OrderItems = new HashSet<OrderItems>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public virtual Stores Store { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
