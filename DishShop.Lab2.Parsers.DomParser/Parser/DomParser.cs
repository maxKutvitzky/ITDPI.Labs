using System.Xml;
using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab2.Parsers.Interfaces;

namespace DishShop.Lab2.Parsers.DomParser.Parser;

public class DomParser : IDishShopParser
{
    private const string DishSchemaPath =
        @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dish.xsd";

    private readonly IDishShopMarshalMapper<Dish> _dishMapper;
    public readonly IDishShopMarshalUtils DocumentGetter;

    public DomParser(IDishShopMarshalUtils documentGetter, IDishShopMarshalMapper<Dish> dishMapper)
    {
        DocumentGetter = documentGetter;
        _dishMapper = dishMapper;
    }

    public List<Dish> GetDishes(string path)
    {
        var doc = new XmlDocument();
        var reader = DocumentGetter.GetReaderWithXml(path, DishSchemaPath);
        if (reader == null) return null;
        try
        {
            var dishes = _dishMapper.XmlToObjects(reader);
            return dishes;
        }
        catch (Exception e)
        {
            DocumentGetter.Validator.ValidationErrors.Add("Failed to map XML to dishes");
            return null;
        }
    }

    public Dish GetDish(string path)
    {
        var doc = new XmlDocument();
        var reader = DocumentGetter.GetReaderWithXml(path, DishSchemaPath);
        if (reader == null) return null;
        try
        {
            var dish = _dishMapper.XmlToObject(reader);
            return dish;
        }
        catch (Exception e)
        {
            DocumentGetter.Validator.ValidationErrors.Add("Failed to map XML to dish");
            return null;
        }
    }

    public void SetDishes(string path, List<Dish> dishes)
    {
        var document = new XmlDocument();
        var xml = _dishMapper.ObjectsToXml(dishes);
        document.LoadXml(xml);
        document.Save(path);
    }

    public void SetDish(string path, Dish dish)
    {
        var document = new XmlDocument();
        var xml = _dishMapper.ObjectToXml(dish);
        document.LoadXml(xml);
        document.Save(path);
    }
}