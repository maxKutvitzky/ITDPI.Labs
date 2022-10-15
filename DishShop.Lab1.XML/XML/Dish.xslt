<?xml version="1.0"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:mat="http://dishShop/Material"
                xmlns:col="http://dishShop/Color"
                xmlns:cat="http://dishShop/Category">
	<xsl:template match="/">
		<html>
			<body>
				<h1>Dish</h1>
				<table border="1">
					<tr bgcolor="#9acd32">
						<th>Attribute</th>
						<th>Value</th>
					</tr>
					<tr>
						<td>Name</td>
						<td>
							<xsl:value-of select="Name" />
						</td>
					</tr>
					<tr>
						<td>Price</td>
						<td>
							<xsl:value-of select="Price" />
						</td>
					</tr>
					<tr>
						<td>Volume</td>
						<td>
							<xsl:value-of select="Volume" />
						</td>
					</tr>

				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>