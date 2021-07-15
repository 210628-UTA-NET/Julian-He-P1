using System;
using System.Collections.Generic;

#nullable disable

namespace StorefrontDL.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
