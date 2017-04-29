namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces;
    using Sitecore.Helix.ConstGenerator.Resources;

    public abstract class BaseRepository : IT4TemplateRepository
    {
        private readonly SitecoreActionType _actionType;
        private readonly ISitecoreWebApiRepository _api;
        private readonly string _query;
        private readonly string _rootPath;

        protected BaseRepository(ISitecoreWebApiRepository repo, string rootPath, SitecoreActionType actionType,
            string query,
            params object[] args)
        {
            _api = repo;
            _actionType = actionType;
            var arguments = new List<object> {rootPath, args.ToList()};
            _query = HttpUtility.UrlEncode(string.Format(query, rootPath, arguments.ToArray()));
            _rootPath = rootPath;
        }

        public virtual IEnumerable<ItemNode> CreateTree()
        {
            var items = GetItemsFromRoot().ToList();

            var lookup = RemoveDuplicates(items);
            lookup = LinkWithParent(lookup);

            return GetRootNodes(lookup);
        }

        protected virtual IEnumerable<Item> GetItemsFromRoot()
        {
            var task = _api.RequestItemsAsync(_actionType, _query);
            var result = task.Result as RequestResult;
            if (result == null)
                throw new NullReferenceException(Errors.EXCEPTION_NOT_EXPECTED_RESULT);

            var items = result.Result.Items;
            if (null == items)
                throw new NullReferenceException(string.Format(Errors.EXCEPTION_NO_ITEMS_FOR_ROOT, _rootPath));

            var enumerable = items as Item[] ?? items.ToArray();
            if (!enumerable.Any())
                throw new NullReferenceException(string.Format(Errors.EXCEPTION_NO_ITEMS_FOR_ROOT, _rootPath));

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
    }
}