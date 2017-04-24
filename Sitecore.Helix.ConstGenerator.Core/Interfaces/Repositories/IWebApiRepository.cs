namespace Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories
{
    using System.Threading.Tasks;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Entities;

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