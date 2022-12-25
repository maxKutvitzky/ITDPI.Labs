using DishShop.Lab2.Parsers.Entities.Base;
using System.ComponentModel;

namespace DishShop.Lab2.Parsers.Entities;

public class Color : Entity
{
    [DisplayName("Color")]
    public string Name { get; set; }

    public string HexValue { get; set; }
}