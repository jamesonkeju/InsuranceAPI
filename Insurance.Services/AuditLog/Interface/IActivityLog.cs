using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Data.Models.Domains;
using Insurance.Services.AuditLog.DTO;
using Insurance.Utilities.Common;

namespace Insurance.Services.AuditLog.Concrete
{
    public interface IActivityLog
    {
        #region Interface for Activity Log Service CRUD
        Task<MessageOut> CreateActivityLog(ActivityLog payload);
        Task CreateActivityLogAsync(string description, string controllerName, string actionName, string userid, object record, object OldRecord);
        void CreateActivityLog(string description, string controllerName, string actionName, string userid, object record, object OldRecord);
       
        #endregion
    }
}
