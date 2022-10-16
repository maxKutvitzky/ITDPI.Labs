using System.Xml.Schema;

namespace DishShop.Lab2.Parsers.Interfaces;

public interface IDishShopValidator
{
    public List<string> ValidationErrors { get; set; }
    bool IsValid(string path, XmlSchema schema);
    bool IsValidString(string xml, XmlSchema schema);
    void ValidateCallBack(object sender, ValidationEventArgs args);
}