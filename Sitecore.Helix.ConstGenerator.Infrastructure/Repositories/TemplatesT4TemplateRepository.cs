namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories;

    public class TemplatesT4TemplateRepository : BaseRepository
    {
        private readonly string _rootPath;

        public TemplatesT4TemplateRepository(IWebApiRepository api, string rootPath)
            : base(api, rootPath, SitecoreActionType.GetTemplatesIds, Settings.TemplatesQuery, TemplatesId.TemplateFolderId, TemplatesId.TemplateFieldId)
        {
            _rootPath = rootPath;
        }

        protected TemplatesT4TemplateRepository(IWebApiRepository api, string rootPath, SitecoreActionType actionType,
            string query)
            : base(api, rootPath, actionType, query, TemplatesId.TemplateFolderId, TemplatesId.TemplateFieldId)
        {
            _rootPath = rootPath;
        }

        public override IEnumerable<ItemNode> CreateTree()
        {
            var items = GetItemsFromRoot(_rootPath).ToList();

            items.RemoveAll(i => i.DisplayName.Equals("__Standard Values")
                                || TemplatesId.TemplateFieldId.Guid.Equals(i.TemplateId));

            var lookup = RemoveDuplicates(items);
            lookup = LinkWithParent(lookup);

            return GetRootNodes(lookup);
        }

        public override string Output(Item item)
        {
            var sb = new StringBuilder();
            if (TemplatesId.TemplateId.Guid.Equals(item.TemplateId))
            {
                sb.AppendFormat(
                    @"public const string {0} = @""{1}"";",
                    Regex.Replace(item.DisplayName, @"\s+", ""),
                    item.ID.ToString("B").ToUpper());
                sb.AppendLine();
            }
            else
            {
                sb.AppendFormat("public class {0} ", Regex.Replace(item.DisplayName, @"\s+", ""));
                sb.AppendLine();
                sb.AppendLine("{{");
                sb.AppendLine("{0}");
                sb.AppendLine("}}");
            }

            return sb.ToString();
        }

        public override string Output(ItemNode node)
        {
            return null == node.Children
                ? TemplatesId.TemplateId.Guid.Equals(node.Value.TemplateId)
                    ? Output(node.Value)
                    : string.Format(Output(node.Value), Environment.NewLine)
                : string.Format(Output(node.Value), string.Join(" ", node.Children.Select(Output)));
        }
    }
}