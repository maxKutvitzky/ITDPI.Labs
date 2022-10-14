namespace DishShop.Lab1.XML.Validation.Interfaces;

public interface IValidator
{
    bool IsValid(string path, string scheme);
}