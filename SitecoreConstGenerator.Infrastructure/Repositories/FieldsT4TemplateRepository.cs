using SitecoreConstGenerator.Core.Constants;
using SitecoreConstGenerator.Core.Entities;
using SitecoreConstGenerator.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SitecoreConstGenerator.Core.Interfaces.Entities;

namespace SitecoreConstGenerator.Infrastructure.Repositories
{
    public class FieldsT4TemplateRepository : IT4TemplateRepository
    {
        public IWebApiRepository Repo { get; set; }

        public FieldsT4TemplateRepository(IWebApiRepository repo)
        {
            this.Repo = repo;
        }
        
        public IEnumerable<ItemNode> CreateTree(string rootPath)
        {
            // get items
            Task<IWebApiRequestResult<Result, Item>> task = this.Repo.RequestFieldsIdsAsync(rootPath);
            RequestResult result = task.Result as RequestResult;
            if (result != null)
            {
                IEnumerable<Item> items = result.Result.Items;if (null == items)
                    throw new ArgumentNullException("items");

                var enumerable = items as Item[] ?? items.ToArray();
                if (!enumerable.Any())
                    return null;

                List<Item> list = enumerable
                    .OrderBy(i => i.Path)
                    .ToList();

                list.RemoveAll(i => i.DisplayName.Equals("__Standard Values"));

                Dictionary<Guid, ItemNode> lookup = new Dictionary<Guid, ItemNode>();
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
                return lookup.Values.Where(x => null == x.ParentId || x.ParentId.Equals(Guid.Empty));
            }

            return null;
        }

        public String Output(Item item)
        {
            StringBuilder sb = new StringBuilder();
            switch (item.TemplateId.ToString("B").ToUpper())
            {
                case TemplatesId.TemplateSectionId:
                    sb.AppendFormat("#region {0} ", Regex.Replace(item.DisplayName, @"\s+", ""));
                    sb.AppendLine();
                    sb.AppendLine(" {0} ");
                    sb.AppendLine("#endregion");
                    break;
                case TemplatesId.TemplateFieldId:
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

        public String Output(ItemNode node)
        {
            return (null == node.Children)
                ? TemplatesId.TemplateFieldId.Equals(node.Value.TemplateId.ToString("B").ToUpper())
                    ? this.Output(node.Value)
                    : String.Format(this.Output(node.Value), Environment.NewLine)
                : String.Format(this.Output(node.Value), String.Join(" ", node.Children.Select(this.Output)));
        }
    }
}
