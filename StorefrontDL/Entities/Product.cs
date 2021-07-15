using System;
using System.Collections.Generic;

#nullable disable

namespace StorefrontDL.Entities
{
    public partial class Product
    {
        public Product()
        {
            LineItems = new HashSet<LineItem>();
        }

        public string Name { get; set; }
        public double? Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
