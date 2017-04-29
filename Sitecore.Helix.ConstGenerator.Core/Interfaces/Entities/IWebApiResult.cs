namespace Sitecore.Helix.ConstGenerator.Core.Interfaces
{
    using System.Collections.Generic;

    public interface IWebApiResult<T>
        where T : ISitecoreWebApiItem
    {
        int TotalCount { get; set; }
        int ResultCount { get; set; }
        IEnumerable<T> Items { get; set; }
    }
}