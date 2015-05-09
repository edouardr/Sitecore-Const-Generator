using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace SitecoreConstGenerator.Core.Interfaces.Api
{
    #region Delegates
    public delegate TResult ServiceRequest<out TResult>(RestRequest req);

    public delegate Task<TResult> ServiceRequestAsync<TResult>(RestRequest req);

    public delegate TResult ServiceCall<out TResult>();

    public delegate Task<TResult> ServiceCallAsync<TResult>();
    #endregion

    /// <summary>
    ///     Defines specific set of methods and their arguments to implement a Rest Api client
    /// </summary>
    public interface IRestApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RestClient GetClient(RestRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urls"></param>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        RestRequest BuildRequest<T>(Dictionary<T, string> urls, T type, Method method, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urls"></param>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        RestRequest BuildRequest<T>(Dictionary<T, string> urls, T type, Method method, params object[] args);

        /// <summary>
        ///     Executes a request with Rest Sharp client
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="request">Request to execute</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        IRestResponse<T> Execute<T>(RestRequest request) where T : new();

        /// <summary>
        ///     Executes asynchronously a request with Rest Sharp Client
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="request">Request to execute</param>
        /// <returns></returns>
        Task<IRestResponse<T>> ExecuteAsync<T>(RestRequest request) where T : new();

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
        TResult Call<TResult, TEnumType>(ServiceRequest<TResult> serviceCall, Dictionary<TEnumType, string> uris,
            TEnumType enumType, Method httpMethod, params object[] args);

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
        Task<TResult> CallAsync<TResult, TEnumType>(ServiceRequestAsync<TResult> serviceCall,
            Dictionary<TEnumType, string> uris, TEnumType enumType, Method httpMethod, params object[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TEnumType"></typeparam>
        /// <param name="serviceCall"></param>
        /// <param name="uris"></param>
        /// <param name="enumType"></param>
        /// <param name="httpMethod"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        TResult Call<TResult, TEnumType>(ServiceRequest<TResult> serviceCall, Dictionary<TEnumType, string> uris,
            TEnumType enumType, Method httpMethod, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TEnumType"></typeparam>
        /// <param name="serviceCall"></param>
        /// <param name="uris"></param>
        /// <param name="enumType"></param>
        /// <param name="httpMethod"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<TResult> CallAsync<TResult, TEnumType>(ServiceRequestAsync<TResult> serviceCall,
            Dictionary<TEnumType, string> uris, TEnumType enumType, Method httpMethod, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="call"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        TResult Invoke<TResult>(ServiceCall<TResult> call, string methodName);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="call"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        Task<TResult> InvokeAsync<TResult>(ServiceCallAsync<TResult> call, string methodName);
    }
}
