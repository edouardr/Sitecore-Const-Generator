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
        #region Constructors

        public WebApiRepository(string baseUrl = null, string accountSid = null, string secretKey = null,
            bool useAuthenticateRequest = false)
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
                    SitecoreActionType.GetFieldsIds,
                    Method.GET,
                    new object[]
                    {
                        Settings.ItemWebApiVersion,
                        HttpUtility.UrlEncode(string.Format(Settings.FieldsQuery, rootPath))
                    });
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
                    IRestResponse<RestResponse> restResponse = Execute<RestResponse>(req);
                    JObject jobject = JObject.Parse(restResponse.Content);

                    return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                },
                WebApiConstants.SitecoreUris,
                SitecoreActionType.GetTemplatesIds,
                Method.GET,
                new object[]
                {
                    Settings.ItemWebApiVersion,
                    HttpUtility.UrlEncode(string.Format(Settings.TemplatesQuery, rootPath, TemplatesId.TemplateFolderId,
                        TemplatesId.TemplateId))
                });
        }

        public IWebApiRequestResult<Result, Item> RequestRenderingsIds(string rootPath)
        {
            return this.Call(
                req =>
                {
                    IRestResponse<RestResponse> restResponse = Execute<RestResponse>(req);
                    JObject jobject = JObject.Parse(restResponse.Content);

                    return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                },
                WebApiConstants.SitecoreUris,
                SitecoreActionType.GetTemplatesIds,
                Method.GET,
                new object[]
                {
                    Settings.ItemWebApiVersion,
                    HttpUtility.UrlEncode(string.Format(Settings.RenderingsQuery, rootPath))
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
                    SitecoreActionType.GetFieldsIds,
                    Method.GET,
                    new object[]
                    {
                        Settings.ItemWebApiVersion,
                        HttpUtility.UrlEncode(string.Format(Settings.FieldsQuery, rootPath))
                    });
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
                    IRestResponse<RestResponse> restResponse = await ExecuteAsync<RestResponse>(req);
                    JObject jobject = JObject.Parse(restResponse.Content);

                    return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                },
                WebApiConstants.SitecoreUris,
                SitecoreActionType.GetTemplatesIds,
                Method.GET,
                new object[]
                {
                    Settings.ItemWebApiVersion,
                    HttpUtility.UrlEncode(string.Format(Settings.TemplatesQuery, rootPath, TemplatesId.TemplateFolderId,
                        TemplatesId.TemplateId))
                });
        }

        public async Task<IWebApiRequestResult<Result, Item>> RequestRenderingsIdsAsync(string rootPath)
        {
            return await this.CallAsync(
                async req =>
                {
                    IRestResponse<RestResponse> restResponse = await ExecuteAsync<RestResponse>(req);
                    JObject jobject = JObject.Parse(restResponse.Content);

                    return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
                },
                WebApiConstants.SitecoreUris,
                SitecoreActionType.GetTemplatesIds,
                Method.GET,
                new object[]
                {
                    Settings.ItemWebApiVersion,
                    HttpUtility.UrlEncode(string.Format(Settings.RenderingsQuery, rootPath))
                });
        }

        #endregion

        #endregion
    }
}