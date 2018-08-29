using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChristalProduct.Models
{
    public class Toys
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public bool Recharable { get; set; }
        public string Battery {get; set;}
        public bool WaterProof { get; set; }
        public bool Glass { get; set; }
        public bool Silicon { get; set; }
        public bool Realistic { get; set; }
        public string ImagePath { get; set; }

    }
}
