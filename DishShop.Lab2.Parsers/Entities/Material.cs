using DishShop.Lab2.Parsers.Entities.Base;
using System.ComponentModel;

namespace DishShop.Lab2.Parsers.Entities;

public class Material : Entity
{
    [DisplayName("Material")]
    public string Name { get; set; }
}