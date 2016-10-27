using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloppyJoes
{
    public class MenuItem
    {
        public MenuItem(string meat, string condiment, string bread)
        {
            Meat = meat;
            Bread = bread;
            Bread = bread;
        }

        public string Meat { get; set; }
        public string Condiment { get; set; }
        public string Bread { get; set; }

        public override string ToString()
        {
            return $"{Meat} with {Condiment} on {Bread}.";
        }
    }
}
