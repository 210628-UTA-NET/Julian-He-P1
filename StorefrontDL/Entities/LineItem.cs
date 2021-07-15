using System;
using System.Collections.Generic;

#nullable disable

namespace StorefrontDL.Entities
{
    public partial class LineItem
    {
        public int? OrderId { get; set; }
        public int ListId { get; set; }
        public int? ProdId { get; set; }
        public int? StoreId { get; set; }
        public int? Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Prod { get; set; }
        public virtual Storefront Store { get; set; }
    }
}
