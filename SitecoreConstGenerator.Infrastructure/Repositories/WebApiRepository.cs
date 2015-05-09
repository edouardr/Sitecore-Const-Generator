using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SitecoreConstGenerator.Core.Constants;
using SitecoreConstGenerator.Core.Entities;
using SitecoreConstGenerator.Core.Interfaces.Entities;
using SitecoreConstGenerator.Core.Interfaces.Repositories;
using SitecoreConstGenerator.Infrastructure.Api;
using SitecoreConstGenerator.Resources;

namespace SitecoreConstGenerator.Infrastructure.Repositories
{
    public class WebApiRepository : SitecoreRestApi, IWebApiRepository
    {
        #region Constructors
        public WebApiRepository(String baseUrl = null, String accountSid = null, String secretKey = null,
            Boolean useAuthenticateRequest = false)
            : base(baseUrl, accountSid, secretKey, useAuthenticateRequest)
        {

        }
        #endregion

        #region Implementation
        public IWebApiRequestResult<Result, Item> RequestFieldsIds(string rootPath)
        {
            try
            {
                return this.Call(
                    req =>
                    {
                        IRestResponse<RestResponse> restResponse = this.Execute<RestResponse>(req);
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
                    SitecoreActionType.GetFieldsIds,
                    Method.GET,
                    new object[]
                    {Settings.ItemWebApiVersion, HttpUtility.UrlEncode(String.Format(Settings.FieldsQuery, rootPath))});
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public IWebApiRequestResult<Result, Item> RequestTemplatesIds(string rootPath)
        {
            return this.Call(
                req =>
                {
                    IRestResponse<RestResponse> restResponse = this.Execute<RestResponse>(req);
                    JObject jobject = JObject.Parse(restResponse.Content);

                    return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                },
                WebApiConstants.SitecoreUris,
                SitecoreActionType.GetTemplatesIds,
                Method.GET,
                new object[]
                {
                    Settings.ItemWebApiVersion,
                    HttpUtility.UrlEncode(String.Format(Settings.TemplatesQuery, rootPath, TemplatesId.TemplateFolderId,
                        TemplatesId.TemplateId))
                });
        }

        public IWebApiRequestResult<Result, Item> RequestRenderingsIds(string rootPath)
        {
            return this.Call(
                req =>
                {
                    IRestResponse<RestResponse> restResponse = this.Execute<RestResponse>(req);
                    JObject jobject = JObject.Parse(restResponse.Content);

                    return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                },
                WebApiConstants.SitecoreUris,
                SitecoreActionType.GetTemplatesIds,
                Method.GET,
                new object[]
                {
                    Settings.ItemWebApiVersion,
                    HttpUtility.UrlEncode(String.Format(Settings.RenderingsQuery, rootPath))
                });
        }

        #region Async
        public async Task<IWebApiRequestResult<Result, Item>> RequestFieldsIdsAsync(string rootPath)
        {
            try
            {
                return await this.CallAsync(
                    async req =>
                    {
                        IRestResponse<RestResponse> restResponse = await this.ExecuteAsync<RestResponse>(req);
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
                    SitecoreActionType.GetFieldsIds,
                    Method.GET,
                    new object[]
                    {Settings.ItemWebApiVersion, HttpUtility.UrlEncode(String.Format(Settings.FieldsQuery, rootPath))});
            }
            catch (ApplicationException)
            {
                throw;
            }

        }

        public async Task<IWebApiRequestResult<Result, Item>> RequestTemplatesIdsAsync(string rootPath)
        {
            return await this.CallAsync(
                async req =>
                {
                    IRestResponse<RestResponse> restResponse = await this.ExecuteAsync<RestResponse>(req);
                    JObject jobject = JObject.Parse(restResponse.Content);

                    return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                },
                WebApiConstants.SitecoreUris,
                SitecoreActionType.GetTemplatesIds,
                Method.GET,
                new object[]
                {
                    Settings.ItemWebApiVersion,
                    HttpUtility.UrlEncode(String.Format(Settings.TemplatesQuery, rootPath, TemplatesId.TemplateFolderId,
                        TemplatesId.TemplateId))
                });
        }

        public async Task<IWebApiRequestResult<Result, Item>> RequestRenderingsIdsAsync(string rootPath)
        {
            return await this.CallAsync(
                async req =>
                {
                    IRestResponse<RestResponse> restResponse = await this.ExecuteAsync<RestResponse>(req);
                    JObject jobject = JObject.Parse(restResponse.Content);

                    return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                },
                WebApiConstants.SitecoreUris,
                SitecoreActionType.GetTemplatesIds,
                Method.GET,
                new object[]
                {
                    Settings.ItemWebApiVersion,
                    HttpUtility.UrlEncode(String.Format(Settings.RenderingsQuery, rootPath))
                });
        }
        #endregion
        #endregion
    }
}
