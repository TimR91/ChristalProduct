using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChristalProduct.Models
{
    public class Pills
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PillCount { get; set; }
        public decimal PillCost { get; set; }
        public string PillIngredients { get; set; }
        public string Description { get; set; }
        public int Milligrams { get; set; }
        public string ImagePath { get; set; }
    }
}
