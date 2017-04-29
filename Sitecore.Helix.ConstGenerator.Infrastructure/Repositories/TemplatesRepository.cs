namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces;

    public class TemplatesRepository : BaseRepository
    {
        public TemplatesRepository(ISitecoreWebApiRepository api, string rootPath)
            : base(api, rootPath, SitecoreActionType.GetTemplatesIds, Settings.TemplatesQuery)
        {
        }

        public override IEnumerable<ItemNode> CreateTree()
        {
            var items = GetItemsFromRoot().ToList();

            items.RemoveAll(i => i.DisplayName.Equals("__Standard Values"));

            var lookup = RemoveDuplicates(items);
            lookup = LinkWithParent(lookup);

            return GetRootNodes(lookup);
        }
    }
}