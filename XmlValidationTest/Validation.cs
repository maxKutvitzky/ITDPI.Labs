using XmlValidator;

Dictionary<string, string> filesToValidate = new Dictionary<string, string>()
{
    { @"D:\UNIVER\3 Course\ITROI\Project\DishShop\Dish.xml", @"D:\UNIVER\3 Course\ITROI\Project\DishShop\Dish.xsd" },
    { @"D:\UNIVER\3 Course\ITROI\Project\DishShop\Dishes.xml", @"D:\UNIVER\3 Course\ITROI\Project\DishShop\Dish.xsd" }
};

foreach (var item in filesToValidate)
{
    Validate(item.Key, item.Value);
}

void Validate(string path, string scheme)
{
    XsdValidator validator = new XsdValidator();

    if (!validator.IsValid(path, scheme))
    {
        Console.WriteLine($"Document {path} is invalid:");
        foreach (var error in validator.ValidationErrors)
        {
            Console.WriteLine(error);
        }
        return;
    }
    Console.WriteLine($"Document {path} is valid");
}