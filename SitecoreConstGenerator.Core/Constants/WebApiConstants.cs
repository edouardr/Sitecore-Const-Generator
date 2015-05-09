using System.Collections.Generic;

namespace SitecoreConstGenerator.Core.Constants
{
    public static class WebApiConstants
    {
        public static readonly Dictionary<SitecoreActionType, string> SitecoreUris = new Dictionary
            <SitecoreActionType, string>
        {
            {SitecoreActionType.GetFieldsIds, "/-/item/{0}/?scope=p|c&payload=min&query={1}//*"},
            {SitecoreActionType.GetTemplatesIds, "/-/item/{0}/?scope=s&payload=min&query={1}"},
            {SitecoreActionType.GetRenderginsIds, "/-/item/{0}/?scope=s|c*&payload=min&query={1}"}
        };
    }

    public enum SitecoreActionType
    {
        GetFieldsIds = 0,
        GetTemplatesIds = 1,
        GetRenderginsIds = 2,
    }
}
