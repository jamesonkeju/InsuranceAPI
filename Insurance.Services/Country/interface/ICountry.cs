using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Services.Country.DTO;
using Insurance.Utilities.Common;

namespace Insurance.Services.Country
{
    public interface ICountry
    {
        Task<List<Insurance.Data.Models.Domains.Country>> GetAllCountries(Data.Payload.CountryFilter payload);
        Task<Insurance.Data.Models.Domains.Country> GetCountryById(long id, bool CheckDeleted);
        Task<MessageOut> AddUpdateCountry(CountryDTO payload);
       
        Task<MessageOut> ToggleCountryStatus(long id);
    }
}
