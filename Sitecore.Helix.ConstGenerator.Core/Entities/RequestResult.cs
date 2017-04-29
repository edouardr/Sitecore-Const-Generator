namespace Sitecore.Helix.ConstGenerator.Core.Entities
{
    using Sitecore.Helix.ConstGenerator.Core.Interfaces;

    public class RequestResult : ISitecoreWebApiRequestResult<Result, Item>
    {
        public int StatusCode { get; set; }

        public Result Result { get; set; }
    }
}