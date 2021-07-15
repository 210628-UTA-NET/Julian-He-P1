using System;
using System.Collections.Generic;

#nullable disable

namespace StorefrontDL.Entities
{
    public partial class Order
    {
        public Order()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int? Location { get; set; }
        public int? CustomerId { get; set; }
        public double? Totalprice { get; set; }
        public int Id { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Storefront LocationNavigation { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
