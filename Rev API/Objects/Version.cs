using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevAPI.Objects
{
    public class Version
    {
        public Version(string version)
        {
            this.version = version;
        }
        public string version { get; set; }
    }
}
