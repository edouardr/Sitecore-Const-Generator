namespace Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories
{
    using System.Threading.Tasks;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Entities;

    public interface IWebApiRepository
    {
        IWebApiRequestResult<Result, Item> RequestItems(SitecoreActionType actionType, string query);

        Task<IWebApiRequestResult<Result, Item>> RequestItemsAsync(SitecoreActionType actionType, string query);
    }
}