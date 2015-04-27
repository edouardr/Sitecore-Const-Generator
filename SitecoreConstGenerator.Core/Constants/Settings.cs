using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitecoreConstGenerator.Core.Constants
{
    public class Settings
    {
        public const string Tool = "Sitecore Constants Generator - FieldIds.tt";
        public const string ToolVersion = "1.0";
        public const string ItemWebApiVersion = "v1";
        public const string FieldsQuery = "{0}//*";
        public const string FieldsRequest = "?scope=s|c&payload=min&query={0}//*";
        public const string TemplatesQuery = "{0}//*[@@templateid='{1}' or @@templateid='{2}']";
        public const string TemplatesRequest = "?scope=s&payload=min&query={0}";
        public const string RenderingsQuery = "{0}//*";
        public const string RenderingsRequest = "?scope=s|c*&payload=min&query={0}";
    }
}
