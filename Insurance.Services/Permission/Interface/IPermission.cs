using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Services.Permission.DTO;
using Insurance.Utilities.Common;

namespace Insurance.Services.Permission
{
   public interface IPermission
    {

        Task<MessageOut> AddUpdatePermission(PermissionViewModel obj);
        Task<List<PermissionViewModel>> GetAllParent(bool? status);
        Task<MessageOut> DeletePermission(DeletePermissionViewModel obj);

        Task<List<PermissionViewModel>> GetAllPermissions(bool? status);



        Task<PermissionViewModel> GetPermissionByID(long id);
   
        Task<bool> UpdateRolePermission(UpdateRolePermissionViewModel payload);
    }
}
