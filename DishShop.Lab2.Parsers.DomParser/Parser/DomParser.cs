using System.Xml;
using System.Xml.Schema;
using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab2.Parsers.Interfaces;

namespace DishShop.Lab2.Parsers.DomParser.Parser
{
    public class DomParser : IDishShopParser
    {
        public List<string> Errors = new List<string>();
        public List<Dish> GetDishes(string path)
        {
            throw new NotImplementedException();
        }

        public Dish GetDish(string path)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(path);
            }
            catch (FileNotFoundException e)
            {
                Errors.Add(e.Message);
            }

            if (!ValidateXml(@"D:\UNIVER\3 Course\ITROI\Project\DishShop\Dish.xsd", doc))
            {
                return null;
            }
            var parsedValue = doc.DocumentElement;

            Dish dish = new Dish()
            {
                Id = Int32.Parse(parsedValue.Attributes[0].Value),
                Name = parsedValue["Name"].InnerText,
                Volume = Int32.Parse(parsedValue["Volume"].InnerText),
                Price = Decimal.Parse(parsedValue["Price"].InnerText.Replace('.',',')),
            };

            Material material = new Material()
            {
                Id = Int32.Parse(parsedValue["Material"].Attributes[0].Value),
                Name = parsedValue["Material"].InnerText
            };

            List<Category> categories = new List<Category>();
            for (int i = 0; i < parsedValue["Categories"].ChildNodes.Count; ++i)
            {
                categories.Add(new()
                {
                    Id = Int32.Parse(parsedValue["Categories"].ChildNodes.Item(i).Attributes[0].Value),
                    Name = parsedValue["Categories"].ChildNodes.Item(i).InnerText
                });
            }

            Color color = new Color();
            color.Id = Int32.Parse(parsedValue["Color"].Attributes[0].Value);
            for (int i = 0; i < parsedValue["Color"].ChildNodes.Count; ++i)
            {
                if (parsedValue["Color"].ChildNodes.Item(i).Name == "Name")
                {
                    color.Name = parsedValue["Color"].ChildNodes.Item(i).InnerText;
                }

                color.HexValue = parsedValue["Color"].ChildNodes.Item(i).InnerText;
            }

            dish.Categories = categories;
            dish.Color = color;
            dish.Material = material;
            return dish;
        }
        public Category GetCategory(string path)
        {
            throw new NotImplementedException();
        }

        public Color GetColor(string path)
        {
            throw new NotImplementedException();
        }

        public Material GetMaterial(string path)
        {
            throw new NotImplementedException();
        }

        public void SetDishes(string path, List<Dish> dishes)
        {
            throw new NotImplementedException();
        }

        public void SetDish(string path, Dish dish)
        {
            throw new NotImplementedException();
        }

        public void SetColor(string path, Color color)
        {
            throw new NotImplementedException();
        }

        public void SetMaterial(string path, Material material)
        {
            throw new NotImplementedException();
        }

        public void SetCategory(string path, Category category)
        {
            throw new NotImplementedException();
        }

        private bool ValidateXml(string schemaPath, XmlDocument doc)
        {
            XmlTextReader reader = new XmlTextReader(schemaPath);
            XmlSchema schema = XmlSchema.Read(reader, ValidateCallBack);
            if (schema == null)
            {
                Errors.Add("Schema is null");
                return false;
            }
            doc.XmlResolver = new XmlUrlResolver();
            doc.Schemas.XmlResolver = new XmlUrlResolver();
            doc.Schemas.Add(schema);
            doc.Validate(ValidateCallBack);
            return !Errors.Any();
        }

        private void ValidateCallBack(object sender, ValidationEventArgs args)
        {
            Errors.Add(args.Message);
        }
    }
}
