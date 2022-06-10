using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Data.Payload;
using Insurance.Services.Country.DTO;
using Insurance.Utilities.Common;

namespace Insurance.Services.FAQs.Interface
{
    public interface IFAQs
    {
        Task<List<Insurance.Data.Models.Domains.Faq>> GetAllQuestionAndAnswers(FAQsFilter payload);
        Task<Insurance.Data.Models.Domains.Faq> GetFAQsById(long id, bool CheckDeleted);
        Task<MessageOut> AddUpdateQuestionAndAnswer(Insurance.Services.FAQsDTO.DTO.NotificationLogDTO payload);


    }
}
