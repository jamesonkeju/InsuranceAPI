using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Data.Models.Domains
{
   public class CustomerKYC :BaseObject
    {
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string PreferredHospital { get; set; }
        public string ProductCode { get; set; }
    }
}
