namespace Sitecore.Helix.ConstGenerator.Core.Interfaces.Entities
{
    using System.Collections.Generic;

    public interface IWebApiResult<T>
        where T : IWebApiItem
    {
        int TotalCount { get; set; }
        int ResultCount { get; set; }
        IEnumerable<T> Items { get; set; }
    }
}