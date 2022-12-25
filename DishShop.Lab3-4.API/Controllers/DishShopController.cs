using DishShop.Lab2.Parsers.DomParser.Mappers;
using DishShop.Lab2.Parsers.DomParser.Parser;
using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab2.Parsers.Utils;
using DishShop.Lab3_4.API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DishShop.Lab3_4.API.Controllers
{
    [ApiController]
    [Route("dishshop/[controller]")]
    public class DishShopController
    {
        private readonly DomParser _parser;

        public DishShopController()
        {
            var mapperDom = new XmlDomDishMapper();
            var validatorDom = new XmlValidator();
            var getterDom = new XmlDocumentHandler(validatorDom);
            _parser = new DomParser(getterDom, mapperDom);
        }

        [HttpGet]
        [Route("getAll")]
        public ActionResult<string> GetAll() 
        {
            List<Dish> dishes = _parser.GetDishes(PathUtils.internalDishesPath);

            return _parser.DishMapper.ObjectsToXml(dishes);
        }
        
        [HttpPost]
        [Route("postDish")]
        public ActionResult AddDish(string xml)
        {
            Random random = new Random();
            string path = PathUtils.internalStorageDirectory + $@"newDish{random.Next()}.xml";

            XmlDocumentHandler documentHandler = new XmlDocumentHandler(new XmlValidator());

            if (!documentHandler.SaveDocument(path, xml, DomParser.DishSchemaPath))
            {
                string errorMessages = string.Empty;

                foreach (var parserError in documentHandler.Validator.ValidationErrors)
                    errorMessages += parserError+"\n";

                return new BadRequestObjectResult(errorMessages);
            }

            return new OkResult();
        }
    }
}
