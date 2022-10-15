using DishShop.Lab1.XML.Validation.Concrete;

var filesToValidate = new Dictionary<string, string>
{
    {
        @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dish.xml",
        @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dish.xsd"
    },
    {
        @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dishes.xml",
        @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dish.xsd"
    }
};

foreach (var item in filesToValidate) Validate(item.Key, item.Value);

void Validate(string path, string scheme)
{
    var validator = new XsdValidator();

    if (!validator.IsValid(path, scheme))
    {
        Console.WriteLine($"Document {path} is invalid:");
        foreach (var error in validator.ValidationErrors) Console.WriteLine(error);
        return;
    }

    Console.WriteLine($"Document {path} is valid");
}