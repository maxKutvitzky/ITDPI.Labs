using DishShop.Lab2.Parsers.DomParser.Mappers;
using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab2.Parsers.Utils;
using DishShop.Lab3_4.API.Utils;
using System.Text;

namespace DishShop.Lab5.WebApp.ApiWrapper
{
    public class ApiWrapper
    {
        private readonly HttpClient _httpClient = new HttpClient() {BaseAddress = new Uri(@"https://localhost:7209") };

        public async Task<HttpResponseMessage> SetDish(Dish dish)
        {
            XmlDomDishMapper mapper = new XmlDomDishMapper();

            string xml = mapper.ObjectToXml(dish);

            return await _httpClient.PostAsync(@"/dishshop/DishShop/postDish", new StringContent(xml, Encoding.UTF8, "application/xml"));
        }
        public async Task<List<Dish>> GetDishes()
        {
            Random random = new Random();

            string outputFilePath = PathUtils.internalStorageDirectory + $"Dishes{random.Next()}.xml";
            string schemaPath = @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dish.xsd";

            XmlDomDishMapper mapper = new XmlDomDishMapper();

            XmlValidator validator = new XmlValidator();

            XmlDocumentHandler documentHandler = new XmlDocumentHandler(validator);

            HttpResponseMessage responseMessage = await _httpClient.GetAsync(@"/dishshop/DishShop/getAll");

            if (!documentHandler.SaveDocument(outputFilePath, await responseMessage.Content.ReadAsStringAsync(), schemaPath))
            {
                return null;
            }
            else
            {
                return mapper.XmlToObjects(documentHandler.GetReaderWithXml(outputFilePath, schemaPath));
            }
        }
    }
}
