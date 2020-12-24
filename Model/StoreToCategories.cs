using System;
using System.Collections.Generic;

namespace SmartOrder.API.Model
{
    public partial class StoreToCategories
    {
        public int CategoryId { get; set; }
        public int StoreId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Stores Store { get; set; }
    }
}
