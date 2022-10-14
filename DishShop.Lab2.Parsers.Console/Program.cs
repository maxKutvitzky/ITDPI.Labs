using DishShop.Lab2.Parsers.DomParser.Parser;

DomParser parser = new DomParser();

var parsed = parser.GetDish(@"D:\UNIVER\3 Course\ITROI\Project\DishShop\Dish.xml");

if (parsed == null)
{
    foreach (var parserError in parser.Errors)
    {
        Console.WriteLine(parserError);
    }
}
else
{
    Console.WriteLine(parsed.ToString());
}


