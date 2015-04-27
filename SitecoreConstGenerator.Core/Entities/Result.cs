using SitecoreConstGenerator.Core.Interfaces.Entities;
using System.Collections.Generic;

namespace SitecoreConstGenerator.Core.Entities
{
    public class Result : IWebApiResult<Item>
    {
        public int TotalCount { get; set; }

        public int ResultCount { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}
