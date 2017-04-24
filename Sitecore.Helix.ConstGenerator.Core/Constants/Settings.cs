namespace Sitecore.Helix.ConstGenerator.Core.Constants
{
    public struct Settings
    {
        public static readonly string Tool = @"Sitecore Constants Generator - FieldIds.tt";
        public static readonly string ToolVersion = @"1.0";
        public static readonly string ItemWebApiVersion = @"v1";
        public static readonly string FieldsQuery = @"{0}//*";
        public static readonly string TemplatesQuery = @"{0}//*[@@templateid='{1}' or @@templateid='{2}']";
        public static readonly string RenderingsQuery = @"{0}//*";
    }
}