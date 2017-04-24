namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories;

    public class BaseRepository : IT4TemplateRepository
    {
        private readonly IWebApiRepository _api;
        private readonly SitecoreActionType _actionType;
        private readonly string _rootPath;
        private readonly string _query;

        protected BaseRepository(IWebApiRepository repo, string rootPath, SitecoreActionType actionType, string query, params object[] args)
        {
            _api = repo;
            _actionType = actionType;
            _query = HttpUtility.UrlEncode(string.Format(query, new object[] { rootPath }.Union(args)));
            _rootPath = rootPath;
        }

        public virtual IEnumerable<ItemNode> CreateTree()
        {
            List<Item> items = GetItemsFromRoot(_rootPath).ToList();

            Dictionary<Guid, ItemNode> lookup = RemoveDuplicates(items);
            lookup = LinkWithParent(lookup);

            return GetRootNodes(lookup);
        }

        protected virtual IEnumerable<Item> GetItemsFromRoot(string rootPath)
        {
            var task = _api.RequestItemsAsync(_actionType, _query);
            var result = task.Result as RequestResult;
            if (result == null)
                throw new ArgumentNullException(nameof(rootPath));

            var items = result.Result.Items;
            if (null == items)
                throw new NullReferenceException($"No items where found at provided root: ${rootPath}");

            var enumerable = items as Item[] ?? items.ToArray();
            if (!enumerable.Any())
                throw new NullReferenceException($"No items where found at provided root: ${rootPath}");

            return enumerable
                .OrderBy(i => i.Path);
        }

        protected virtual Dictionary<Guid, ItemNode> RemoveDuplicates(List<Item> list)
        {
            var lookup = new Dictionary<Guid, ItemNode>();
            list.ForEach(x =>
            {
                try
                {
                    lookup.Add(x.ID, new ItemNode {Value = x});
                }
                catch (Exception)
                {
                    // already added this key
                }
            });
            return lookup;
        }

        protected virtual Dictionary<Guid, ItemNode> LinkWithParent(Dictionary<Guid, ItemNode> lookup)
        {
            foreach (var item in lookup.Values)
            {
                ItemNode proposedParent;
                if (!lookup.TryGetValue(item.Value.ParentId, out proposedParent)) continue;

                item.ParentId = proposedParent.Value.ID;
                if (null == proposedParent.Children)
                    proposedParent.Children = new List<ItemNode>();

                proposedParent.Children.Add(item);
            }

            return lookup;
        }

        protected virtual IEnumerable<ItemNode> GetRootNodes(Dictionary<Guid, ItemNode> lookup)
        {
            return lookup.Values.Where(x => x.ParentId.Equals(Guid.Empty));
        }

        public virtual string Output(Item item)
        {
            throw new NotImplementedException();
        }

        public virtual string Output(ItemNode node)
        {
            throw new NotImplementedException();
        }
    }
}