﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Xsl;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace WarehouseElectric.Helpers
{
    class HtmlExporter : IExporter
    {
        #region IExporter Members

        public void ExportInvoice(DataLayer.IN_Invoice invoice)
        {
            XDocument xmlTree= new XDocument(
                                    new XElement("invoice",
                                        new XElement("customer",
                                            new XElement("name", invoice.CU_Customer.CU_NAME),
                                            new XElement("phone", invoice.CU_Customer.CU_PHONE),
                                            new XElement("town", invoice.CU_Customer.CU_TOWN),
                                            new XElement("street", invoice.CU_Customer.CU_STREET),
                                            new XElement("postcode", invoice.CU_Customer.CU_POST_CODE)
                                        ),
                                        new XElement("date",  invoice.IN_ADDED.ToShortDateString()),
                                        new XElement("speditionCost", invoice.IN_SPEDITION_COST),
                                        new XElement("totalCost" , invoice.IN_TOTAL), 
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
            XmlWriter xmlWriter = XmlWriter.Create("test.html");

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
                
            xmlTree.Save("export.xml");
        }

        #endregion
    }
}