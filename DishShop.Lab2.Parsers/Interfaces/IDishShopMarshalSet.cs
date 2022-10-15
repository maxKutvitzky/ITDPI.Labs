using DishShop.Lab2.Parsers.Entities;

namespace DishShop.Lab2.Parsers.Interfaces;

public interface IDishShopMarshalSet
{
    void SetDishes(string path, List<Dish> dishes);
    void SetDish(string path, Dish dish);
}