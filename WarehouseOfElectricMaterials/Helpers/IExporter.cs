using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric.Helpers
{
    interface IExporter
    {
       void ExportInvoice(IN_Invoice invoice, String filePath);

       void ExportOrder(OR_Order order, String filePath);
    }
}
