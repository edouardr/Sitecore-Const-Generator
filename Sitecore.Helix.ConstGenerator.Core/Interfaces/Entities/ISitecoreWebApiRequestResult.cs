namespace Sitecore.Helix.ConstGenerator.Core.Interfaces
{
    public interface ISitecoreWebApiRequestResult<TResult, TItem>
        where TItem : ISitecoreWebApiItem
        where TResult : IWebApiResult<TItem>
    {
        int StatusCode { get; set; }
        TResult Result { get; set; }
    }
}