﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models.Domains
{
   public class Faq :BaseObject
   {
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}
