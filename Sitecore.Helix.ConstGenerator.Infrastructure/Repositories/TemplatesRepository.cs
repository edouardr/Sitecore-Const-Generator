namespace Sitecore.Helix.ConstGenerator.Infrastructure.Repositories
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Helix.ConstGenerator.Core.Constants;
  using Sitecore.Helix.ConstGenerator.Core.Entities;
  using Sitecore.Helix.ConstGenerator.Core.Interfaces.Repositories;

  public class TemplatesRepository : BaseRepository
  {
    private readonly string _rootPath;

    public TemplatesRepository(IWebApiRepository api, string rootPath)
      : base(
        api, rootPath, SitecoreActionType.GetTemplatesIds, Settings.TemplatesQuery)
    {
      _rootPath = rootPath;
    }

    public override IEnumerable<ItemNode> CreateTree()
    {
      var items = GetItemsFromRoot(_rootPath).ToList();

      items.RemoveAll(i => i.DisplayName.Equals("__Standard Values"));

      var lookup = RemoveDuplicates(items);
      lookup = LinkWithParent(lookup);

      return GetRootNodes(lookup);
    }
  }
}