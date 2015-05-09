﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using RestSharp;
using RestSharp.Contrib;
using SitecoreConstGenerator.Core.Interfaces.Api;
using SitecoreConstGenerator.Resources;

namespace SitecoreConstGenerator.Infrastructure.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class RestApi : IRestApi
    {

        #region Consts
        protected readonly string BaseUrl;
        protected readonly string AccountSid;
        protected readonly string SecretKey;
        protected readonly bool UseAuthenticateRequest;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RestApi));
        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="accountSid"></param>
        /// <param name="secretKey"></param>
        /// <param name="useAuthenticateRequest"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        protected RestApi(String baseUrl = null, String accountSid = null, String secretKey = null,
            Boolean useAuthenticateRequest = false)
        {
            this.AccountSid = accountSid;
            this.SecretKey = secretKey;
            this.UseAuthenticateRequest = useAuthenticateRequest;
            this.BaseUrl = baseUrl;

            if (string.IsNullOrEmpty(this.BaseUrl))
                throw new ArgumentNullException("baseUrl", @"The base Url is mandatory to create a REST API client.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual RestClient GetClient(RestRequest request)
        {
            var client = new RestClient { BaseUrl = new Uri(this.BaseUrl) };
            // Api authentification
            if (this.UseAuthenticateRequest)
            {
                client.Authenticator = new HttpBasicAuthenticator(this.AccountSid, this.SecretKey);
            }

            // headers used on every request
            request.AddHeader(@"accept", @"application/json");

            return client;
        }

        /// <summary>
        ///     Builds a RestRequest.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urls"></param>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public virtual RestRequest BuildRequest<T>(
            Dictionary<T, string> urls,
            T type,
            Method method,
            object value)
        {
            var request = new RestRequest();
            // Get API Url from list of urls
            string constUrl;
            urls.TryGetValue(type, out constUrl);

            if (value is DateTime)
            {
                // If argument is type of Datetime format it to the correct format (readable by the web api)
                value = ((DateTime) value).ToString("s");
            }

            if (constUrl != null)
            {
                request.Resource = String.Format(constUrl, value);
            }
            request.Method = method;

            return request;
        }

        public virtual RestRequest BuildRequest<T>(
            Dictionary<T, string> urls,
            T type,
            Method method,
            params object[] args)
        {
            var request = new RestRequest();
            // Get API Url from list of urls
            string constUrl;
            urls.TryGetValue(type, out constUrl);

            for (var i = 0; i < args.Length; ++i)
            {
                if (args[i] is DateTime)
                {
                    // If argument is type of Datetime format it to the correct format (readable by the web api)
                    args[i] = ((DateTime) args[i]).ToString("s");
                }
            }

            if (constUrl != null)
            {
                request.Resource = String.Format(constUrl, args);
            }
            request.Method = method;

            return request;
        }

        /// <summary>
        ///     Executes a request with Rest Sharp client
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="request">Request to execute</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public virtual IRestResponse<T> Execute<T>(RestRequest request) where T : new()
        {
            var client = this.GetClient(request);

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw new NotSupportedException(
                    @"Error retrieving response. Check inner details for more info.", response.ErrorException);
            }
            return response;
        }

        /// <summary>
        ///     Executes asynchronously a request with Rest Sharp Client
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="request">Request to execute</param>
        /// <returns></returns>
        public virtual Task<IRestResponse<T>> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            return this.ExecuteAsync<T>(request, CancellationToken.None);
        }

        protected virtual Task<IRestResponse<T>> ExecuteAsync<T>(RestRequest request, CancellationToken token) where T : new()
        {
            var client = this.GetClient(request);

            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();

            token.Register(() => taskCompletionSource.TrySetCanceled());

            if (!token.IsCancellationRequested
                && client != null)
            {
                client.ExecuteAsync<T>(
                    request,
                    (response, handle) =>
                    {
                        if (response == null || response.ErrorException != null)
                        {
                            if (response != null)
                            {
                                taskCompletionSource.TrySetException(response.ErrorException);
                                throw new NotSupportedException(
                                    response.ErrorException.Message,
                                    response.ErrorException.InnerException);
                            }
                        }

                        if(response != null) taskCompletionSource.TrySetResult(response);
                    });
            }
            else
                taskCompletionSource.TrySetCanceled();

            return taskCompletionSource.Task;
        }


        #region Service Calls

        /// <summary>
        /// 
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
        public virtual TResult Call<TResult, TEnumType>(
            ServiceRequest<TResult> serviceCall,
            Dictionary<TEnumType, string> uris,
            TEnumType enumType,
            Method httpMethod,
            params object[] args)
        {
            return this.Invoke(
                () =>
                    {
                        RestRequest request = this.BuildRequest(uris, enumType, httpMethod, args);

                        return serviceCall(request);
                    },
                serviceCall.Method.Name);
        }

        public virtual async Task<TResult> CallAsync<TResult, TEnumType>(ServiceRequestAsync<TResult> serviceCall,
            Dictionary<TEnumType, string> uris,
            TEnumType enumType,
            Method httpMethod,
            object value)
        {
            return await this.InvokeAsync(async () =>
            {
                RestRequest request = this.BuildRequest(uris, enumType, httpMethod, value);

                return await serviceCall(request);
            }, serviceCall.Method.Name);
        }

        /// <summary>
        /// 
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
            Dictionary<TEnumType, string> uris,
            TEnumType enumType,
            Method httpMethod,
            params object[] args)
        {
            return await this.InvokeAsync(async () =>
            {
                RestRequest request = this.BuildRequest(uris, enumType, httpMethod, args);

                return await serviceCall(request);
            }, serviceCall.Method.Name);
        }

        public virtual TResult Call<TResult, TEnumType>(ServiceRequest<TResult> serviceCall,
            Dictionary<TEnumType, string> uris,
            TEnumType enumType,
            Method httpMethod,
            object value)
        {
            return this.Invoke(() =>
            {
                RestRequest request = this.BuildRequest(uris, enumType, httpMethod, value);

                return serviceCall(request);
            }, serviceCall.Method.Name);
        }

        /// <summary>
        ///     This method is responsible for the try catch logic of all calls to the web api.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="call"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public virtual TResult Invoke<TResult>(ServiceCall<TResult> call, string methodName)
        {
            try
            {
                Stopwatch watch = Stopwatch.StartNew();

                TResult result = call();

                watch.Stop();
                Logger.InfoFormat("ExternalAPIsManager.{0} Call Success - ElaspedTime: {1} ms", methodName, watch.ElapsedMilliseconds);

                return result;
            }
            catch (NotSupportedException nse)
            {
                // Error thrown by method GetErrorMessage method if response content is null
                // or uncastable to the specified type
                Logger.FatalFormat("ExternalAPIsManager.{0} Call Failed - Exception: {1}", methodName, nse);
                throw new ApplicationException(
                    string.Format(
                        Errors.WS_Unavailable,
                        HttpUtility.HtmlEncode(methodName)),
                    nse);
            }
            catch (ApplicationException ae)
            {
                // this is thrown by the method (getting error message from WebAPI)
                // so we just have to throw it like it comes
                // see GetErrorMessage method
                Logger.FatalFormat("ExternalAPIsManager.{0} Call Failed - Exception: {1}", methodName, ae);
                throw;
            }
            catch (Exception e)
            {
                // Error low level ?
                Logger.FatalFormat("ExternalAPIsManager.{0} Call Failed - Exception: {1}", methodName, e);
                throw new ApplicationException(
                    string.Format(
                        Errors.WS_Unavailable,
                        HttpUtility.HtmlEncode(methodName)),
                    e);
            }
        }

        public virtual async Task<TResult> InvokeAsync<TResult>(ServiceCallAsync<TResult> call, string methodName)
        {
            try
            {
                Stopwatch watch = Stopwatch.StartNew();

                TResult result = await call();

                watch.Stop();
                Logger.InfoFormat("ExternalAPIsManager.{0} Call Success - ElaspedTime: {1} ms", methodName, watch.ElapsedMilliseconds);

                return result;
            }
            catch (NotSupportedException nse)
            {
                // Error thrown by method GetErrorMessage method if response content is null
                // or uncastable to the specified type
                Logger.FatalFormat("ExternalAPIsManager.{0} Call Failed - Exception: {1}", methodName, nse);
                throw new ApplicationException(
                    string.Format(
                        Errors.WS_Unavailable,
                        HttpUtility.HtmlEncode(methodName)),
                    nse);
            }
            catch (ApplicationException ae)
            {
                // this is thrown by the method (getting error message from WebAPI)
                // so we just have to throw it like it comes
                // see GetErrorMessage method
                Logger.FatalFormat("ExternalAPIsManager.{0} Call Failed - Exception: {1}", methodName, ae);
                throw;
            }
            catch (Exception e)
            {
                // Error low level ?
                Logger.FatalFormat("ExternalAPIsManager.{0} Call Failed - Exception: {1}", methodName, e);
                throw new ApplicationException(
                    string.Format(
                        Errors.WS_Unavailable,
                        HttpUtility.HtmlEncode(methodName)),
                    e);
            }
        }

        #endregion
        #endregion
    }
}
