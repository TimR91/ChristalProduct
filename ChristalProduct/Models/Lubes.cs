using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChristalProduct.Models
{
    public class Lubes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public bool SiliconBased { get; set; }
        public bool WaterBased { get; set; }
        public bool Hybrid { get; set; }
        public bool Desensitizing { get; set; }
        public string Flavor { get; set; }
        public string AddedEffects { get; set; }
        public string ImagePath { get; set; }
    }
}
