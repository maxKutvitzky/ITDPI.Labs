using DishShop.Lab2.Parsers.Base;
using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab2.Parsers.Interfaces;

namespace DishShop.Lab2.Parsers.SaxParser.Parser;

public class SaxParser : ParserBase, IDishShopParser
{
    public SaxParser(
        IDishShopMarshalUtils documentHandler,
        IDishShopMarshalMapper<Dish> dishMapper) :
        base(documentHandler, dishMapper)
    {
    }

    public List<Dish> GetDishes(string path)
    {
        var reader = DocumentHandler.GetReaderWithXml(path, DishSchemaPath);
        if (reader == null) return null;
        try
        {
            var dishes = DishMapper.XmlToObjects(reader);
            return dishes;
        }
        catch (Exception e)
        {
            DocumentHandler.Validator.ValidationErrors.Add("Failed to map XML to dishes");
            return null;
        }
    }

    public Dish GetDish(string path)
    {
        var reader = DocumentHandler.GetReaderWithXml(path, DishSchemaPath);
        if (reader == null) return null;
        try
        {
            var dish = DishMapper.XmlToObject(reader);
            return dish;
        }
        catch (Exception e)
        {
            DocumentHandler.Validator.ValidationErrors.Add("Failed to map XML to dish");
            return null;
        }
    }

    public bool SetDishes(string path, List<Dish> dishes)
    {
        var xml = DishMapper.ObjectsToXml(dishes);
        return DocumentHandler.SaveDocument(path, xml, DishSchemaPath);
    }

    public bool SetDish(string path, Dish dish)
    {
        var xml = DishMapper.ObjectToXml(dish);
        return DocumentHandler.SaveDocument(path, xml, DishSchemaPath);
    }
}