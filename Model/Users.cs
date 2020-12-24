using System;
using System.Collections.Generic;

namespace SmartOrder.API.Model
{
    public partial class Users
    {
        public Users()
        {
            Addresses = new HashSet<Addresses>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
