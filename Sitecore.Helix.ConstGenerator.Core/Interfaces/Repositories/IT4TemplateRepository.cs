namespace Sitecore.Helix.ConstGenerator.Core.Interfaces
{
    using System.Collections.Generic;
    using Sitecore.Helix.ConstGenerator.Core.Entities;

    public interface IT4TemplateRepository
    {
        IEnumerable<ItemNode> CreateTree();
    }
}