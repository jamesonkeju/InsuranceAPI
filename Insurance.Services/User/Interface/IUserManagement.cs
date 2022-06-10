using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Data.Models;
using Insurance.Data.Models.ViewModel;
using Insurance.Data.Payload;

namespace Insurance.Services.User.Interface
{
    public interface IUserManagement
    {

        Task<CustomResponse> AddUpdateCustomer(UserViewModel user, bool isPrimary);
        Task<IEnumerable<ApplicationUser>> GetUsers();

        Task<ApplicationUser> GetUserMobile(string userId);
        Task<IEnumerable<ApplicationUser>> GetDealerSubAgents(string UserId);

        Task<IEnumerable<ApplicationUser>> GetAllDealersByType(string AccountType);

        Task<UserViewModel> GetUser(string id);
        List<DetailsOptionDTO> GetRoles();
        Task<CustomResponse> AddUpdatePortalUser(UserViewModel obj);
   

        bool ValidatePhoneNumber(string parameter, string id);
        bool ValidateUserName(string parameter, string id);
        bool ValidateEMail(string parameter, string id);


        Task<CustomResponse> ValidateOTPToken(OTPPayload payload);

        Task<CustomResponse> RegenerateOTP(OTPPayload payload);

        List<ApplicationUser> GetUsersByRole(string role);

        Task<UpdateUserViewModel> UpdateUserMobile(UpdateUserViewModel model);

        Task<UserViewModel> CreateUserMobile(UserViewModel model);


    

    }
}
