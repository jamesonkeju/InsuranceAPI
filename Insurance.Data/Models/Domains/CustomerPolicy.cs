using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models.Domains
{
     public  class CustomerPolicy:BaseObject
    {
        public string TrackingNumber { get; set; }
        public string ProductCode { get; set; }
        public string PolicyType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
        public string localGovernment { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyStartEndDate { get; set; }


    }
}
