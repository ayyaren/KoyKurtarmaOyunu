using System.Collections.Generic;

namespace KoyKurtarmaOyunu.Models
{
    public class Koy
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public List<Item> Envanter { get; set; } = new List<Item>();
    }
}
