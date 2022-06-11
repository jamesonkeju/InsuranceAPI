using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Data;
using Insurance.Data.Models.Domains;
using Insurance.Data.Payload;
using Insurance.Services.AuditLog.Concrete;
using Insurance.Services.AuditLog.DTO;
using Insurance.Services.CommonRoute;
using Insurance.Services.Country;
using Insurance.Services.Country.DTO;
using Insurance.Utilities.Common;
using Insurance.Services.AxaMansard.DTO;
using Insurance.Services.AxaMansard.Interface;
using Insurance.Utilities.JsonResult.WebResult.ErrorResult;
using static Insurance.Services.AxaMansard.DTO.AxaMansardDTO;

namespace Insurance.Services.AxaMansard.Concrete
{
   public  class AxamansardService : IAxamansardInsurance
    {
        private static string ApplicationState = ConfigHelper.AppSetting("ApplicationState", "AXAMansard");
        private static string TestBaseurl = ConfigHelper.AppSetting("BaseUrlTest", "AXAMansard");
        private static string BaseUrlProduction = ConfigHelper.AppSetting("BaseUrlProduction", "AXAMansard");
        private static string ProductionSecret = ConfigHelper.AppSetting("ProductionSecret", "AXAMansard");
        private static string TestSecret = ConfigHelper.AppSetting("TestSecret", "AXAMansard");
        private static string PartnerCodeTest = ConfigHelper.AppSetting("PartnerCodeTest", "AXAMansard");
        private static string PartnerCodeProduction = ConfigHelper.AppSetting("PartnerCodeProduction", "AXAMansard");

        private static string SercetCode = "";
        private static string PartnerCode = "";
        private static string baseurl = "";

        private InsuranceAppContext _context;
        private IActivityLog _activityLogService;
        private ILogger<AxamansardService> _logger;
        private ICommonRoute _commonServices;

        public AxamansardService(InsuranceAppContext context, ILogger<AxamansardService> logger, IActivityLog activityLogService, ICommonRoute commonServices)
        {
            _context = context;
            _activityLogService = activityLogService;
            _logger = logger;
            _commonServices = commonServices;
        }

       

        private async Task SaveErrorLog(Exception ex)
        {
           
                var error = new ErrorLog
                {
                    CreatedBy = "system",
                    CreatedById = "",
                    ErrorDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    InnerException = ex.InnerException.ToString(),
                    Source = ex.Message,
                };

                await  _context.ErrorLogs.AddAsync(error);
                await  _context.SaveChangesAsync();
                
           

        }

      
      
      
      

        #region Implementation of Axa Mansard APIs

        public async Task<ApiResult<AxaMansardDTO.PolicyDetailsResponse.Root>> GetPolicyDetails(AxaMansardDTO.TrackingNumberRequest payload, string Token)
        {
            var msg = new ApiResult<AxaMansardDTO.PolicyDetailsResponse.Root>();
            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }

                var response = await MiddleWare.PostBasicAsync(payload, System.Threading.CancellationToken.None, Token, WebApiAddress.Getpolicydetails, true);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.PolicyDetailsResponse.Root();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.PolicyDetailsResponse.Root>(response);

                if (result.isSuccessful != true)
                {
                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.PolicyDetailsResponse.Root();
                    return msg;
                }
                else
                {


                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.PolicyDetailsResponse.Root();
                return msg;
            }
        }
        public async Task<ApiResult<AxaMansardDTO.PolicyByPartnerResponse.Root>> GetPolicyByPartner(AxaMansardDTO.PartnerCodeRequest payload, string Token)
        {
            var msg = new ApiResult<AxaMansardDTO.PolicyByPartnerResponse.Root>();
            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }

                var response = await MiddleWare.PostBasicAsync(payload, System.Threading.CancellationToken.None, Token, WebApiAddress.GetPolicybyPartner, true);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.PolicyByPartnerResponse.Root();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.PolicyByPartnerResponse.Root>(response);

