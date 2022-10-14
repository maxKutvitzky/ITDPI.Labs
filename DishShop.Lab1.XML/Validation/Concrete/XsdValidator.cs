using DishShop.Lab1.XML.Validation.Interfaces;
using System.Xml;
using System.Xml.Schema;

namespace DishShop.Lab1.XML.Validation.Concrete;

public class XsdValidator : IValidator
{
    public List<string> ValidationErrors { get; set; } = new List<string>();
    public bool IsValid(string path, string scheme)
    {
        try
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.Schemas.Add(null, scheme);
            settings.Schemas.XmlResolver = new XmlUrlResolver();
            settings.ValidationEventHandler += (sender, args) => { ValidationErrors.Add(args.Message); };

            XmlReader reader = XmlReader.Create(path, settings);

            while (reader.Read()) ;
        }
        catch (Exception e)
        {
            ValidationErrors.Add(e.Message);
            return false;
        }
        return !ValidationErrors.Any();
    }
}