﻿using System.Text;
using DishShop.Lab2.Parsers.Entities.Base;

namespace DishShop.Lab2.Parsers.Entities
{
    public class Dish : Entity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Volume { get; set; }

        public List<Category> Categories { get; set; }

        public Color Color { get; set; }

        public Material Material { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
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

            foreach (Category category in Categories)
            {
                builder.AppendLine($"  Id: {category.Id}")
                    .AppendLine($"  Name: {category.Name}");
            }

            return builder.ToString();
        }
    }
}