﻿namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories;

    public class TemplatesT4TemplateRepository : IT4TemplateRepository
    {
        public TemplatesT4TemplateRepository(IWebApiRepository repo)
        {
            Repo = repo;
        }

        public IWebApiRepository Repo { get; set; }

        public IEnumerable<ItemNode> CreateTree(string rootPath)
        {
            // get items
            var task = Repo.RequestTemplatesIdsAsync(rootPath);
            var result = task.Result as RequestResult;
            if (result != null)
            {
                var items = result.Result.Items;

                if (null == items)
                    throw new ArgumentNullException(@"items");

                var enumerable = items as Item[] ?? items.ToArray();
                if (!enumerable.Any())
                    return null;

                var list = enumerable
                    .OrderBy(i => i.Path)
                    .ToList();

                list.RemoveAll(i => i.DisplayName.Equals("__Standard Values")
                                    || TemplatesId.TemplateFieldId.Equals(i.TemplateId.ToString("B").ToUpper()));

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

                foreach (var item in lookup.Values)
                {
                    ItemNode proposedParent;
                    if (lookup.TryGetValue(item.Value.ParentId, out proposedParent))
                    {
                        item.ParentId = proposedParent.Value.ID;
                        if (null == proposedParent.Children)
                            proposedParent.Children = new List<ItemNode>();

                        proposedParent.Children.Add(item);
                    }
                }
                return lookup.Values.Where(x => x.ParentId == null || x.ParentId.Equals(Guid.Empty));
            }

            return null;
        }


        public string Output(Item item)
        {
            var sb = new StringBuilder();
            switch (item.TemplateId.ToString("B").ToUpper())
            {
                case TemplatesId.TemplateId:
                    sb.AppendFormat(
                        @"public const string {0} = @""{1}"";",
                        Regex.Replace(item.DisplayName, @"\s+", ""),
                        item.ID.ToString("B").ToUpper());
                    sb.AppendLine();
                    break;
                default:
                    sb.AppendFormat("public class {0} ", Regex.Replace(item.DisplayName, @"\s+", ""));
                    sb.AppendLine();
                    sb.AppendLine("{{");
                    sb.AppendLine("{0}");
                    sb.AppendLine("}}");
                    break;
            }

            return sb.ToString();
        }

        public string Output(ItemNode node)
        {
            return null == node.Children
                ? TemplatesId.TemplateId.Equals(node.Value.TemplateId.ToString("B").ToUpper())
                    ? Output(node.Value)
                    : string.Format(Output(node.Value), Environment.NewLine)
                : string.Format(Output(node.Value), string.Join(" ", node.Children.Select(Output)));
        }
    }
}