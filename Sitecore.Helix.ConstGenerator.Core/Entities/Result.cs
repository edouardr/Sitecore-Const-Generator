namespace Sitecore.Helix.ConstGenerator.Core.Entities
{
    using System.Collections.Generic;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces;

    public class Result : IWebApiResult<Item>
    {
        public int TotalCount { get; set; }

        public int ResultCount { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}