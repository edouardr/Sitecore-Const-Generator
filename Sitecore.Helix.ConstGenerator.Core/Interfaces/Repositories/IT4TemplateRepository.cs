namespace Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories
{
    using System.Collections.Generic;
    using Sitecore.Helix.ConstGenerator.Core.Entities;

    public interface IT4TemplateRepository
    {
        IWebApiRepository Repo { get; set; }

        IEnumerable<ItemNode> CreateTree(string rootPath);

        string Output(Item item);

        string Output(ItemNode node);
    }
}