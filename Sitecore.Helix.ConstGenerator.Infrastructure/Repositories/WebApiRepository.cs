namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RestSharp;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories;
    using Sitecore.Helix.ConstGenerator.Infrastructure.Api;
    using Sitecore.Helix.ConstGenerator.Resources;

    public class WebApiRepository : SitecoreRestApi, IWebApiRepository
    {
        public WebApiRepository(string baseUrl = null, string accountSid = null, string secretKey = null,
            bool useAuthenticateRequest = false)
            : base(baseUrl, accountSid, secretKey, useAuthenticateRequest)
        {
        }
        
        public IWebApiRequestResult<Result, Item> RequestItems(SitecoreActionType actionType, string query)
        {
            try
            {
                return this.Call(
                    req =>
                    {
                        IRestResponse<RestResponse> restResponse = Execute<RestResponse>(req);
                        JObject jobject = JObject.Parse(restResponse.Content);

                        switch (restResponse.Data.StatusCode)
                        {
                            case HttpStatusCode.BadRequest:
                            case HttpStatusCode.Forbidden:
                            case HttpStatusCode.Unauthorized:
                                throw new ApplicationException(Errors.WS_Unavailable);
                        }

                        return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                    },
                    WebApiConstants.SitecoreUris,
                    actionType,
                    Method.GET,
                    new object[]
                    {
                        Settings.ItemWebApiVersion,
                        query
                    });
            }
            catch (ApplicationException)
            {
                throw;
            }
        }
        
        public async Task<IWebApiRequestResult<Result, Item>> RequestItemsAsync(SitecoreActionType actionType, string query)
        {
            return await this.CallAsync(
                    async req =>
                    {
                        IRestResponse<RestResponse> restResponse = await ExecuteAsync<RestResponse>(req);
                        JObject jobject = JObject.Parse(restResponse.Content);

                        switch (restResponse.Data.StatusCode)
                        {
                            case HttpStatusCode.BadRequest:
                            case HttpStatusCode.Forbidden:
                            case HttpStatusCode.Unauthorized:
                                throw new ApplicationException(Errors.WS_Unavailable);
                        }

                        return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                    },
                    WebApiConstants.SitecoreUris, 
                    actionType,
                    Method.GET,
                    new object[]
                    {
                        Settings.ItemWebApiVersion,
                        query
                    });
        }
    }
}