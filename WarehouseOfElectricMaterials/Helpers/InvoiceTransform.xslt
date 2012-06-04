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
					.customerDiv
					{
					float:right;
					}
					.customerParagraph
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
						<b>Sprzedawca: </b><xsl:value-of select="invoice/company/name"/>
						<br/>
						<b>Adres: </b>
						<xsl:value-of select="invoice/company/postcode"/>&#160;
						<xsl:value-of select="invoice/company/town"/>,&#160;
						ul. <xsl:value-of select="invoice/company/street"/>
						<br/>
						<b>Telefon: </b>
						<xsl:value-of select="invoice/company/phone"/> &#160;
					</p>
				</div>
				<div class="customerDiv">
					<p class="customerParagraph">
						<b>Nabywca: </b><xsl:value-of select="invoice/customer/name"/>
						<br/>
						<b>Adres: </b>
						<xsl:value-of select="invoice/customer/postcode"/>&#160;
						<xsl:value-of select="invoice/customer/town"/>,&#160;
						ul. <xsl:value-of select="invoice/customer/street"/>
						<br/>
						<b>Telefon: </b>
						<xsl:value-of select="invoice/customer/phone"/> &#160;
					</p>
				</div>
				<br/>
				<table>
					<tr>
						<th>L.p.</th>
						<th>Nazwa produktu</th>
						<th>Ilość</th>
						<th>Jednostka miary</th>
						<th>Cena jednostki</th>
						<th>Cena całości (netto)</th>
						<th>Stawka VAT</th>
						<th>Kwota VAT</th>
						<th>Cena całości (brutto)</th>
					</tr>
					<xsl:for-each select="invoice/items/item">
						<tr>
							<td>
								<xsl:value-of select="positionNumber"/>
							</td>
							<td>
								<xsl:value-of select="name"/>
							</td>
							<td>
								<xsl:value-of select="quantityUnit"/>
							</td>
							<td>
								<xsl:value-of select="quantity"/>
							</td>
							<td>
								<xsl:value-of select="unitPrice"/>
							</td>
							<td>
								<xsl:value-of select="totalNettoPrice"/>
							</td>
							<td>
								<xsl:value-of select="vatRate"/>
							</td>
							<td>
								<xsl:value-of select="totalVat"/>
							</td>
							<td>
								<xsl:value-of select="totalBruttoPrice"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
				<br/>
				<b>Data wystawienia faktury: </b><xsl:value-of select="invoice/date"/>
				<br/>
				<b>Koszt wysyłki: </b><xsl:value-of select="invoice/speditionCost"/>
				<br/>
				<b>Do zapłaty: </b><xsl:value-of select="invoice/totalCost"/>
			</body>
		</html>
	</xsl:template>

</xsl:stylesheet>
