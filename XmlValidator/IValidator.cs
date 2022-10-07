namespace XmlValidator;

public interface IValidator
{
    bool IsValid(string path, string scheme);
}