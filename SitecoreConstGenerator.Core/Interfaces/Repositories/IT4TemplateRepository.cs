using System;
using System.Collections.Generic;

namespace SitecoreConstGenerator.Core.Interfaces.Repositories
{
    public interface IT4TemplateRepository
    {
        IEnumerable<Core.Entities.ItemNode> CreateTree(IEnumerable<Core.Entities.Item> items);

        String Output(Core.Entities.Item item);

        String Output(Core.Entities.ItemNode node);
    }
}
