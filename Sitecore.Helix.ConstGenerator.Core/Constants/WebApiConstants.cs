namespace Sitecore.Helix.ConstGenerator.Core.Constants
{
  using System.Collections.Generic;

  public struct WebApiConstants
  {
    public static readonly Dictionary<SitecoreActionType, string> SitecoreUris = new Dictionary
      <SitecoreActionType, string>
      {
        {SitecoreActionType.GetTemplatesIds, @"/-/item/{0}/?scope=p|c&payload=min&query={1}"},
        {SitecoreActionType.GetRenderginsIds, @"/-/item/{0}/?scope=s|c*&payload=min&query={1}"}
      };
  }

  public enum SitecoreActionType
  {
    GetTemplatesIds = 1,
    GetRenderginsIds = 2
  }
}