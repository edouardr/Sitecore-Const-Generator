namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Sitecore.Helix.ConstGenerator.Core.Constants;
    using Sitecore.Helix.ConstGenerator.Core.Entities;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories;

    public class RenderingsT4TemplateRepository : BaseRepository
    {
        public RenderingsT4TemplateRepository(IWebApiRepository api, string rootPath)
            : base(api, rootPath, SitecoreActionType.GetRenderginsIds, Settings.RenderingsQuery)
        {
        }

        public override string Output(Item item)
        {
            var sb = new StringBuilder();
            if (TemplatesId.TemplateControllerRenderingId.Guid.Equals(item.TemplateId) 
                || TemplatesId.TemplateViewRenderingId.Guid.Equals(item.TemplateId))
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
                ? TemplatesId.TemplateViewRenderingId.Guid.Equals(node.Value.TemplateId)
                  || TemplatesId.TemplateControllerRenderingId.Guid.Equals(node.Value.TemplateId)
                    ? Output(node.Value)
                    : string.Format(Output(node.Value), Environment.NewLine)
                : string.Format(Output(node.Value), string.Join(" ", node.Children.Select(Output)));
        }
    }
}