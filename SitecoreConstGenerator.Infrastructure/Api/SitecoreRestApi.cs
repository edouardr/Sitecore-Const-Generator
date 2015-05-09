using System;
using RestSharp;

namespace SitecoreConstGenerator.Infrastructure.Api
{
    public class SitecoreRestApi : RestApi
    {
        #region Constructors
        protected SitecoreRestApi(String baseUrl = null, String accountSid = null, String secretKey = null,
            Boolean useAuthenticateRequest = false)
            : base(baseUrl, accountSid, secretKey, useAuthenticateRequest)
        {

        }

        #endregion

        #region Overrides
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override RestClient GetClient(RestRequest request)
        {
            var client = new RestClient { BaseUrl = new Uri(this.BaseUrl) };

            // headers used on every request
            request.AddHeader(@"accept", @"application/json");

            if (this.UseAuthenticateRequest)
            {
                request.AddHeader(@"X-Scitemwebapi-Username", this.AccountSid);
                request.AddHeader(@"X-Scitemwebapi-Password", this.SecretKey);
            }

            return client;
        }
        #endregion
    }
}
