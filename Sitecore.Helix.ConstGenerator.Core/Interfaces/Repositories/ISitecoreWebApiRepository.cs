namespace Sitecore.Helix.ConstGenerator.Core.Interfaces
{
    using System.Threading.Tasks;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;

    public interface ISitecoreWebApiRepository
    {
        ISitecoreWebApiRequestResult<Result, Item> RequestItems(SitecoreActionType actionType, string query);

        Task<ISitecoreWebApiRequestResult<Result, Item>> RequestItemsAsync(SitecoreActionType actionType, string query);
    }
}