                if (result.isSuccessful != true)
                {
                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.PolicyByPartnerResponse.Root();
                    return msg;
                }
                else
                {


                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.PolicyByPartnerResponse.Root();
                return msg;
            }
        }

        public async Task<ApiResult<AxaMansardDTO.RenewRequestResponse.Root>> RenewPolicy(AxaMansardDTO.RenewRequest payload, string Token, string ProductCode)
        {
            


            var msg = new ApiResult<AxaMansardDTO.RenewRequestResponse.Root>();
            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }

                var response = await MiddleWare.PostBasicAsync(payload, System.Threading.CancellationToken.None, Token, WebApiAddress.Renewpolicy, true);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.RenewRequestResponse.Root();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.RenewRequestResponse.Root>(response);

                if (result.isSuccessful != "true")
                {
                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.RenewRequestResponse.Root();
                    return msg;
                }
                else
                {
                    // add new policy
                    var PolicyName = new TrackingNumberRequest
                    {
                        TrackingNumber = payload.TrackingNumber
                    };
                    var data =await GetPolicyDetails(PolicyName, Token);

                    var Policy = new CustomerPolicy
                    {
                        TrackingNumber = payload.TrackingNumber,
                        PolicyType = "Renew Policy",
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        ProductCode = data.Result.firstName,
                        emailAddress= data.Result.emailAddress,
                        FirstName =data.Result.firstName,
                        LastName= data.Result.lastName,
                        PolicyStartDate = data.Result.policyStartDate,
                        PolicyStartEndDate = data.Result.policyStartDate,
                        phoneNumber = data.Result.phoneNumber,
                        localGovernment=data.Result.localGovernment

                    };

                    await _context.CustomerPolicies.AddAsync(Policy);
                    await _context.SaveChangesAsync();

                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.RenewRequestResponse.Root();
                return msg;
            }
        }

        public async Task<ApiResult<AxaMansardDTO.PatnerCustomerReponse.Root>> CreatePartnersCustomers(dynamic payload, string Token,string ProductCode)
        {
            var msg = new ApiResult<AxaMansardDTO.PatnerCustomerReponse.Root>();

            var request = new PartnerRequest
            {
                CreatedBy = "System",
                IsActive = true,
                CreatedDate = DateTime.Now,
                RequestBody = JsonConvert.SerializeObject(payload),
                Staus = "Pending",
                ProductCode= ProductCode
            };

            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }

                var response = await MiddleWare.PostBasicAsync(payload, System.Threading.CancellationToken.None, Token, WebApiAddress.Createpartnerscustomers, true);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.PatnerCustomerReponse.Root();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.PatnerCustomerReponse.Root>(response);


              

                if (result.isSuccessful != "true")
                {
                    request.Staus = "Failed";

                    request.LastModified = DateTime.Now;
                    request.IsActive = true;
                    request.RequestRespone = JsonConvert.SerializeObject(result);

                    await _context.PartnerRequests.AddAsync(request);
                    await _context.SaveChangesAsync();

                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.PatnerCustomerReponse.Root();
                    return msg;
                }
                else
                {
                    // add new policy

                    var PolicyName = new TrackingNumberRequest
                    {
                        TrackingNumber = result.TrackingNumber
                    };

                    var data = await GetPolicyDetails(PolicyName, Token);

                    var Policy = new CustomerPolicy
                    {
                        TrackingNumber = result.TrackingNumber,
                        PolicyType = "New Policy",
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        ProductCode = data.Result.firstName,
                        emailAddress = data.Result.emailAddress,
                        FirstName = data.Result.firstName,
                        LastName = data.Result.lastName,
                        PolicyStartDate = data.Result.policyStartDate,
                        PolicyStartEndDate = data.Result.policyStartDate

                    };

                    await _context.CustomerPolicies.AddAsync(Policy);

                    // save the request 
                    request.Staus = "Successful";
                    request.TrackingNumber = result.TrackingNumber;
                    request.LastModified = DateTime.Now;
                    request.RequestRespone = JsonConvert.SerializeObject(result);
                  
                    await _context.PartnerRequests.AddAsync(request);
                    await _context.SaveChangesAsync();

                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                request.Staus = "Failed";

                request.LastModified = DateTime.Now;
                request.IsActive = true;
                request.RequestRespone = JsonConvert.SerializeObject(ex);

                 _context.PartnerRequests.Add(request);
                 _context.SaveChanges();

                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.PatnerCustomerReponse.Root();
                return msg;
            }
        }
        public async Task<ApiResult<AxaMansardDTO.ProviderListResponse.Root>> GetProvider(AxaMansardDTO.ProviderListRequest payload, string Token)
        {
            var msg = new ApiResult<AxaMansardDTO.ProviderListResponse.Root>();
            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }



               


                var response = await MiddleWare.PostBasicAsync(payload, System.Threading.CancellationToken.None, Token, WebApiAddress.GetHospitalList, true);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.ProviderListResponse.Root();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.ProviderListResponse.Root>(response);

                if (result.isSuccessful != true)
                {
                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.ProviderListResponse.Root();
                    return msg;
                }
                else
                {
                    // let save the product list for future use
                    var getProducts = result.returnedObject;

                    var dataList = new List<Insurance.Data.Models.Domains.ProviderList>();



                    foreach (var item in getProducts.ToList())
                    {
                        var data = new Insurance.Data.Models.Domains.ProviderList
                        {
                            address = item.address,
                            name = item.name,
                            CreatedDate = DateTime.Now,
                            IsActive = true,
                            IsDeleted = false,
                            productCode= payload.ProductCode,
                            Lga = payload.localGovt,
                            state = payload.state
                        };
                        dataList.Add(data);



                    }
                    string deleteItem = string.Format("delete from ProviderLists where state ='{0}' and lga = '{1}' and  productCode='{2}'", 

                    payload.state, payload.localGovt, payload.ProductCode);
                    _context.ProviderLists.FromSqlRaw(deleteItem);

                    // save the list of items 

                    await _context.ProviderLists.AddRangeAsync(dataList);

                    await _context.SaveChangesAsync();

                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.ProviderListResponse.Root();
                return msg;
            }
        }

        public async Task<ApiResult<AxaMansardDTO.LGAResponse.Root>> GetLocalGovernment(string token, string StateCode)
        {
            var msg = new ApiResult<AxaMansardDTO.LGAResponse.Root>();
            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }



                var statecodeData = new Insurance.Services.AxaMansard.DTO.AxaMansardDTO.StateCode
                {
                    state = StateCode
                };


                var response = await MiddleWare.PostBasicAsync(statecodeData, System.Threading.CancellationToken.None, token, WebApiAddress.GetLocalGovt, true);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.LGAResponse.Root();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.LGAResponse.Root>(response);

                if (result.isSuccessful != true)
                {
                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.LGAResponse.Root();
                    return msg;
                }
                else
                {

                    // let save the product list for future use
                    var getProducts = result.returnedObject;

                    var lga = new List<Insurance.Data.Models.Domains.LGA>();



                    foreach (var item in getProducts.ToList())
                    {
                        var data = new Insurance.Data.Models.Domains.LGA
                        {
                            LGAName = item,
                            StateCode = StateCode,
                            CreatedDate = DateTime.Now,
                            IsActive = true,
                            IsDeleted = false,
                        };
                        lga.Add(data);



                    }

                    _context.LGAs.FromSqlRaw("DELETE FROM LGAs");

                    // save the list of items 

                    await _context.LGAs.AddRangeAsync(lga);
                    await _context.SaveChangesAsync();



                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.LGAResponse.Root();
                return msg;
            }
        }
        public async Task<ApiResult<AxaMansardDTO.GetProductResponse.Root>> GetProducts(string token)
        {
            var msg = new ApiResult<AxaMansardDTO.GetProductResponse.Root>();
            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }


                var payload = new AxaMansardDTO.PartnerCodeRequest
                {
                    PartnerCode = PartnerCode
                };




                var response = await MiddleWare.PostBasicAsync(payload, System.Threading.CancellationToken.None, token, WebApiAddress.GetProductList, true);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.GetProductResponse.Root();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.GetProductResponse.Root>(response);

                if (result.isSuccessful != true)
                {
                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.GetProductResponse.Root();
                    return msg;
                }
                else
                {

                    // let save the product list for future use
                    var getProducts = result.returnedObject;

                    var products = new List<Insurance.Data.Models.Domains.ProductList>();

                    var productBenefit = new List<Insurance.Data.Models.Domains.ProductBenefit>();

                    foreach (var item in getProducts)
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

                        var dataBenefit = new Insurance.Data.Models.Domains.ProductBenefit
                        {
                            airTravel = item.benefit.airTravel,
                            hospitalization = item.benefit.hospitalization,
                            life = item.benefit.life,
                            malaria = item.benefit.malaria,
                            permanentDisability = item.benefit.permanentDisability,
                            property = item.benefit.property,
                            propertyCrop = item.benefit.propertyCrop,
                            roadTravel = item.benefit.roadTravel,
                            IsActive = true,
                            IsDeleted = false,
                            productCode = item.productCode,
                            CreatedBy = "System"
                        };
                        productBenefit.Add(dataBenefit);

                    }


                    /// delete all exist product to avoid duplicate 

                    _context.productLists.FromSqlRaw("UPDATE productLists SET isdeleted =1, isactive =0, LastModified= getdate() ");

                    _context.productBenefits.FromSqlRaw("UPDATE productBenefits SET isdeleted =1, isactive =0, LastModified= getdate()");


                    // save the list of items 
                    await _context.productLists.AddRangeAsync(products);
                    await _context.productBenefits.AddRangeAsync(productBenefit);

                    await _context.SaveChangesAsync();

                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.GetProductResponse.Root();
                return msg;
            }
        }

        public async Task<ApiResult<AxaMansardDTO.KYCRequestResponse.Root>> GetProductKYC(string ProductCode, string Token)

        {
            var msg = new ApiResult<AxaMansardDTO.KYCRequestResponse.Root>();

            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }


                var payload = new AxaMansardDTO.KYCRequestBody
                {
                    PartnerCode = PartnerCode,
                    ProductCode = ProductCode

                };

                var response = await MiddleWare.PostBasicAsync(payload, System.Threading.CancellationToken.None, Token, WebApiAddress.GetKYC, true);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.KYCRequestResponse.Root();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.KYCRequestResponse.Root>(response);

                if (result.isSuccessful != true)
                {
                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.KYCRequestResponse.Root();
                    return msg;
                }
                else
                {

                   

                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                await SaveErrorLog(ex);
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.KYCRequestResponse.Root();
                return msg;
            }
        }
        public async Task<ApiResult<AxaMansardDTO.StateResponse.Root>> GetStates(string token)
        {
            var msg = new ApiResult<AxaMansardDTO.StateResponse.Root>();
            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }






                var response = await MiddleWare.GetAsync( WebApiAddress.GetStates,token, true);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.StateResponse.Root();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.StateResponse.Root>(response);

                if (result.isSuccessful != true)
                {
                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.StateResponse.Root();
                    return msg;
                }
                else
                {

                    // let save the product list for future use
                    var getProducts = result.returnedObject;

                    var stateList = new List<Insurance.Data.Models.Domains.State>();



                    foreach (var item in getProducts.ToList())
                    {
                        var data = new Insurance.Data.Models.Domains.State
                        {
                            Name = item,
                            StateCode = item,
                            CreatedDate = DateTime.Now,
                            IsActive = true,
                            IsDeleted = false,
                        };
                        stateList.Add(data);



                    }


                    /// delete all exist state to avoid duplicate 

                    _context.States.FromSqlRaw("DELETE FROM STATES");

                    // save the list of items 

                    await _context.States.AddRangeAsync(stateList);
                    await _context.SaveChangesAsync();

                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.StateResponse.Root();
                return msg;
            }
        }
        public async Task<ApiResult<AxaMansardDTO.AuthenicateRequestResponse>> Authentication()
        {
            var msg = new ApiResult<AxaMansardDTO.AuthenicateRequestResponse>();


            try
            {
                if (ApplicationState.ToLower() == "production")
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = ProductionSecret;
                    baseurl = BaseUrlProduction;
                }
                else
                {
                    SercetCode = ProductionSecret;
                    PartnerCode = PartnerCodeTest;
                    baseurl = TestBaseurl;
                }


                var payload = new AxaMansardDTO.AuthenicateRequest
                {
                    PartnerCode = PartnerCode,
                    SecretKey = SercetCode

                };

                var response = await MiddleWare.PostBasicAsync(payload, System.Threading.CancellationToken.None, null, WebApiAddress.Authenication, false);

                if (response == null)
                {
                    msg.Message = CommonResponseMessage.InvalidRequest;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.AuthenicateRequestResponse();
                    return msg;
                }

                // convert the response
                var result = JsonConvert.DeserializeObject<AxaMansardDTO.AuthenicateRequestResponse>(response);

                if (result.isSuccessful != true)
                {
                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = false;
                    msg.Result = new AxaMansardDTO.AuthenicateRequestResponse();
                    return msg;
                }
                else
                {

                    var saveToken = new ApplicationSession
                    {
                        accessToken = result.accessToken,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false
                    };

                    // save the session 
                    _context.applicationSessions.FromSqlRaw("Update applicationSessions set isactive =0, isdeleted=1");

                    await _context.applicationSessions.AddAsync(saveToken);
                    await _context.SaveChangesAsync();

                    msg.Message = result.message;
                    msg.HasError = true;
                    msg.IsSuccessful = true;
                    msg.Result = result;
                    return msg;
                }

            }
            catch (Exception ex)
            {
                await SaveErrorLog(ex);
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.IsSuccessful = false;
                msg.Result = new AxaMansardDTO.AuthenicateRequestResponse();
                return msg;
            }
        }

     
        #endregion
    }
}
