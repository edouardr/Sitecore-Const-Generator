namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories;

    public class TemplatesT4TemplateRepository : BaseRepository
    {
        private readonly string _rootPath;

        public TemplatesT4TemplateRepository(IWebApiRepository api, string rootPath)
            : base(
                api, rootPath, SitecoreActionType.GetTemplatesIds, Settings.TemplatesQuery)
        {
            _rootPath = rootPath;
        }

        protected TemplatesT4TemplateRepository(IWebApiRepository api, string rootPath, SitecoreActionType actionType,
            string query)
            : base(api, rootPath, actionType, query)
        {
            _rootPath = rootPath;
        }

        public override IEnumerable<ItemNode> CreateTree()
        {
            var items = GetItemsFromRoot(_rootPath).ToList();

            items.RemoveAll(i => i.DisplayName.Equals("__Standard Values"));

            var lookup = RemoveDuplicates(items);
            lookup = LinkWithParent(lookup);

            return GetRootNodes(lookup);
        }
    }
}