using System;
using System.Collections.Generic;

namespace SitecoreConstGenerator.Core.Interfaces.Repositories
{
    public interface IT4TemplateRepository
    {
        IWebApiRepository Repo { get; set; }

        IEnumerable<Core.Entities.ItemNode> CreateTree(string rootPath);

        String Output(Core.Entities.Item item);

        String Output(Core.Entities.ItemNode node);
    }
}
