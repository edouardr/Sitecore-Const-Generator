using SitecoreConstGenerator.Core.Constants;
using SitecoreConstGenerator.Core.Entities;
using SitecoreConstGenerator.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SitecoreConstGenerator.Infrastructure.Repositories
{
    public class RenderingsT4TemplateRepository : IT4TemplateRepository
    {
        public IEnumerable<ItemNode> CreateTree(IEnumerable<Item> items)
        {
            if (null == items)
                throw new ArgumentNullException("items");

            if (0 >= items.Count())
                return null;

            List<Item> list = items
                .OrderBy(i => i.Path)
                .ToList();

            Dictionary<Guid, ItemNode> lookup = new Dictionary<Guid, ItemNode>();
            list.ForEach(x =>
            {
                try
                {
                    lookup.Add(x.ID, new ItemNode { Value = x });
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

        public String Output(Item item)
        {
            StringBuilder sb = new StringBuilder();
            switch (item.TemplateId.ToString("B").ToUpper())
            {
                case TemplatesId.TemplateControllerRenderingId:
                case TemplatesId.TemplateViewRenderingId:
                    sb.AppendFormat(
                        @"public const string {0} = @""{1}"";",
                        Regex.Replace(item.DisplayName, @"\s+", ""),
                        item.ID.ToString("B").ToUpper());
                    sb.AppendLine();
                    break;
                case TemplatesId.TemplateFolderId:
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

        public String Output(ItemNode node)
        {
            return (null == node.Children)
                ? (TemplatesId.TemplateViewRenderingId.Equals(node.Value.TemplateId.ToString("B").ToUpper())
                || TemplatesId.TemplateControllerRenderingId.Equals(node.Value.TemplateId.ToString("B").ToUpper()))
                    ? this.Output(node.Value)
                    : String.Format(this.Output(node.Value), Environment.NewLine)
                : String.Format(this.Output(node.Value), String.Join(" ", node.Children.Select(c => this.Output(c))));
        }
    }
}
