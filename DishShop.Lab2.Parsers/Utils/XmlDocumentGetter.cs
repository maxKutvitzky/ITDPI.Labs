using System.Xml;
using System.Xml.Schema;
using DishShop.Lab2.Parsers.Interfaces;

namespace DishShop.Lab2.Parsers.Utils;

public class XmlDocumentGetter : IDishShopMarshalUtils
{
    public XmlDocumentGetter(IDishShopValidator validator)
    {
        Validator = validator;
    }

    public IDishShopValidator Validator { get; }

    public XmlTextReader GetReaderWithXml(string path, string schemaPath)
    {
        var schema = GetSchema(schemaPath);
        if (schema == null) return null;

        if (!Validator.IsValid(path, schema)) return null;
        var reader = new XmlTextReader(path);
        return reader;
    }

    public XmlSchema GetSchema(string path)
    {
        Validator.ValidationErrors.Clear();
        var schema = new XmlSchema();
        try
        {
            var reader = new XmlTextReader(path);
            schema = XmlSchema.Read(reader, Validator.ValidateCallBack);
        }
        catch (Exception e)
        {
            Validator.ValidationErrors.Add(e.Message);
        }

        if (Validator.ValidationErrors.Any()) return null;
        return schema;
    }
}