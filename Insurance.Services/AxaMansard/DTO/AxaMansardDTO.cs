using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Services.AxaMansard.DTO
{
    public class AxaMansardDTO
    {
        public class AuthenicateRequest
        {
            public string PartnerCode { get; set; }
            public string SecretKey { get; set; }
        }

        public class AuthenicateRequestResponse
        {
                public string accessToken { get; set; }
                public int expiresIn { get; set; }
                public string code { get; set; }
                public bool isSuccessful { get; set; }
                public string message { get; set; }
            }

        

        public class PolicyByPartnerResponse
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class ReturnedObject
            {
                public string trackingNumber { get; set; }
                public string firstName { get; set; }
                public string lastName { get; set; }
                public string bundleName { get; set; }
                public string partnerName { get; set; }
                public string phoneNumber { get; set; }
                public DateTime policyStartDate { get; set; }
                public DateTime policyEndDate { get; set; }
                public string emailAddress { get; set; }
                public string localGovernment { get; set; }
                public string address { get; set; }
            }

            public class Root
            {
                public string returnedCode { get; set; }
                public bool isSuccessful { get; set; }
                public string message { get; set; }
                public List<ReturnedObject> returnedObject { get; set; }
            }


        }
        public class PartnerCodeRequest
        {
            public string PartnerCode { get; set; }
        }
        public class KYCRequestBody
        {
            public string PartnerCode { get; set; }
            public string ProductCode { get; set; }
        }

        public class ProviderListRequest
        {
           
            public string ProductCode { get; set; }
            public string state { get; set; }
            public string localGovt { get; set; }
        }

        public class PatnerCustomerReponse
        {
            public class Root
            {
                public string isSuccessful { get; set; }
                public string message { get; set; }
                public string TrackingNumber { get; set; }
            }
        }
        public class PolicyDetailsResponse
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class Root
            {
                public string phoneNumber { get; set; }
                public string firstName { get; set; }
                public string lastName { get; set; }
                public string emailAddress { get; set; }
                public DateTime policyStartDate { get; set; }
                public DateTime policyEndDate { get; set; }
                public string localGovernment { get; set; }
                public string address { get; set; }
                public string Code { get; set; }
                public bool isSuccessful { get; set; }
                public string message { get; set; }
            }


        }
        public class TrackingNumberRequest
        {
            public string TrackingNumber { get; set; }

        }
        public class RenewRequest
        {
                public string TrackingNumber { get; set; }
            
        }
        public class RenewRequestResponse
        {
            public class Root
            {
                public string isSuccessful { get; set; }
                public string message { get; set; }
               
            }
        }

        public class PatnerCustomerRequest
        {
            public string PartnerCode { get; set; }
            public string ProductCode { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
        }
        public class ProviderListResponse
        {
            public class ReturnedObject
            {
                public string name { get; set; }
                public string address { get; set; }
            }

            public class Root
            {
                public string returnedCode { get; set; }
                public bool isSuccessful { get; set; }
                public string message { get; set; }
                public List<ReturnedObject> returnedObject { get; set; }
            }
        }

        public class KYCRequestResponse
        {
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            public class Root
            {
                public string returnedCode { get; set; }
                public bool isSuccessful { get; set; }
                public string message { get; set; }
                public List<string> returnedObject { get; set; }
            }
        }

        public class LGAResponse
        {
            public class Root
            {
                public string returnedCode { get; set; }
                public bool isSuccessful { get; set; }
                public string message { get; set; }
                public List<string> returnedObject { get; set; }
            }


        }
        public class StateCode
        {
            public string state { get; set; }
        }
        public class StateResponse
        {
            public class Root
            {
                public string returnedCode { get; set; }
                public bool isSuccessful { get; set; }
                public string message { get; set; }
                public List<string> returnedObject { get; set; }
            }
        }
        public class GetProductResponse
        {
            public class Benefit
            {
                public int malaria { get; set; }
                public int life { get; set; }
                public int property { get; set; }
                public int propertyCrop { get; set; }
                public int hospitalization { get; set; }
                public int permanentDisability { get; set; }
                public int airTravel { get; set; }
                public int roadTravel { get; set; }
            }

            public class ReturnedObject
            {
                public string productCode { get; set; }
                public double price { get; set; }
                public int rate { get; set; }
                public string description { get; set; }
                public Benefit benefit { get; set; }
                public string duration { get; set; }
            }

            public class Root
            {
                public string returnedCode { get; set; }
                public bool isSuccessful { get; set; }
                public string message { get; set; }
                public List<ReturnedObject> returnedObject { get; set; }
            }


        }
    }
}
