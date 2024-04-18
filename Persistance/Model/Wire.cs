using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Model
{
    public class Wire
    {
        public int WireId { get; set; }
        public Color Color { get; set; }
        public Diameter Diameter { get; set; }
        public Length Length { get; set; }
        public Material Material { get; set; }
    }
}
