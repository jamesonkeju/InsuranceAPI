using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Utilities.Common;

namespace Insurance.Services.Account.Interface
{
    public interface IAccount
    {
        Task<MessageOut> Login(Data.Payload.UserLoginPayload payload);
        Task<MessageOut> Register(Data.Payload.AdminUserSettingViewModel payload);
    }
}
