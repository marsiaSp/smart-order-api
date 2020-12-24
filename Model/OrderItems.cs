using System;
using System.Collections.Generic;

namespace SmartOrder.API.Model
{
    public partial class OrderItems
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuitemId { get; set; }
        public int Quantity { get; set; }

        public virtual MenuItems Menuitem { get; set; }
        public virtual Orders Order { get; set; }
    }
}
