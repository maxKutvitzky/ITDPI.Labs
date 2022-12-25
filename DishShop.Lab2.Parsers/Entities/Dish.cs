using System.ComponentModel;
using System.Text;
using DishShop.Lab2.Parsers.Entities.Base;

namespace DishShop.Lab2.Parsers.Entities;

public class Dish : Entity
{
    [DisplayName("Name")]
    public string Name { get; set; }
    [DisplayName("Price")]
    public decimal Price { get; set; }
    [DisplayName("Volume")]
    public int Volume { get; set; }

    public List<Category> Categories { get; set; } = new();
    public Color Color { get; set; } = new();

    public Material Material { get; set; } = new();

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Id: {Id}")
            .AppendLine($"Name: {Name}")
            .AppendLine($"Price: {Price}")
            .AppendLine($"Volume: {Volume}")
            .AppendLine("Color:")
            .AppendLine($"  Id: {Color.Id}")
            .AppendLine($"  Name: {Color.Name}")
            .AppendLine($"  HexValue: {Color.HexValue}")
            .AppendLine("Material:")
            .AppendLine($"  Id: {Material.Id}")
            .AppendLine($"  Name: {Material.Name}")
            .AppendLine("Categories");

        foreach (var category in Categories)
            builder.AppendLine($"  Id: {category.Id}")
                .AppendLine($"  Name: {category.Name}");

        return builder.ToString();
    }
}