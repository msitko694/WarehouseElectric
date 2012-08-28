<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <html>
      <head>
        <style type="text/css">
          table,th,td
          {
          width: 1024;
          clear: right;
          border: 1px solid black;
          border-collapse: collapse;
          }
          th
          {
          background: #F0F0F0;
          }
          .companyDiv
          {
          float:left;
          }
          .companyParagraph
          {
          border: 1px solid black;
          border-collapse: collapse;
          text-align: left;
          padding:10px;
          }
          .supplierDiv
          {
          float:right;
          }
          .supplierParagraph
          {
          border: 1px solid black;
          border-collapse: collapse;
          text-align: left;
          padding:10px;
          }
          body
          {
          width: 1024;
          }
        </style>
      </head>
      <body>
        <div class="companyDiv">
          <p class="companyParagraph">
            <b>Zamawiający: </b><xsl:value-of select="order/company/name"/>
            <br/>
            <b>Adres: </b>
            <xsl:value-of select="order/company/postcode"/>&#160;
            <xsl:value-of select="order/company/town"/>,&#160;
            ul. <xsl:value-of select="order/company/street"/>
            <br/>
            <b>Telefon: </b>
            <xsl:value-of select="order/company/phone"/> &#160;
          </p>
        </div>
        <div class="supplierDiv">
          <p class="supplierParagraph">
            <b>Dostawca: </b><xsl:value-of select="order/supplier/name"/>
            <br/>
            <b>Adres: </b>
            <xsl:value-of select="order/supplier/postcode"/>&#160;
            <xsl:value-of select="order/supplier/town"/>,&#160;
            ul. <xsl:value-of select="order/supplier/street"/>
            <br/>
            <b>Telefon: </b>
            <xsl:value-of select="order/supplier/phone"/> &#160;
          </p>
        </div>
        <br/>
        <table>
          <tr>
            <th>L.p.</th>
            <th>Nazwa produktu</th>
            <th>Ilość</th>
            <th>Jednostka miary</th>
          </tr>
          <xsl:for-each select="order/items/item">
            <tr>
              <td>
                <xsl:value-of select="positionNumber"/>
              </td>
              <td>
                <xsl:value-of select="name"/>
              </td>
              <td>
                <xsl:value-of select="quantity"/>
              </td>
              <td>
                <xsl:value-of select="quantityUnit"/>
              </td>
            </tr>
          </xsl:for-each>
        </table>
        <br/>
        <b>Data zamówienia: </b>
        <xsl:value-of select="order/date"/>
        <br/>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
