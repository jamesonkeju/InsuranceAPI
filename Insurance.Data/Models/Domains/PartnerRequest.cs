using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models.Domains
{
    public class PartnerRequest:BaseObject
    {
        public string RequestBody { get; set; }
        public string RequestRespone { get; set; }
        public string TrackingNumber { get; set; }
        public string ProductCode { get; set; }
        public string PartnerCode { get; set; }
        public string Staus { get; set; }

    }
}
