using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Data;

namespace Insurance.Services.DataAccess
{
    public class DatabaseHandlerFactory
    {

        private readonly InsuranceAppContext _context;
        public DatabaseHandlerFactory(InsuranceAppContext context)
        {
            _context = context;
            // connectionStringSettings = context.Database.GetDbConnection().ConnectionString();
        }

        public IDataAccess CreateDatabase()
        {
            IDataAccess database = null;

            switch (_context.Database.ProviderName.ToLower())
            {
                case "microsoft.entityframeworkcore.sqlserver":
                    database = new SqlDataAccess(_context.Database.GetDbConnection().ConnectionString);
                    break;
               

            }

            return database;
        }

        public string GetProviderName()
        {
            return _context.Database.ProviderName;
        }
    }
}
