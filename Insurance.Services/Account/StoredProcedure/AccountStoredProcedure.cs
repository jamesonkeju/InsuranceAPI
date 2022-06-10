using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Data;
using Insurance.Data.Models;
using Insurance.Data.Models.Domains;

namespace Insurance.Services.Account.StoredProcedure
{
    public static class AccountStoredProcedure
    {

        public static Customer sp_GetUserById(string id, InsuranceAppContext context)
        {  

            var _params = new SqlParameter("id", id);

            var result = context.Customers.FromSqlRaw("EXEC dbo.sp_GetUserById @id", _params).ToList();
            return result.FirstOrDefault();
        }
    }
}
