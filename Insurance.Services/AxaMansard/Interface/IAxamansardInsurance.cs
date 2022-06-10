using Insurance.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Insurance.Services.AxaMansard.DTO.AxaMansardDTO;

namespace Insurance.Services.AxaMansard.Interface
{
    public interface  IAxamansardInsurance
    {
        
        Task<ApiResult<AuthenicateRequestResponse>> Authentication();
        Task<ApiResult<GetProductResponse.Root>> GetProducts(string token);



        Task<ApiResult<KYCRequestResponse.Root>> GetProductKYC(string ProductCode, string token);
        Task<ApiResult<StateResponse.Root>> GetStates(string token);
        Task<ApiResult<LGAResponse.Root>> GetLocalGovernment(string token,string StateCode);
       Task<ApiResult<ProviderListResponse.Root>> GetProvider(ProviderListRequest payload, string token);
        Task<ApiResult<PatnerCustomerReponse.Root>> CreatePartnersCustomers(dynamic payload, string token, string ProductCode);
        Task<ApiResult<RenewRequestResponse.Root>> RenewPolicy(RenewRequest payload, string token, string ProductCode);
        Task<ApiResult<PolicyDetailsResponse.Root>> GetPolicyDetails(TrackingNumberRequest payload, string token);
        Task<ApiResult<PolicyByPartnerResponse.Root>> GetPolicyByPartner(PartnerCodeRequest payload, string token);
    }
}
