using DishShop.Lab2.Parsers.Entities;

namespace DishShop.Lab2.Parsers.Interfaces;

public interface IDishShopMarshalGet
{
    List<Dish> GetDishes(string path);
    Dish GetDish(string path);
}