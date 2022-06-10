using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Data.Models;
using Insurance.Data.Payload;

namespace Insurance.Services.SessionTokenGenerator.Interface
{
    public interface ISessionTokenGenerator
    {
        string GenerateToken(VwUserInfornation session);
        Task<string> GenerateToken(ApplicationUser usr);
    }
}
