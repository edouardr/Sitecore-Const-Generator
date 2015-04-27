using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SitecoreConstGenerator.Core.Constants;
using SitecoreConstGenerator.Core.Entities;
using SitecoreConstGenerator.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitecoreConstGenerator.Infrastructure.Repositories
{
    public class WebApiRepository : IWebApiRepository
    {
        public Core.Entities.RequestResult RequestFieldsIds(string sitecoreUrl, string rootPath)
        {
            String query = HttpUtility.UrlEncode(String.Format(Settings.FieldsQuery, rootPath));
            String url = String.Format(Settings.FieldsRequest, query);

            var client = new RestClient(sitecoreUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var requestUrl = String.Format("/-/item/{0}/{1}", Settings.ItemWebApiVersion, url);
            var request = new RestRequest(requestUrl, Method.GET);

            // execute the request
            RestResponse restResponse = client.Execute(request) as RestResponse;
            JObject jobject = JObject.Parse(restResponse.Content);
            
            return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
        }

        public Core.Entities.RequestResult RequestTemplatesIds(string sitecoreUrl, string rootPath)
        {
            String query = HttpUtility.UrlEncode(String.Format(Settings.TemplatesQuery, rootPath, TemplatesId.TemplateFolderId, TemplatesId.TemplateId));
            String url = String.Format(Settings.TemplatesRequest, query);

            var client = new RestClient(sitecoreUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var requestUrl = string.Format("/-/item/{0}/{1}", Settings.ItemWebApiVersion, url);
            var request = new RestRequest(requestUrl, Method.GET);

            // execute the request
            RestResponse restResponse = client.Execute(request) as RestResponse;
            JObject jobject = JObject.Parse(restResponse.Content);

            return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
        }

        public Core.Entities.RequestResult RequestRenderingsIds(string sitecoreUrl, string rootPath)
        {
            String query = HttpUtility.UrlEncode(String.Format(Settings.RenderingsQuery, rootPath));
            String url = String.Format(Settings.RenderingsRequest, query);

            var client = new RestClient(sitecoreUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var requestUrl = string.Format("/-/item/{0}/{1}", Settings.ItemWebApiVersion, url);
            var request = new RestRequest(requestUrl, Method.GET);

            // execute the request
            RestResponse restResponse = client.Execute(request) as RestResponse;
            JObject jobject = JObject.Parse(restResponse.Content);

            return JsonConvert.DeserializeObject<RequestResult>(jobject.ToString());
        }
    }
}
