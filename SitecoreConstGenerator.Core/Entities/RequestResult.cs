using SitecoreConstGenerator.Core.Interfaces.Entities;

namespace SitecoreConstGenerator.Core.Entities
{
    public class RequestResult : IWebApiRequestResult<Result, Item>
    {
        public int StatusCode { get; set; }

        public Result Result { get; set; }
    }
}
