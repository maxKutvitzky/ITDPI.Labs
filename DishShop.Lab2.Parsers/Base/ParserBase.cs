using DishShop.Lab2.Parsers.Entities;
using DishShop.Lab2.Parsers.Interfaces;

namespace DishShop.Lab2.Parsers.Base;

public abstract class ParserBase
{
    public const string DishSchemaPath =
        @"D:\UNIVER\3 Course\ITROI\Project\DishShop\DishShop.Lab1.XML\XML\Dish.xsd";

    protected ParserBase(IDishShopMarshalUtils documentHandler, IDishShopMarshalMapper<Dish> dishMapper)
    {
        DocumentHandler = documentHandler;
        DishMapper = dishMapper;
    }

    public IDishShopMarshalMapper<Dish> DishMapper { get; }
    public IDishShopMarshalUtils DocumentHandler { get; }
}