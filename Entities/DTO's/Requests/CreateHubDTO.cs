using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s.Requests
{
    public class CreateHubDTO
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Tags { get; set; }
        public string Image { get; set; }
    }
}
