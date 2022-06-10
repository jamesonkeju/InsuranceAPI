using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models.Domains
{
    public class State :BaseObject
    {
        public string StateCode { get; set; }
        public string Name { get; set; }
    }

    public class LGA :BaseObject
    {
        public string StateCode { get; set; }
        public string LGAName { get; set; }

    }
}
