using System;
using System.Collections.Generic;

namespace SmartOrder.API.Model
{
    public partial class Categories
    {
        public Categories()
        {
            InverseParent = new HashSet<Categories>();
            StoreToCategories = new HashSet<StoreToCategories>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual Categories Parent { get; set; }
        public virtual ICollection<Categories> InverseParent { get; set; }
        public virtual ICollection<StoreToCategories> StoreToCategories { get; set; }
    }
}
