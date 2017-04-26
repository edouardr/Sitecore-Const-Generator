namespace Sitecore.Helix.ConstGenerator.Infrastructure.Api
{
  using System;
  using RestSharp;

  public class SitecoreRestApi : RestApi
  {
    protected SitecoreRestApi(string baseUrl = null, string accountSid = null, string secretKey = null,
      bool useAuthenticateRequest = false)
      : base(baseUrl, accountSid, secretKey, useAuthenticateRequest)
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public override RestClient GetClient(RestRequest request)
    {
      var client = new RestClient {BaseUrl = new Uri(BaseUrl)};

      // headers used on every request
      request.AddHeader(@"accept", @"application/json");

      if (!UseAuthenticateRequest) return client;

      request.AddHeader(@"X-Scitemwebapi-Username", AccountSid);
      request.AddHeader(@"X-Scitemwebapi-Password", SecretKey);

      return client;
    }
  }
}