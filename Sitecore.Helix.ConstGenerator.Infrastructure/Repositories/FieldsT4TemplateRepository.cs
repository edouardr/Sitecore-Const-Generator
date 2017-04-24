namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories;

    public class FieldsT4TemplateRepository : TemplatesT4TemplateRepository
    {
        public FieldsT4TemplateRepository(IWebApiRepository api, string rootPath)
            : base(api, rootPath, SitecoreActionType.GetTemplatesIds, Settings.TemplatesQuery)
        {
        }

        public override string Output(Item item)
        {
            var sb = new StringBuilder();
            if (TemplatesId.TemplateSectionId.Guid.Equals(item.TemplateId))
            {
                sb.AppendFormat("#region {0} ", Regex.Replace(item.DisplayName, @"\s+", ""));
                sb.AppendLine();
                sb.AppendLine(" {0} ");
                sb.AppendLine("#endregion");
            }
            else if (TemplatesId.TemplateFieldId.Guid.Equals(item.TemplateId))
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
                ? TemplatesId.TemplateFieldId.Guid.Equals(node.Value.TemplateId)
                    ? Output(node.Value)
                    : string.Format(Output(node.Value), Environment.NewLine)
                : string.Format(Output(node.Value), string.Join(" ", node.Children.Select(Output)));
        }
    }
}