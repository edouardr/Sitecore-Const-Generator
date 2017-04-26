namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
  using System;
  using System.Net;
  using System.Threading.Tasks;
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
      return Call(req =>
      {
        var restResponse = Execute<RestResponse>(req);
        var jobject = JObject.Parse(restResponse.Content);

        if (restResponse.Data.StatusCode == HttpStatusCode.BadRequest ||
            restResponse.Data.StatusCode == HttpStatusCode.Forbidden ||
            restResponse.Data.StatusCode == HttpStatusCode.Unauthorized)
          throw new ApplicationException(Errors.WS_Unavailable);

        return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
      }, WebApiConstants.SitecoreUris, actionType, Method.GET, Settings.ItemWebApiVersion, query);
    }

    public async Task<IWebApiRequestResult<Result, Item>> RequestItemsAsync(SitecoreActionType actionType,
      string query)
    {
      return await CallAsync(async req =>
      {
        var restResponse = await ExecuteAsync<RestResponse>(req);
        var jobject = JObject.Parse(restResponse.Content);

        if (restResponse.Data.StatusCode == HttpStatusCode.BadRequest ||
            restResponse.Data.StatusCode == HttpStatusCode.Forbidden ||
            restResponse.Data.StatusCode == HttpStatusCode.Unauthorized)
          throw new ApplicationException(Errors.WS_Unavailable);

        return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
      }, WebApiConstants.SitecoreUris, actionType, Method.GET, Settings.ItemWebApiVersion, query);
    }
  }
}