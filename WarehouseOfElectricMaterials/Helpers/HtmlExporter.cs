using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Xml;
using System.IO;
using System.Diagnostics;
using WarehouseElectric.DataLayer;
using WarehouseElectric.Models;

namespace WarehouseElectric.Helpers
{
    class HtmlExporter : IExporter
    {
        #region IExporter Members

        public void ExportInvoice(DataLayer.IN_Invoice invoice, String filePath)
        {
            ICompanyManager companyManager = new CompanyManager();
            CI_CompanyInfo companyInfo =  companyManager.GetCompanyData();
            XDocument xmlTree= new XDocument(
                                    new XElement("invoice",
                                        new XElement("customer",
                                            new XElement("name", invoice.CU_Customer.CU_NAME),
                                            new XElement("phone", invoice.CU_Customer.CU_PHONE),
                                            new XElement("town", invoice.CU_Customer.CU_TOWN),
                                            new XElement("street", invoice.CU_Customer.CU_STREET),
                                            new XElement("postcode", invoice.CU_Customer.CU_POST_CODE)
                                            
                                        ),
                                        new XElement("company",
                                            new XElement("name", companyInfo.CI_NAME),
                                            new XElement("phone", companyInfo.CI_PHONE),
                                            new XElement("postcode", companyInfo.CI_POST_CODE),
                                            new XElement("town", companyInfo.CI_TOWN),
                                            new XElement("street", companyInfo.CI_STREET)
                                        ),
                                        new XElement("date",  invoice.IN_ADDED.ToShortDateString()),
                                        new XElement("speditionCost", invoice.IN_SPEDITION_COST),
                                        new XElement("totalCost" , invoice.IN_TOTAL),
                                        new XElement("invoiceNumber", invoice.IN_INVOICE_NUMBER),
                                        new XElement("items")
                                    )
                                );

            XElement itemsNode = xmlTree.Element("invoice").Element("items");
            int i = 0;
            foreach(var item in invoice.IE_InvoicesItems)
            {
                itemsNode.Add(new XElement("item",
                                new XElement("positionNumber", i),
                                new XElement("name", item.PR_Product.PR_NAME),
                                new XElement("quantityUnit", item.PR_Product.QT_QuantityType.QT_NAME),
                                new XElement("quantity", item.IE_QUANTITY),
                                new XElement("unitPrice" , item.IE_UNIT_PRICE),
                                new XElement("totalNettoPrice" , item.IE_TOTAL_NETTO),
                                new XElement("vatRate", item.IE_VAT_RATE),
                                new XElement("totalVat", item.IE_TOTAL_VAT),
                                new XElement("totalBruttoPrice", item.IE_TOTAL_BRUTTO)
                              )
                );
                i++;
            }
            XslCompiledTransform xslTransform = new XslCompiledTransform();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("WarehouseElectric.Helpers.InvoiceTransform.xslt");
            XmlWriter xmlWriter = XmlWriter.Create(filePath);

            XmlReader xmlReader = XmlReader.Create(stream);
            xslTransform.Load(xmlReader);
            try
            {
                xslTransform.Transform(xmlTree.CreateReader(),xmlWriter);
            }
            catch(Exception e)
            {
                Debug.Write(e);
            }
        }

        public void ExportOrder(DataLayer.OR_Order order, String filePath)
        {
            ICompanyManager companyManager = new CompanyManager();
            CI_CompanyInfo companyInfo = companyManager.GetCompanyData();
            XDocument xmlTree = new XDocument(
                                    new XElement("order",
                                        new XElement("supplier",
                                            new XElement("name", order.SU_Supplier.SU_NAME),
                                            new XElement("phone", order.SU_Supplier.SU_PHONE),
                                            new XElement("town", order.SU_Supplier.SU_TOWN),
                                            new XElement("street", order.SU_Supplier.SU_STREET),
                                            new XElement("postcode", order.SU_Supplier.SU_POST_CODE)
                                        ),
                                        new XElement("company",
                                            new XElement("name", companyInfo.CI_NAME),
                                            new XElement("phone", companyInfo.CI_PHONE),
                                            new XElement("postcode", companyInfo.CI_POST_CODE),
                                            new XElement("town", companyInfo.CI_TOWN),
                                            new XElement("street", companyInfo.CI_STREET)
                                        ),
                                        new XElement("date", order.OR_ADDED.ToShortDateString()),
                                        new XElement("items")
                                    )
                                );

            XElement itemsNode = xmlTree.Element("order").Element("items");
            int i = 1;
            foreach (var item in order.OE_OrderItems)
            {
                itemsNode.Add(new XElement("item",
                                new XElement("positionNumber", i),
                                new XElement("name", item.PR_Product.PR_NAME),
                                new XElement("quantityUnit", item.PR_Product.QT_QuantityType.QT_NAME),
                                new XElement("quantity", item.OE_QUANTITY)
                              )
                );
                i++;
            }
            XslCompiledTransform xslTransform = new XslCompiledTransform();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("WarehouseElectric.Helpers.OrderTransform.xslt");
            XmlWriter xmlWriter = XmlWriter.Create(filePath);

            XmlReader xmlReader = XmlReader.Create(stream);
            xslTransform.Load(xmlReader);
            try
            {
                xslTransform.Transform(xmlTree.CreateReader(), xmlWriter);
            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
        }

        #endregion
    }
}
