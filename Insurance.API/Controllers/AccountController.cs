using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Data.Payload;
using Insurance.Utilities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Insurance.Data;
using Insurance.Data.Models;
using Insurance.Services.AuditLog.Concrete;
using Insurance.Services.Permission.Interface;
using Insurance.Services.Role.Interface;
using System.Data;
using Insurance.Services.DataAccess;
using Insurance.Services.Role.DTO;
using Insurance.Data.Models.Indentity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using Insurance.Data.Models.ViewModel;
using Insurance.Services.Emailing.DTO;
using Insurance.Services.Emailing.Interface;
using System.Globalization;
using static Insurance.Services.AxaMansard.DTO.AxaMansardDTO;
using Insurance.Services.AxaMansard.Interface;

namespace Insurance.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        protected readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRole _roleSignManager;
        private readonly IRolePermission _rolePermissionManager;
        private readonly InsuranceAppContext _context;
        private readonly IActivityLog _activityRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailing _emailManager;
        private IAxamansardInsurance _insurance;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager, IConfiguration configuration,
            IRole roleSignManager, IRolePermission rolePermissionManager,
            InsuranceAppContext context, IActivityLog activityRepo, IHttpContextAccessor httpContextAccessor, IEmailing emailManager, IAxamansardInsurance insurance)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _roleSignManager = roleSignManager;
            _rolePermissionManager = rolePermissionManager;
            _context = context;
            _roleManager = roleManager;
            _activityRepo = activityRepo;
            _httpContextAccessor = httpContextAccessor;
            _emailManager = emailManager;
            _insurance = insurance;
        }

        /// <summary>
        /// Authenicate a user by provider code 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<AuthenicateRequestResponse>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Authentication()
        {

            try
            {
                var result = await _insurance.Authentication();
                return Ok(result);
            }
            catch (Exception ex)
            {
                var u = new ApiResult<AuthenicateRequestResponse>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }
        }

    }
}
