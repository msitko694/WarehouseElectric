using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseElectric.Entities
{
    class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? parentId { get; set; }
    }
}
