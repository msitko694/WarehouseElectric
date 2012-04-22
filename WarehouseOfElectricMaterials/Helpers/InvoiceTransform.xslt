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
				<div class="customerDiv">
					<p class="customerParagraph">
						<b>Nabywca: </b><xsl:value-of select="invoice/customer/name"/>
						<br/>
						<b>Adres: </b>	
						<xsl:value-of select="invoice/customer/postcode"/>, &#160;
						<xsl:value-of select="invoice/customer/town"/> &#160;
						ul. <xsl:value-of select="invoice/customer/street"/>
						<br/>
						<b>Telefon: </b>
						<xsl:value-of select="invoice/customer/phone"/> &#160;
						<xsl:value-of select="invoice/date"/>
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
			</body>
		</html>
	</xsl:template>

</xsl:stylesheet>
