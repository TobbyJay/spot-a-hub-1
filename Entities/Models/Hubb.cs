using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Hubb
    {
        public Guid HubbId { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Tags { get; set; }
        public string Image { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }


    }
}
