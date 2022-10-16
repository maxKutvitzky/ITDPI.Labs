using System.Xml;
using System.Xml.Schema;

namespace DishShop.Lab2.Parsers.Interfaces;

public interface IDishShopMarshalUtils
{
    IDishShopValidator Validator { get; }
    bool SaveDocument(string path, string xml, string schemaPath);
    XmlTextReader GetReaderWithXml(string path, string schemaPath);
    XmlSchema GetSchema(string path);
}