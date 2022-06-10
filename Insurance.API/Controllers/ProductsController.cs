using Insurance.API.Shared;
using Insurance.Services.AxaMansard.Interface;
using Insurance.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Insurance.Services.AxaMansard.DTO.AxaMansardDTO;

namespace Insurance.API.Controllers
{
    [CustomRoleFilter]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private IAxamansardInsurance _insurance;

        public ProductsController(IAxamansardInsurance insurance)
        {
            _insurance = insurance;
        }


        /// <summary>
        /// Get all list of products
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<AuthenicateRequestResponse>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProducts()
        {


            try
            {
                var newSession = await _insurance.Authentication();

                if (newSession.IsSuccessful != true)
                {
                    var result = new ApiResult<GetProductResponse.Root>
                    {
                        HasError = true,
                        Result = null,
                        Message = CommonResponseMessage.InternalError,
                        StatusCode = CommonResponseMessage.FailStatusCode
                    };

                    return Ok(result);
                }

                var dataResult = await _insurance.GetProducts(newSession.Result.accessToken);

                return Ok(dataResult);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<GetProductResponse.Root>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }



        /// <summary>
        /// This api will help create partner customer
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<PatnerCustomerReponse.Root>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePartnersCustomers(dynamic payload)
        {

            try
            {
                if (!ModelState.IsValid)
                    return Ok(
                        new ApiResult<MessageOut>
                        {
                            HasError = false,
                            Message = CommonResponseMessage.MissingProperties,
                            StatusCode = CommonResponseMessage.FailStatusCode,
                        });
                var newSession = await _insurance.Authentication();

                if (newSession.IsSuccessful != true)
                {
                    var result = new ApiResult<PatnerCustomerReponse.Root>
                    {
                        HasError = true,
                        Result = null,
                        Message = CommonResponseMessage.InternalError,
                        StatusCode = CommonResponseMessage.FailStatusCode
                    };

                    return Ok(result);
                }

                var dataResult = await _insurance.CreatePartnersCustomers(payload,newSession.Result.accessToken, "");

                return Ok(dataResult);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<PatnerCustomerReponse.Root>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }



        /// <summary>
        /// This api will list all local government of the state
        /// </summary>
        /// <param name="StateName"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<LGAResponse.Root>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetLocalGovernment(string StateName)
        {

            try
            {


                if (!ModelState.IsValid)
                    return Ok(
                        new ApiResult<MessageOut>
                        {
                            HasError = false,
                            Message = CommonResponseMessage.MissingProperties,
                            StatusCode = CommonResponseMessage.FailStatusCode,
                        });

                var newSession = await _insurance.Authentication();


                if (newSession.IsSuccessful != true)
                {
                    var result = new ApiResult<LGAResponse.Root>
                    {
                        HasError = true,
                        Result = null,
                        Message = CommonResponseMessage.InternalError,
                        StatusCode = CommonResponseMessage.FailStatusCode
                    };

                    return Ok(result);
                }

                var dataResult = await _insurance.GetLocalGovernment(newSession.Result.accessToken, StateName);
                return Ok(dataResult);


            }
            catch (Exception ex)
            {
                var u = new ApiResult<LGAResponse.Root>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }
        /// <summary>
        /// This api will enable you get policy by partner 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<PolicyByPartnerResponse.Root>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPolicyByPartner(PartnerCodeRequest payload)
        {

            try
            {


                if (!ModelState.IsValid)
                    return Ok(
                        new ApiResult<MessageOut>
                        {
                            HasError = false,
                            Message = CommonResponseMessage.MissingProperties,
                            StatusCode = CommonResponseMessage.FailStatusCode,
                        });

                //get current session 
                var newSession = await _insurance.Authentication();
                if (newSession.IsSuccessful != true)
                {
                    var result = new ApiResult<PolicyByPartnerResponse.Root>
                    {
                        HasError = true,
                        Result = null,
                        Message = CommonResponseMessage.InternalError,
                        StatusCode = CommonResponseMessage.FailStatusCode
                    };

                    return Ok(result);
                }

                var dataResult = await _insurance.GetPolicyByPartner(payload, newSession.Result.accessToken);

                return Ok(dataResult);


            }
            catch (Exception ex)
            {
                var u = new ApiResult<PolicyByPartnerResponse.Root>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }

        /// <summary>
        /// This api will get all he policy details by using the tracking number 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<PolicyDetailsResponse.Root>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPolicyDetails(TrackingNumberRequest payload)
        {

            try
            {

                if (!ModelState.IsValid)
                    return Ok(
                        new ApiResult<MessageOut>
                        {
                            HasError = false,
                            Message = CommonResponseMessage.MissingProperties,
                            StatusCode = CommonResponseMessage.FailStatusCode,
                        });

                var newSession = await _insurance.Authentication();
                if (newSession.IsSuccessful != true)
                {
                    var result = new ApiResult<PolicyDetailsResponse.Root>
                    {
                        HasError = true,
                        Result = null,
                        Message = CommonResponseMessage.InternalError,
                        StatusCode = CommonResponseMessage.FailStatusCode
                    };

                    return Ok(result);
                }

                var dataResult = await _insurance.GetPolicyDetails(payload, newSession.Result.accessToken);

                return Ok(dataResult);

            }
            catch (Exception ex)
            {
                var u = new ApiResult<PolicyDetailsResponse.Root>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }

        /// <summary>
        /// This api will be used to get the product kyc
        /// </summary>
        /// <param name="ProductCode"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<KYCRequestResponse.Root>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductKYC(string ProductCode)
        {

            try
            {

                if (!ModelState.IsValid)
                    return Ok(
                        new ApiResult<MessageOut>
                        {
                            HasError = false,
                            Message = CommonResponseMessage.MissingProperties,
                            StatusCode = CommonResponseMessage.FailStatusCode,
                        });

                var newSession = await _insurance.Authentication();

                if (newSession.IsSuccessful != true)
                {
                    var result = new ApiResult<KYCRequestResponse.Root>
                    {
                        HasError = true,
                        Result = null,
                        Message = CommonResponseMessage.InternalError,
                        StatusCode = CommonResponseMessage.FailStatusCode
                    };

                    return Ok(result);
                }

                var dataResult = await _insurance.GetProductKYC(ProductCode, newSession.Result.accessToken);

                return Ok(dataResult);


            }
            catch (Exception ex)
            {
                var u = new ApiResult<KYCRequestResponse.Root>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }

        /// <summary>
        /// This api will get all provider list 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<ProviderListResponse.Root>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProvider(ProviderListRequest payload)
        {

            try
            {


                if (!ModelState.IsValid)
                    return Ok(
                        new ApiResult<MessageOut>
                        {
                            HasError = false,
                            Message = CommonResponseMessage.MissingProperties,
                            StatusCode = CommonResponseMessage.FailStatusCode,
                        });

                //get current session 
                var newSession = await _insurance.Authentication();

                if (newSession.IsSuccessful != true)
                {
                    var result = new ApiResult<ProviderListResponse.Root>
                    {
                        HasError = true,
                        Result = null,
                        Message = CommonResponseMessage.InternalError,
                        StatusCode = CommonResponseMessage.FailStatusCode
                    };

                    return Ok(result);
                }

                var dataResult = await _insurance.GetProvider(payload, newSession.Result.accessToken);

                return Ok(dataResult);


            }
            catch (Exception ex)
            {
                var u = new ApiResult<ProviderListResponse.Root>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }



        /// <summary>
        /// This api will fetch all states if non is avaiable on the system
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<StateResponse.Root>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStates()
        {

            try
            {

                if (!ModelState.IsValid)
                    return Ok(
                        new ApiResult<MessageOut>
                        {
                            HasError = false,
                            Message = CommonResponseMessage.MissingProperties,
                            StatusCode = CommonResponseMessage.FailStatusCode,
                        });

                //get current session 
                var newSession = await _insurance.Authentication();


                if (newSession.IsSuccessful != true)
                {
                    var result = new ApiResult<StateResponse.Root>
                    {
                        HasError = true,
                        Result = null,
                        Message = CommonResponseMessage.InternalError,
                        StatusCode = CommonResponseMessage.FailStatusCode
                    };

                    return Ok(result);
                }

                var dataResult = await _insurance.GetStates(newSession.Result.accessToken);

                return Ok(dataResult);


            }
            catch (Exception ex)
            {
                var u = new ApiResult<StateResponse.Root>
                {
                    HasError = true,
                    Result = null,
                    Message = ex.Message,
                    StatusCode = CommonResponseMessage.FailStatusCode
                };
                return BadRequest(u);
            }

        }

        /// <summary>
        /// This api is for renew of policies 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<RenewRequestResponse.Root>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RenewPolicy(RenewRequest payload)
        {

            try
            {
                if (!ModelState.IsValid)
                    return Ok(
                        new ApiResult<MessageOut>
                        {
                            HasError = false,
                            Message = CommonResponseMessage.MissingProperties,
                            StatusCode = CommonResponseMessage.FailStatusCode,
                        });

                //get current session 
                var newSession = await _insurance.Authentication();


                if (newSession.IsSuccessful != true)
                {
                    var result = new ApiResult<RenewRequestResponse.Root>
                    {
                        HasError = true,
                        Result = null,
                        Message = CommonResponseMessage.InternalError,
                        StatusCode = CommonResponseMessage.FailStatusCode
                    };

                    return Ok(result);
                }

                var dataResult = await _insurance.RenewPolicy(payload, newSession.Result.accessToken,"");

                return Ok(dataResult);
            }
            catch (Exception ex)
            {
                var u = new ApiResult<RenewRequestResponse.Root>
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
