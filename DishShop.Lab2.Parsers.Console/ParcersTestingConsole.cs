using DishShop.Lab2.Parsers.DomParser.Mappers;
using DishShop.Lab2.Parsers.DomParser.Parser;
using DishShop.Lab2.Parsers.SaxParser.Mappers;
using DishShop.Lab2.Parsers.SaxParser.Parser;
using DishShop.Lab2.Parsers.Utils;

var dishPath =
    @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dish.xml";
var dishesPath =
    @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dishes.xml";
var newDishDomPath =
    @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\NewDishDom.xml";
var newDishesDomPath =
    @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\NewDishesDom.xml";
var newDishSaxPath =
    @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\NewDishSax.xml";
var newDishesSaxPath =
    @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\NewDishesSax.xml";


Console.WriteLine("-----DOM PARSER-----");

var parserDom = CreateDomParser();

Console.WriteLine("-----DOM PARSER: Parsing Dish.xml-----");

var dishDom = parserDom.GetDish(dishPath);

if (dishDom == null)
{
    Console.WriteLine("-----DOM PARSER: Parsing failure-----");
    foreach (var parserError in parserDom.DocumentHandler.Validator.ValidationErrors)
        Console.WriteLine(parserError);
}

else
{
    Console.WriteLine(dishDom.ToString());
}

Console.WriteLine($"-----DOM PARSER: Writing Dish to {newDishDomPath}-----");

if (!parserDom.SetDish(newDishDomPath, dishDom))
{
    Console.WriteLine("-----DOM PARSER: Writing failure-----");
    foreach (var validationError in parserDom.DocumentHandler.Validator.ValidationErrors)
        Console.WriteLine(validationError);
}
else
{
    Console.WriteLine($"-----DOM PARSER: Writing Dish to {newDishDomPath} is successful-----");
}


Console.WriteLine("-----DOM PARSER: Parsing Dishes.xml-----");

var dishesDom = parserDom.GetDishes(dishesPath);

if (dishesDom == null)
{
    Console.WriteLine("-----DOM PARSER: Parsing failure-----");
    foreach (var parserError in parserDom.DocumentHandler.Validator.ValidationErrors)
        Console.WriteLine(parserError);
}

else
{
    foreach (var dish in dishesDom)
        Console.WriteLine(dish.ToString());
}

Console.WriteLine($"-----DOM PARSER: Writing Dishes to {newDishesDomPath}-----");

if (!parserDom.SetDishes(newDishesDomPath, dishesDom))
{
    Console.WriteLine("-----DOM PARSER: Writing failure-----");
    foreach (var validationError in parserDom.DocumentHandler.Validator.ValidationErrors)
        Console.WriteLine(validationError);
}
else
{
    Console.WriteLine($"-----DOM PARSER: Writing Dishes to {newDishesDomPath} is successful-----");
}

Console.WriteLine("-----SAX PARSER-----");

var parserSax = CreateSaxParser();

Console.WriteLine("-----SAX PARSER: Parsing Dish.xml-----");

var dishSax = parserSax.GetDish(dishPath);
if (dishSax == null)
{
    Console.WriteLine("-----SAX PARSER: Parsing failure-----");
    foreach (var parserError in parserSax.DocumentHandler.Validator.ValidationErrors)
        Console.WriteLine(parserError);
}

else
{
    Console.WriteLine(dishSax.ToString());
}

Console.WriteLine($"-----SAX PARSER: Writing Dish to {newDishSaxPath}-----");

if (!parserSax.SetDish(newDishSaxPath, dishSax))
{
    Console.WriteLine("-----SAX PARSER: Writing failure-----");
    foreach (var validationError in parserSax.DocumentHandler.Validator.ValidationErrors)
        Console.WriteLine(validationError);
}
else
{
    Console.WriteLine($"-----SAX PARSER: Writing Dish to {newDishSaxPath} is successful-----");
}

Console.WriteLine("-----SAX PARSER: Parsing Dishes.xml-----");

var dishesSax = parserSax.GetDishes(dishesPath);

if (dishesSax == null)
{
    Console.WriteLine("-----SAX PARSER: Parsing failure-----");
    foreach (var parserError in parserSax.DocumentHandler.Validator.ValidationErrors)
        Console.WriteLine(parserError);
}
else
{
    foreach (var dish in dishesSax) Console.WriteLine(dish.ToString());
}

Console.WriteLine($"-----SAX PARSER: Writing Dishes to {newDishesSaxPath}-----");

if (!parserSax.SetDishes(newDishesSaxPath, dishesSax))
{
    Console.WriteLine("-----SAX PARSER: Writing failure-----");
    foreach (var validationError in parserSax.DocumentHandler.Validator.ValidationErrors)
        Console.WriteLine(validationError);
}
else
{
    Console.WriteLine($"-----DOM PARSER: Writing Dishes to {newDishesSaxPath} is successful-----");
}

DomParser CreateDomParser()
{
    var mapperDom = new XmlDomDishMapper();
    var validatorDom = new XmlValidator();
    var getterDom = new XmlDocumentHandler(validatorDom);
    return new DomParser(getterDom, mapperDom);
}

SaxParser CreateSaxParser()
{
    var mapperSax = new XmlSaxDishMapper();
    var validatorSax = new XmlValidator();
    var getterSax = new XmlDocumentHandler(validatorSax);
    return new SaxParser(getterSax, mapperSax);
}