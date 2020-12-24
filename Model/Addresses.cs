using System;
using System.Collections.Generic;

namespace SmartOrder.API.Model
{
    public partial class Addresses
    {
        public Addresses()
        {
            Stores = new HashSet<Stores>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? StoreId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Tk { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }

        public virtual Stores Store { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Stores> Stores { get; set; }
    }
}
