namespace Sitecore.Helix.ConstGenerator.Core.Interfaces.Entities
{
    public interface IWebApiRequestResult<TResult, TItem>
        where TItem : IWebApiItem
        where TResult : IWebApiResult<TItem>
    {
        int StatusCode { get; set; }
        TResult Result { get; set; }
    }
}