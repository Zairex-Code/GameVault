using System;
using System.Collections.Generic;
using System.Text;

namespace GameVault.Models
{
    internal class Game
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Genre { get; set; }
        public decimal Price { get; set; }
    }
}
