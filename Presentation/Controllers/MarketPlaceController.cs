using Insurance.Data;
using Insurance.Services.AxaMansard.DTO;
using Insurance.Utilities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public MarketPlaceController(InsuranceAppContext context)
        {
            _context = context;
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
                return View(products);

            }
            var result = JsonConvert.DeserializeObject<Presentation.Models.productobject.Root>(response);


            if (result.isSuccessful != true)
            {

                return View(products);

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
                    return View(null);
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<Presentation.Models.KYCProperties.Root>(response);

                if (result.isSuccessful != true)
                {
                    return View(null);
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
                return View(loadList);
            }
            catch (Exception)
            {

                throw;
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

        // GET: MarketPlaceController/Create
        public ActionResult ManagePolicies()
        {
            var getPolices = _context.CustomerPolicies.ToList();

            return View(getPolices);
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
            var list = new Dictionary<string, string>();

            int addOn = 0;
            foreach (string key in collection.Keys)
            {
                addOn = addOn + 1;
                if (addOn != totalData)
                {
                    list.Add(key, collection[key]);
                }

            }

             string JsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            var response = await MiddleWare.PortalPostBasicAsync((dynamic)JsonResult, System.Threading.CancellationToken.None, ApplicationURL.Createpartnerscustomers);

            if (response == null)
            {
                return View(null);
            }
            var result = JsonConvert.DeserializeObject<Presentation.Models.generatepolicy.Root>(response);

            if (result.isSuccessful != "true")
            {
                return View(null);
            }
            ViewBag.Message = result.Result.message;
            ViewBag.trackingNumber = result.Result.trackingNumber;
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