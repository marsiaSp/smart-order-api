using System;
using System.Collections.Generic;

namespace SmartOrder.API.Model
{
    public partial class Stores
    {
        public Stores()
        {
            Addresses = new HashSet<Addresses>();
            MenuItems = new HashSet<MenuItems>();
            Orders = new HashSet<Orders>();
            StoreToCategories = new HashSet<StoreToCategories>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? AddressId { get; set; }

        public virtual Addresses Address { get; set; }
        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<MenuItems> MenuItems { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<StoreToCategories> StoreToCategories { get; set; }
    }
}
