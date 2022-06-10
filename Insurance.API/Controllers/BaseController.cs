using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Insurance.API.Shared;
using Insurance.Data;
using Insurance.Data.Models;
using Insurance.Data.Models.Indentity;
using Insurance.Data.Payload;
using Insurance.Services.DataAccess;
using Insurance.Services.Role.DTO;
using Insurance.Utilities.Common;
using Insurance.Data.Models.Domains;

namespace Insurance.API.Controllers
{
    public class BaseController : ControllerBase
    {
      
        public BaseController()
        {
            
        }

        [NonAction]
        public string getMainAction()
        {
            return ControllerContext.ActionDescriptor.ActionName;
        }

        [NonAction]
        public string getMainController()
        {
            return ControllerContext.ActionDescriptor.ControllerName;
        }
        [NonAction]
        public ApiResult<ApplicationSession> GetCurrentSession()
        {
            var jwt_token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var resp = new ApiResult<ApplicationSession> { IsSuccessful = false, Message = "Unable to token!" };

            if (string.IsNullOrWhiteSpace(jwt_token)) return resp;

            using (var _context = new InsuranceAppContext())
            {

                try
                {
                    var data = (from u in _context.applicationSessions
                                where u.IsActive==true
                                select u).FirstOrDefault();

                    if (data == null)
                    {
                        resp.IsSuccessful = false;
                        resp.Result = null;

                    }
                    else
                    {
                        resp.IsSuccessful = true;
                        resp.Result = data;
                        resp.Message = "Record Found";
                    }
                    return resp;
                }

                catch (Exception ex)
                {
                    resp.Message = ex.Message;
                    return resp;
                }


            }

        }

    }
}
