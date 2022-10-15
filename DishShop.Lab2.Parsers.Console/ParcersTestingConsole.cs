using DishShop.Lab2.Parsers.DomParser.Mappers;
using DishShop.Lab2.Parsers.DomParser.Parser;
using DishShop.Lab2.Parsers.Utils;

var mapper = new XmlDomDishMapper();
var validator = new XmlValidator();
var getter = new XmlDocumentGetter(validator);

var parser = new DomParser(getter, mapper);

var parsed = parser.GetDish(@"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dish.xml");

if (parsed == null)
    foreach (var parserError in parser.DocumentGetter.Validator.ValidationErrors)
        Console.WriteLine(parserError);
else
    Console.WriteLine(parsed.ToString());

//parser.SetDish(@"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\NewDish.xml", parsed);

var parsedList = parser.GetDishes(@"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dishes.xml");

if (parsedList == null)
    foreach (var parserError in parser.DocumentGetter.Validator.ValidationErrors)
        Console.WriteLine(parserError);
else
    foreach (var dish in parsedList)
        Console.WriteLine(dish.ToString());

//parser.SetDishes(@"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\NewDishes.xml", parsedList);