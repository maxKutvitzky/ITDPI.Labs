using System.Text;
using System.Xml;
using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab2.Parsers.Interfaces;

namespace DishShop.Lab2.Parsers.SaxParser.Mappers;

public class XmlSaxDishMapper : IDishShopMarshalMapper<Dish>
{
    public Dish XmlToObject(XmlTextReader Xml)
    {
        var dish = new Dish();
        var elementName = string.Empty;
        while (Xml.Read())
        {
            if (Xml.NodeType == XmlNodeType.Element)
            {
                elementName = Xml.Name;
                MapElementAttributeToProperty(elementName, dish, Xml);
            }

            if (Xml.NodeType == XmlNodeType.Text) MapElementValueToProperty(elementName, dish, Xml);
        }

        Xml.Close();
        return dish;
    }

    public List<Dish> XmlToObjects(XmlTextReader Xml)
    {
        var dishes = new List<Dish>();
        var elementName = string.Empty;
        while (Xml.Read())
        {
            if (Xml.NodeType == XmlNodeType.Element && Xml.Name == "Dishes") continue;
            if (Xml.NodeType == XmlNodeType.Element && Xml.Name == "Dish")
            {
                dishes.Add(new Dish());
                elementName = Xml.Name;
                MapElementAttributeToProperty(elementName, dishes[^1], Xml);
                continue;
            }

            if (Xml.NodeType == XmlNodeType.Element)
            {
                elementName = Xml.Name;
                MapElementAttributeToProperty(elementName, dishes[^1], Xml);
            }

            if (Xml.NodeType == XmlNodeType.Text) MapElementValueToProperty(elementName, dishes[^1], Xml);
        }

        Xml.Close();
        return dishes;
    }

    public string ObjectToXml(Dish obj)
    {
        var builder = new StringBuilder();
        builder.AppendLine($@"<?xml version=""1.0"" encoding=""utf-8""?>
<?xml-stylesheet type=""text/xsl"" href=""DishTest.xslt""?>
<Dish id=""{obj.Id}""
      xmlns=""http://dishShop/Dish""
      xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
      xsi:schemaLocation=""http://dishShop/Dish Dish.xsd""
      xmlns:mat=""http://dishShop/Material""
      xmlns:col=""http://dishShop/Color""
      xmlns:cat=""http://dishShop/Category"">
	<Name>{obj.Name}</Name>
	<Volume>{obj.Volume}</Volume>
	<Price>{obj.Price.ToString().Replace(',', '.')}</Price>
	<Material id=""{obj.Material.Id}"">
		<mat:Name>{obj.Material.Name}</mat:Name>
	</Material>
	<Color id=""{obj.Color.Id}"">
		<col:HexValue>{obj.Color.HexValue}</col:HexValue>
		<col:Name>{obj.Color.Name}</col:Name>
	</Color>
<Categories>");
        foreach (var category in obj.Categories)
            builder.AppendLine($@"
        <cat:Category id=""{category.Id}"" >
			<cat:Name>{category.Name}</cat:Name>
		</cat:Category>");

        builder.AppendLine("</Categories>\r\n</Dish>");
        return builder.ToString();
    }

    public string ObjectsToXml(List<Dish> list)
    {
        var builder = new StringBuilder();
        builder.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8""?>
<Dishes xmlns=""http://dishShop/Dish""
        xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
        xsi:schemaLocation=""http://dishShop/Dish Dish.xsd""
        xmlns:mat=""http://dishShop/Material""
        xmlns:col=""http://dishShop/Color""
        xmlns:cat=""http://dishShop/Category"">");
        foreach (var obj in list)
        {
            builder.AppendLine($@"<Dish id=""{obj.Id}"">
	<Name>{obj.Name}</Name>
	<Volume>{obj.Volume}</Volume>
	<Price>{obj.Price.ToString().Replace(',', '.')}</Price>
	<Material id=""{obj.Material.Id}"">
		<mat:Name>{obj.Material.Name}</mat:Name>
	</Material>
	<Color id=""{obj.Color.Id}"">
		<col:HexValue>{obj.Color.HexValue}</col:HexValue>
		<col:Name>{obj.Color.Name}</col:Name>
	</Color>
<Categories>");
            foreach (var category in obj.Categories)
                builder.AppendLine($@"
        <cat:Category id=""{category.Id}"" >
			<cat:Name>{category.Name}</cat:Name>
		</cat:Category>");

            builder.AppendLine("</Categories>\r\n</Dish>");
        }

        builder.AppendLine(@"</Dishes>");
        return builder.ToString();
    }

    private void MapElementValueToProperty(string elementName, Dish dish, XmlTextReader Xml)
    {
        switch (elementName)
        {
            case "Name":
                dish.Name = Xml.Value;
                break;
            case "Volume":
                dish.Volume = int.Parse(Xml.Value);
                break;
            case "Price":
                dish.Price = decimal.Parse(Xml.Value.Replace('.', ','));
                break;
            case "mat:Name":
                dish.Material.Name = Xml.Value;
                break;
            case "col:HexValue":
                dish.Color.HexValue = Xml.Value;
                break;
            case "col:Name":
                dish.Color.Name = Xml.Value;
                break;
            case "cat:Name":
                dish.Categories[^1].Name = Xml.Value;
                break;
        }
    }

    private void MapElementAttributeToProperty(string elementName, Dish dish, XmlTextReader Xml)
    {
        switch (Xml.Name)
        {
            case "Dish":
                dish.Id = int.Parse(Xml.GetAttribute("id"));
                break;
            case "Material":
                dish.Material.Id = int.Parse(Xml.GetAttribute("id"));
                break;
            case "Color":
                dish.Color.Id = int.Parse(Xml.GetAttribute("id"));
                break;
            case "cat:Category":
                dish.Categories.Add(new Category { Id = int.Parse(Xml.GetAttribute("id")) });
                break;
        }
    }
}