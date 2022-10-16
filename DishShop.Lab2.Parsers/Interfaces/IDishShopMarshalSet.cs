using DishShop.Lab2.Parsers.Entities;

namespace DishShop.Lab2.Parsers.Interfaces;

public interface IDishShopMarshalSet
{
    bool SetDishes(string path, List<Dish> dishes);
    bool SetDish(string path, Dish dish);
}