using System.Text;
using System.Xml;
using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab2.Parsers.Interfaces;

namespace DishShop.Lab2.Parsers.DomParser.Mappers;

public class XmlDomDishMapper : IDishShopMarshalMapper<Dish>
{
    public Dish XmlToObject(XmlTextReader Xml)
    {
        var doc = new XmlDocument();
        doc.Load(Xml);
        var parsedValue = doc.DocumentElement;

        var dish = new Dish
        {
            Id = int.Parse(parsedValue.Attributes[0].Value),
            Name = parsedValue["Name"].InnerText,
            Volume = int.Parse(parsedValue["Volume"].InnerText),
            Price = decimal.Parse(parsedValue["Price"].InnerText.Replace('.', ','))
        };

        var material = new Material
        {
            Id = int.Parse(parsedValue["Material"].Attributes[0].Value),
            Name = parsedValue["Material"].InnerText
        };

        var categories = new List<Category>();
        for (var i = 0; i < parsedValue["Categories"].ChildNodes.Count; ++i)
            categories.Add(new Category
            {
                Id = int.Parse(parsedValue["Categories"].ChildNodes.Item(i).Attributes[0].Value),
                Name = parsedValue["Categories"].ChildNodes.Item(i).InnerText
            });

        var color = new Color();
        color.Id = int.Parse(parsedValue["Color"].Attributes[0].Value);
        for (var i = 0; i < parsedValue["Color"].ChildNodes.Count; ++i)
        {
            var test = parsedValue["Color"].ChildNodes.Item(i).Name;
            if (parsedValue["Color"].ChildNodes.Item(i).Name.Contains("Name"))
            {
                color.Name = parsedValue["Color"].ChildNodes.Item(i).InnerText;
                continue;
            }

            color.HexValue = parsedValue["Color"].ChildNodes.Item(i).InnerText;
        }

        dish.Categories = categories;
        dish.Color = color;
        dish.Material = material;
        return dish;
    }

    public List<Dish> XmlToObjects(XmlTextReader Xml)
    {
        var dishes = new List<Dish>();
        var doc = new XmlDocument();
        doc.Load(Xml);
        foreach (XmlNode parsedValue in doc.DocumentElement.ChildNodes)
        {
            var dish = new Dish
            {
                Id = int.Parse(parsedValue.Attributes[0].Value),
                Name = parsedValue["Name"].InnerText,
                Volume = int.Parse(parsedValue["Volume"].InnerText),
                Price = decimal.Parse(parsedValue["Price"].InnerText.Replace('.', ','))
            };

            var material = new Material
            {
                Id = int.Parse(parsedValue["Material"].Attributes[0].Value),
                Name = parsedValue["Material"].InnerText
            };

            var categories = new List<Category>();
            for (var i = 0; i < parsedValue["Categories"].ChildNodes.Count; ++i)
                categories.Add(new Category
                {
                    Id = int.Parse(parsedValue["Categories"].ChildNodes.Item(i).Attributes[0].Value),
                    Name = parsedValue["Categories"].ChildNodes.Item(i).InnerText
                });

            var color = new Color();
            color.Id = int.Parse(parsedValue["Color"].Attributes[0].Value);
            for (var i = 0; i < parsedValue["Color"].ChildNodes.Count; ++i)
            {
                if (parsedValue["Color"].ChildNodes.Item(i).Name.Contains("Name"))
                {
                    color.Name = parsedValue["Color"].ChildNodes.Item(i).InnerText;
                    continue;
                }

                color.HexValue = parsedValue["Color"].ChildNodes.Item(i).InnerText;
            }

            dish.Categories = categories;
            dish.Color = color;
            dish.Material = material;
            dishes.Add(dish);
        }


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
}