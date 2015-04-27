using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreConstGenerator.Core.Interfaces.Entities
{
    public interface IWebApiResult<T>
        where T : IWebApiItem
    {
        int TotalCount { get; set; }
        int ResultCount { get; set; }
        IEnumerable<T> Items { get; set; }
    }
}
