namespace Sitecore.Helix.ConstGenerator.Core.Interfaces.Entities
{
    using System;

    public interface IWebApiItem
    {
        string Category { get; set; }
        string Database { get; set; }
        string DisplayName { get; set; }
        bool HasChildren { get; set; }
        string Icon { get; set; }
        Guid ID { get; set; }
        string Language { get; set; }
        string LongID { get; set; }
        string MediaUrl { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        Guid ParentId { get; set; }
        string Template { get; set; }
        Guid TemplateId { get; set; }
        string TemplateName { get; set; }
        string Url { get; set; }
        int Version { get; set; }
        object Fields { get; set; }
    }
}