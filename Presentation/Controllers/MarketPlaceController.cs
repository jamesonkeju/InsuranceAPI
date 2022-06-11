using Insurance.Data;
using Insurance.Services.AxaMansard.DTO;
using Insurance.Utilities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using static Insurance.Services.AxaMansard.DTO.AxaMansardDTO;
using static Presentation.Models.policydetails;

namespace Presentation.Controllers
{
    public class MarketPlaceController : Controller
    {
        private InsuranceAppContext _context;
        private IToastNotification _toastNotification;
        public MarketPlaceController(InsuranceAppContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        // GET: MarketPlaceController
        public async Task<ActionResult> Index()
        {
            
            return View();
        }

        public async Task<ActionResult> ManageProduct()
        {
            var products = new List<Insurance.Data.Models.Domains.ProductList>();

            var msg = new ApiResult<AxaMansardDTO.GetProductResponse.Root>();

            var response = await MiddleWare.PortalPostBasicAsync(null, System.Threading.CancellationToken.None, ApplicationURL.GetProductList);

            if (response == null)
            {
                _toastNotification.AddErrorToastMessage("System unable to fetch the data at the moment.");
                return RedirectToAction("ManageProduct");

            }
            var result = JsonConvert.DeserializeObject<Presentation.Models.productobject.Root>(response);


            if (result.isSuccessful != true)
            {
                _toastNotification.AddErrorToastMessage("System unable to fetch the data at the moment.");
                return RedirectToAction("ManageProduct");

            }


            foreach (var item in result.result.returnedObject)
            {
                var data = new Insurance.Data.Models.Domains.ProductList
                {
                    description = item.description,
                    duration = item.duration,
                    price = item.price,
                    IsActive = true,
                    IsDeleted = false,
                    rate = item.rate,
                    productCode = item.productCode,
                    CreatedBy = "System"
                };
                products.Add(data);


            }
            _toastNotification.AddSuccessToastMessage("Request Found");
            return View(products);
        }

        [HttpGet]
        public async Task<ActionResult> PayPolicy(string ProductCode,string Description)
        {
            try
            {
                ViewBag.PartnerCode = "03105";
                ViewBag.ProductCode = ProductCode;
                ViewBag.Description = Description;
                var response = await MiddleWare.PortalGetBasicAsync(ApplicationURL.GetKYC + "?ProductCode=" + ProductCode);

                if (response == null)
                {
                    _toastNotification.AddAlertToastMessage("Selected Plan not found, please try again");
                    return RedirectToAction("ManageProduct");
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<Presentation.Models.KYCProperties.Root>(response);

                if (result.isSuccessful != true)
                {
                    _toastNotification.AddAlertToastMessage(result.message);
                    return RedirectToAction("ManageProduct");
                }

                var returnProperties = result.result.returnedObject;

                var loadList = new List<DynamicFormField>();

                foreach (var item in returnProperties)
                {
                    loadList.Add(new DynamicFormField()
                    {
                        Name = item
                    });
                }
                _toastNotification.AddInfoToastMessage("Complete your KYC to purcahse your policy");
                
                return View(loadList);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("Inner error " + ex.Message);
                return RedirectToAction("ManageProduct");
            }
        }

        public async Task<ActionResult> PolicyDetails(string TrackingNumber)
        {
            var datavalue = new Result();
            try
            {
                var body = new TrackingNumberRequest
                {
                    TrackingNumber = TrackingNumber
                };

                var response = await MiddleWare.PortalPostBasicAsync(body, System.Threading.CancellationToken.None, ApplicationURL.Getpolicydetails);

                if (response == null)
                {
                    return View(datavalue);
                }

                var result = JsonConvert.DeserializeObject<Presentation.Models.policydetails.Root>(response);

                if (result.isSuccessful == false)
                {
                    return View(datavalue);
                }

                datavalue = new Result
                {
                    firstName = result.result.firstName,
                    lastName = result.result.lastName,
                    phoneNumber = result.result.phoneNumber,
                    policyEndDate = result.result.policyEndDate,
                    policyStartDate = result.result.policyStartDate,
                };


                return View(datavalue);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet]
        public async Task<ActionResult> ManageState()
        {
            var loadState = await _context.States.Where(a => a.IsActive == true).ToListAsync();
            return View(loadState);
        }
        [HttpGet]
        public async Task<ActionResult> ManageLGA(string StateName)
        {
            var loadState = await _context.LGAs.Where(a => a.IsActive == true && a.StateCode == StateName).ToListAsync();
            return View(loadState);
        }

        // GET: MarketPlaceController/Create
        public ActionResult ManagePolicies()
        {
            var getPolices = _context.CustomerPolicies.ToList();

            return View(getPolices);
        }

        public ActionResult RenewPolicy()
        {
            return View(new Insurance.Services.AxaMansard.DTO.AxaMansardDTO.RenewRequest());
        }

        // POST: MarketPlaceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RenewPolicyResult(Insurance.Services.AxaMansard.DTO.AxaMansardDTO.RenewRequest renewRequest)
        {
            var policies = new List<Insurance.Data.Models.Domains.CustomerPolicy>();

            try
            {
                var response = await MiddleWare.PortalPostBasicAsync(renewRequest, System.Threading.CancellationToken.None, ApplicationURL.Renewpolicy);

                if (response == null)
                {
                    return View(policies);
                }

                var result = JsonConvert.DeserializeObject<AxaMansardDTO.RenewRequestResponse.Root>(response);

                if (result.isSuccessful != "true")
                {
                    return View(policies);
                }

                var getPolices = _context.CustomerPolicies.Where(a => a.TrackingNumber == renewRequest.TrackingNumber).ToList();
                return View(getPolices);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProcessPolicy(IFormCollection collection)
        {
            int totalData = collection.Count();

            if (totalData == 0)
            {
                _toastNotification.AddErrorToastMessage("Kindly supply all fields");
                return RedirectToAction("ManageProduct");

            }

            var list = new Dictionary<string, string>();

            int addOn = 0;
            int countPhone = 0;
            foreach (string key in collection.Keys)
            {
                addOn = addOn + 1;
                if (addOn != totalData)
                {
                    if(key == "PhoneNumber")
                    {
                        countPhone = collection[key].Count();
                    }
                    list.Add(key, collection[key]);
                }

            }

          

             string JsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            var response = await MiddleWare.PortalPostBasicAsync((dynamic)JsonResult, System.Threading.CancellationToken.None, ApplicationURL.Createpartnerscustomers);

            if (response == null)
            {
                _toastNotification.AddErrorToastMessage("Invalid operation");
                return RedirectToAction("ManageProduct");
            }
            var result = JsonConvert.DeserializeObject<Presentation.Models.generatepolicy.Root>(response);

            if (result.isSuccessful != true)
            {
                _toastNotification.AddErrorToastMessage(result.message);
                return RedirectToAction("ManageProduct");
            }
            ViewBag.Message = result.result.message;
            ViewBag.trackingNumber = result.result.trackingNumber;
            _toastNotification.AddSuccessToastMessage("Policy number is " + result.result.trackingNumber);
            return View("Completed");
        }
        private JObject FormCollectionToJson(IFormCollection obj)
        {
            dynamic json = new JObject();
            if (obj.Keys.Any())
            {
                foreach (string key in obj.Keys)
                {   //check if the value is an array                 
                    if (obj[key].Count > 1)
                    {
                        JArray array = new JArray();
                        for (int i = 0; i < obj[key].Count; i++)
                        {
                            array.Add(obj[key][i]);
                        }
                        json.Add(key, array);
                    }
                    else
                    {
                        var value = obj[key][0];
                        json.Add(key, value);
                    }
                }
            }
            return json;


        }
    }
}