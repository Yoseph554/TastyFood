using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyFood
{
    internal class Class1
    {
        public List<MenuItem> Menu { get; set; }

        public Class1()
        {
            Menu = new List<MenuItem>
            {
                new MenuItem { Name = "Combo 1", Price = 125 },
                new MenuItem { Name = "Combo 2", Price = 130 },
                new MenuItem { Name = "Pechuga", Price = 45 },
                new MenuItem { Name = "Cadera", Price = 35 },
                new MenuItem { Name = "Ala", Price = 24 },
                new MenuItem { Name = "Pierna", Price = 24 },
                new MenuItem { Name = "Salsa", Price = 20 },
                new MenuItem { Name = "Frescos", Price = 25 },
                new MenuItem { Name = "Tajada con pollo", Price = 85 },
                new MenuItem { Name = "Paquete Tortilla", Price = 15 },
                new MenuItem { Name = "Granita", Price = 40 },
                new MenuItem { Name = "Jugos", Price = 25 },
                new MenuItem { Name = "Papas", Price = 23 }
            };
        }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
