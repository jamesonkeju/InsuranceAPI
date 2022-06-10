using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Data.Models.Domains;

namespace Insurance.Data.Models.Indentity
{
    public class Permission : BaseObject
    {
        public string PermissionName { get; set; }

        public string PermissionCode { get; set; }


        public string Icon { get; set; }


        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }
        public int ParentId { get; set; }


    }
}
