using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevAPI.Objects
{
    public class Champion
    {
        public Champion(string name, string id)
        {
            this.name = name;
            this.id = id;
            this.image = $"/champions/{name}_0.jpg";
        }
        public string name { get; set; }
        public string id { get; set; }
        public string image { get; set; }
    }
}
