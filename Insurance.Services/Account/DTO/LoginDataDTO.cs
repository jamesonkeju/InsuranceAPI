using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Services.Role.DTO;

namespace Insurance.Services.Account.DTO
{
   public class LoginDataDTO
    {
        public List<SidebarMenuViewModel> sideVarMenu { get; set; }
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
        public string Menus { get; set; }
    }

    
}
