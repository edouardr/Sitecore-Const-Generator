using SitecoreConstGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreConstGenerator.Core.Interfaces.Repositories
{
    public interface IWebApiRepository
    {
        RequestResult RequestFieldsIds(string sitecoreUrl, string rootPath);
        RequestResult RequestTemplatesIds(string sitecoreUrl, string rootPath);
        RequestResult RequestRenderingsIds(string sitecoreUrl, string rootPath);
    }
}
