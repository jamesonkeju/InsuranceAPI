using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Services.Role.DTO;

namespace Insurance.Services.SystemSetting.Interface
{
    public interface ISystemSetting
    {
        Task<List<SystemSettingViewModel>> GetSystemSettings();

        Task<SystemSettingViewModel> GetSystemSettingById(long id);
        Task<Utilities.Common.MessageOut> CreateUpdateSystemSetting(SystemSettingViewModel payload);

        Task<Utilities.Common.MessageOut> DeleteSystemSetting(DeleteSystemSettingViewModel payload);

        Task<List<SystemSettingViewModel>> GetSystemSettingByLookUpCodeAsync(string LookUpCode);
        List<SystemSettingViewModel> GetSystemSettingByLookUpCode(string LookUpCode);
    }
}
