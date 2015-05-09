using SitecoreConstGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SitecoreConstGenerator.Core.Interfaces.Entities;

namespace SitecoreConstGenerator.Core.Interfaces.Repositories
{
    public interface IWebApiRepository
    {
        IWebApiRequestResult<Result, Item> RequestFieldsIds(string rootPath);
        Task<IWebApiRequestResult<Result, Item>> RequestFieldsIdsAsync(string rootPath);
        IWebApiRequestResult<Result, Item> RequestTemplatesIds(string rootPath);
        Task<IWebApiRequestResult<Result, Item>> RequestTemplatesIdsAsync(string rootPath);
        IWebApiRequestResult<Result, Item> RequestRenderingsIds(string rootPath);
        Task<IWebApiRequestResult<Result, Item>> RequestRenderingsIdsAsync(string rootPath);
    }
}
