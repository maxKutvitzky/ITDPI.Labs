using DishShop.Lab2.Parsers.DomParser.Mappers;
using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab2.Parsers.Utils;
using DishShop.Lab3_4.API.Utils;
using System.Text;

string outputFilePath = PathUtils.internalStorageDirectory + "testDishes.xml";
string schemaPath = @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dish.xsd";

Console.WriteLine("Press any key");

Console.ReadLine();

HttpClient httpClient = new HttpClient();

httpClient.BaseAddress = new Uri(@"https://localhost:7209");

HttpResponseMessage responseMessage = await httpClient.GetAsync(@"/dishshop/DishShop/getAll");

XmlDomDishMapper mapper = new XmlDomDishMapper();

XmlValidator validator = new XmlValidator();

XmlDocumentHandler documentHandler = new XmlDocumentHandler(validator);

if(!documentHandler.SaveDocument(outputFilePath, await responseMessage.Content.ReadAsStringAsync(), schemaPath))
{
	foreach (var error in documentHandler.Validator.ValidationErrors)
	{
		Console.WriteLine(error);
	}
}
else
{
    List<Dish> dishes = mapper.XmlToObjects(documentHandler.GetReaderWithXml(outputFilePath, schemaPath));

    foreach (Dish dish in dishes)
    {
        Console.WriteLine(dish.ToString());
    }

    Dish dish1 = new Dish()
    {
        Id = 10,
        Categories = dishes.First().Categories,
        Color = dishes.First().Color,
        Material = dishes.First().Material,
        Name = "NewDish",
        Price = 10.99M,
        Volume = 200
    };

    string dishToSend = mapper.ObjectToXml(dish1);

    var content = new StringContent(dishToSend, Encoding.UTF8, "application/xml");

    HttpResponseMessage postMessage = await httpClient.PostAsync(@"/dishshop/DishShop/postDish", content);
}

Console.ReadLine();