using System.Xml;
using System.Xml.Schema;

namespace DishShop.Lab2.Parsers.Interfaces;

public interface IDishShopMarshalUtils
{
    public IDishShopValidator Validator { get; }
    XmlTextReader GetReaderWithXml(string path, string schemaPath);
    XmlSchema GetSchema(string path);
}