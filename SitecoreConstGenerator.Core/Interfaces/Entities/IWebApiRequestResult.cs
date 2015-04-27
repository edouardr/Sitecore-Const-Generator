using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreConstGenerator.Core.Interfaces.Entities
{
    public interface IWebApiRequestResult<TResult, TItem>
        where TItem : IWebApiItem
        where TResult : IWebApiResult<TItem>
    {
        int StatusCode { get; set; }
        TResult Result { get; set; }
    }
}
