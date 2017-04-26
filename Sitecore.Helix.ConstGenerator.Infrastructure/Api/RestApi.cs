namespace Sitecore.Helix.ConstGenerator.Infrastructure.Api
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Web;
  using log4net;
  using RestSharp;
  using RestSharp.Authenticators;
  using Sitecore.Helix.ConstGenerator.Core.Interfaces.Api;
  using Sitecore.Helix.ConstGenerator.Resources;
  
  public class RestApi : IRestApi
  {
    private static readonly ILog Logger = LogManager.GetLogger(typeof(RestApi));
    protected readonly string AccountSid;
    protected readonly string BaseUrl;
    protected readonly string SecretKey;
    protected readonly bool UseAuthenticateRequest;

    protected RestApi(string baseUrl = null, string accountSid = null, string secretKey = null,
      bool useAuthenticateRequest = false)
    {
      AccountSid = accountSid;
      SecretKey = secretKey;
      UseAuthenticateRequest = useAuthenticateRequest;
      BaseUrl = baseUrl;

      if (string.IsNullOrEmpty(BaseUrl))
        throw new ArgumentNullException(nameof(baseUrl), @"The base Url is mandatory to create a REST API client.");
    }
    
    /// <summary>
    ///   Basic client creator.
    ///   Adds ACCEPT header with application/json value.
    ///   Initializes with basic HTTP authenticator if required.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public virtual RestClient GetClient(RestRequest request)
    {
      var client = new RestClient {BaseUrl = new Uri(BaseUrl)};

      if (UseAuthenticateRequest)
        client.Authenticator = new HttpBasicAuthenticator(AccountSid, SecretKey);

      request.AddHeader(@"accept", @"application/json");

      return client;
    }

    /// <summary>
    ///   Builds a RestRequest.
    ///   Injects all values to the URLs provided (string format).
    ///   Formats values to string before that.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="urls">List of endpoints</param>
    /// <param name="type">selected endpoint</param>
    /// <param name="method">HTTP verb</param>
    /// <param name="value">value to inject in URL</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentException"></exception>
    public virtual RestRequest BuildRequest<T>(Dictionary<T, string> urls, T type, Method method, object value)
    {
      var request = new RestRequest();
      string constUrl;

      urls.TryGetValue(type, out constUrl);

      if (value is DateTime)
      {
        value = ((DateTime) value).ToString(@"s");
      }

      if (constUrl != null)
      {
        request.Resource = string.Format(constUrl, value);
      }

      request.Method = method;

      return request;
    }

    /// <summary>
    ///   Builds a RestRequest.
    ///   Injects all values to the URLs provided (string format).
    ///   Formats values to string before that.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="urls">List of endpoints</param>
    /// <param name="type">selected endpoint</param>
    /// <param name="method">HTTP verb</param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentException"></exception>
    public virtual RestRequest BuildRequest<T>(Dictionary<T, string> urls, T type, Method method, params object[] args)
    {
      var request = new RestRequest();
      string constUrl;

      urls.TryGetValue(type, out constUrl);

      for (var i = 0; i < args.Length; ++i)
        if (args[i] is DateTime)
          args[i] = ((DateTime) args[i]).ToString("s");

      if (constUrl != null)
        request.Resource = string.Format(constUrl, args);
      request.Method = method;

      return request;
    }

    /// <summary>
    ///   Executes a request with Rest Sharp client
    /// </summary>
    /// <typeparam name="T">Return Type</typeparam>
    /// <param name="request">Request to execute</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public virtual IRestResponse<T> Execute<T>(RestRequest request) where T : new()
    {
      var client = GetClient(request);

      var response = client.Execute<T>(request);

      if (response.ErrorException != null)
        throw new NotSupportedException(
          @"Error retrieving response. Check inner details for more info.", response.ErrorException);

      return response;
    }

    /// <summary>
    ///   Executes asynchronously a request with Rest Sharp Client
    /// </summary>
    /// <typeparam name="T">Return Type</typeparam>
    /// <param name="request">Request to execute</param>
    /// <returns></returns>
    public virtual Task<IRestResponse<T>> ExecuteAsync<T>(RestRequest request) where T : new()
    {
      return ExecuteAsync<T>(request, CancellationToken.None);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TEnumType"></typeparam>
    /// <param name="serviceCall"></param>
    /// <param name="uris"></param>
    /// <param name="enumType"></param>
    /// <param name="httpMethod"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="System.ApplicationException"></exception>
    public virtual TResult Call<TResult, TEnumType>(ServiceRequest<TResult> serviceCall,
      Dictionary<TEnumType, string> uris, TEnumType enumType, Method httpMethod, params object[] args)
    {
      return Invoke(() =>
        {
          var request = BuildRequest(uris, enumType, httpMethod, args);

          return serviceCall(request);
        },
        serviceCall.Method.Name);
    }

    public virtual async Task<TResult> CallAsync<TResult, TEnumType>(ServiceRequestAsync<TResult> serviceCall,
      Dictionary<TEnumType, string> uris, TEnumType enumType, Method httpMethod, object value)
    {
      return await InvokeAsync(async () =>
      {
        var request = BuildRequest(uris, enumType, httpMethod, value);

        return await serviceCall(request);
      }, serviceCall.Method.Name);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TEnumType"></typeparam>
    /// <param name="serviceCall"></param>
    /// <param name="uris"></param>
    /// <param name="enumType"></param>
    /// <param name="httpMethod"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="System.ApplicationException"></exception>
    public virtual async Task<TResult> CallAsync<TResult, TEnumType>(ServiceRequestAsync<TResult> serviceCall,
      Dictionary<TEnumType, string> uris, TEnumType enumType, Method httpMethod, params object[] args)
    {
      return await InvokeAsync(async () =>
      {
        var request = BuildRequest(uris, enumType, httpMethod, args);

        return await serviceCall(request);
      }, serviceCall.Method.Name);
    }

    public virtual TResult Call<TResult, TEnumType>(ServiceRequest<TResult> serviceCall,
      Dictionary<TEnumType, string> uris, TEnumType enumType, Method httpMethod, object value)
    {
      return Invoke(() =>
      {
        var request = BuildRequest(uris, enumType, httpMethod, value);

        return serviceCall(request);
      }, serviceCall.Method.Name);
    }

    /// <summary>
    ///   This method is responsible for the try catch logic of all calls to the web api.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="call"></param>
    /// <param name="methodName"></param>
    /// <returns></returns>
    public virtual TResult Invoke<TResult>(ServiceCall<TResult> call, string methodName)
    {
      try
      {
        var watch = Stopwatch.StartNew();

        var result = call();

        watch.Stop();
        Logger.InfoFormat("RestApi.{0} Call Success - ElaspedTime: {1} ms", methodName,
          watch.ElapsedMilliseconds);

        return result;
      }
      catch (NotSupportedException nse)
      {
        Logger.FatalFormat("RestApi.{0} Call Failed - Exception: {1}", methodName, nse);
        throw new ApplicationException(
          string.Format(Errors.WS_Unavailable, HttpUtility.HtmlEncode(methodName)),
          nse);
      }
      catch (ApplicationException ae)
      {
        Logger.FatalFormat("RestApi.{0} Call Failed - Exception: {1}", methodName, ae);
        throw;
      }
      catch (Exception e)
      {
        Logger.FatalFormat("RestApi.{0} Call Failed - Exception: {1}", methodName, e);
        throw new ApplicationException(
          string.Format(Errors.WS_Unavailable, HttpUtility.HtmlEncode(methodName)), e);
      }
    }

    public virtual async Task<TResult> InvokeAsync<TResult>(ServiceCallAsync<TResult> call, string methodName)
    {
      try
      {
        var watch = Stopwatch.StartNew();

        var result = await call();

        watch.Stop();
        Logger.InfoFormat("RestApi.{0} Call Success - ElaspedTime: {1} ms", methodName,
          watch.ElapsedMilliseconds);

        return result;
      }
      catch (NotSupportedException nse)
      {
        Logger.FatalFormat("RestApi.{0} Call Failed - Exception: {1}", methodName, nse);
        throw new ApplicationException(
          string.Format(Errors.WS_Unavailable, HttpUtility.HtmlEncode(methodName)), nse);
      }
      catch (ApplicationException ae)
      {
        Logger.FatalFormat("RestApi.{0} Call Failed - Exception: {1}", methodName, ae);
        throw;
      }
      catch (Exception e)
      {
        Logger.FatalFormat("RestApi.{0} Call Failed - Exception: {1}", methodName, e);
        throw new ApplicationException(
          string.Format(Errors.WS_Unavailable, HttpUtility.HtmlEncode(methodName)), e);
      }
    }

    protected virtual Task<IRestResponse<T>> ExecuteAsync<T>(RestRequest request, CancellationToken token)
      where T : new()
    {
      var client = GetClient(request);

      var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();

      token.Register(() => taskCompletionSource.TrySetCanceled());

      if (!token.IsCancellationRequested
          && client != null)
      {
        client.ExecuteAsync<T>(request, (response, handle) =>
        {
          if (response == null) throw new NotSupportedException();

          if (response.ErrorException != null)
          {
            taskCompletionSource.TrySetException(response.ErrorException);
            throw new NotSupportedException(response.ErrorException.Message, response.ErrorException.InnerException);
          }

          taskCompletionSource.TrySetResult(response);
        });
      }
      else
      {
        taskCompletionSource.TrySetCanceled();
      }

      return taskCompletionSource.Task;
    }
  }
}