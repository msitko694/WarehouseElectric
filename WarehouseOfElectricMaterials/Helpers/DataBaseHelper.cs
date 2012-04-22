using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WarehouseElectric.Models;
using WarehouseElectric.DataLayer;

namespace WarehouseElectric.Helpers
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataBaseHelper" in both code and config file together.
    public class DataBaseHelper
    {
        /// <summary>
        /// Fills the database with sample data.
        /// </summary>
        public static void FillDatabaseWithSampleData()
        {
            ProductCategoriesManager productCategoriesManager = new ProductCategoriesManager();
            PC_ProductCategory productCategory = new PC_ProductCategory
            {
                PC_NAME = "Kable",
                PC_PC_ID = null
            };
            productCategoriesManager.Add(productCategory);

            QuantityTypesManager quantityManager = new QuantityTypesManager();
            QT_QuantityType quantityType = new QT_QuantityType
            {
                QT_NAME = "metr"
            };
            quantityManager.Add(quantityType);

            ProductsManager productManager = new ProductsManager();
            PR_Product product = new PR_Product
            {
                PR_PC_ID = productCategory.PC_ID,
                PR_ADDED = DateTime.Now,
                PR_IS_ACTIVE = true,
                PR_USED = true,
                PR_NAME = "kabel miedziany 2mm",
                PR_LAST_MODIFIED = DateTime.Now,
                PR_UNIT_PRICE = 3.44m,
                PR_DEPOT_QUANTITY = 1000,
                PR_QT_ID = quantityType.QT_ID
            };
            productManager.Add(product);

            SpeditionManager speditionManager = new SpeditionManager();
            SP_Spedition spedition = new SP_Spedition
            {
                SP_NAME = "Poczta Polska"
            };
            speditionManager.Add(spedition);

            CustomersManager customerManager = new CustomersManager();
            CU_Customer customer = new CU_Customer
            {
                CU_ADDED = DateTime.Now,
                CU_LAST_MODIFIED = DateTime.Now,
                CU_NAME = "Electrolux",
                CU_PHONE = "534093845",
                CU_POST_CODE = "55-555",
                CU_STREET = "Elektryczna",
                CU_TOWN = "Rybnik"
            };
            customerManager.Add(customer);

            PositionsManager postionsManager = new PositionsManager();
            PO_Position position = new PO_Position
            {
                PO_NAME= "Kierownik"
            };
            postionsManager.Add(position);

            WorkersManager workerManager = new WorkersManager();
            WO_Worker worker = new WO_Worker
            {
                WO_ADDED = DateTime.Now,
                WO_LAST_MODIFIED = DateTime.Now,
                WO_BIRTH_DATE = new DateTime(1990, 02, 14),
                WO_NAME = "Marcin",
                WO_SURNAME = "Sitko",
                WO_PESEL = "90021416955",
                WO_PHONE = "555 555 555",
                WO_WORKING_SINCE = new DateTime(2010, 12, 12),
                WO_PO_ID = position.PO_ID
            };
            workerManager.Add(worker);

            UsersManager usersManager = new UsersManager();
            US_User user = new US_User
            {
                US_ADDED = DateTime.Now,
                US_IS_ADMIN = true,
                US_IS_STOREKEEPER = true,
                US_IS_CASHIER = true,
                US_LAST_MODIFIED = DateTime.Now,
                US_PASSWORD = "admin",
                US_USERNAME = "admin",
                US_WO_ID = worker.WO_ID
            };
            usersManager.Add(user);
                                        
            InvoicesManager invoiceManager = new InvoicesManager();
            IN_Invoice invoice = new IN_Invoice
            {
                IN_ADDED = DateTime.Now,
                IN_LAST_MODIFIED = DateTime.Now,
                IN_SPEDITION_COST = 23.4m,
                IN_TOTAL = 1000,
                IN_SP_ID = spedition.SP_ID,
                IN_WO_ID = worker.WO_ID,
                IN_CU_ID = customer.CU_ID,
            };
            invoiceManager.Add(invoice);

            InvoicesItemsManager invoiceItemsManager = new InvoicesItemsManager();
            IE_InvoicesItem invoiceItem = new IE_InvoicesItem
            {
                IE_IN_ID = invoice.IN_ID,
                IE_QUANTITY = 100,
                IE_LAST_MODIFIED = DateTime.Now,
                IE_ADDED = DateTime.Now,
                IE_PR_ID = product.PR_ID,
                IE_UNIT_PRICE = product.PR_UNIT_PRICE,
                IE_TOTAL_NETTO = 100m * product.PR_UNIT_PRICE,
                IE_VAT_RATE = 0.22m,
                IE_TOTAL_VAT = (100m*product.PR_UNIT_PRICE) * 0.22m,
                IE_TOTAL_BRUTTO = (100m * product.PR_UNIT_PRICE) + (100m * product.PR_UNIT_PRICE) * 0.22m
            };
            invoiceItemsManager.Add(invoiceItem);
            IE_InvoicesItem invoiceItem2 = new IE_InvoicesItem
            {
                IE_IN_ID = invoice.IN_ID,
                IE_QUANTITY = 10,
                IE_LAST_MODIFIED = DateTime.Now,
                IE_ADDED = DateTime.Now,
                IE_PR_ID = product.PR_ID,
                IE_UNIT_PRICE = product.PR_UNIT_PRICE,
                IE_TOTAL_NETTO = 10m * product.PR_UNIT_PRICE,
                IE_VAT_RATE = 0.22m,
                IE_TOTAL_VAT = (10m * product.PR_UNIT_PRICE) * 0.22m,
                IE_TOTAL_BRUTTO = (10m * product.PR_UNIT_PRICE) + (10m * product.PR_UNIT_PRICE) * 0.22m
            };
            invoiceItemsManager.Add(invoiceItem2);
        }
    }
}
