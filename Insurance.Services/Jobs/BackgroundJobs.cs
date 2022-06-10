using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Insurance.Data;
using Insurance.Services.Emailing.Interface;
using Insurance.Utilities.Common;
using Insurance.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Services.Jobs
{
    public class BackgroundJobs
    {
        private readonly IConfiguration _configuration;
       
        private readonly InsuranceAppContext _context;
        public IEmailing _emailManager;
        public BackgroundJobs(IEmailing emailManager, IConfiguration configuration,InsuranceAppContext context)
        {
            _emailManager = emailManager;
            _configuration = configuration;
           
            _context = context;
           
        }

        public void ProcessEmail()
        {
            if (_configuration["EnableHangFire_Email"].ToLower() == "yes")
            {
                _emailManager.ProcessPendingEmails();
            }
        }

       
    }
}
