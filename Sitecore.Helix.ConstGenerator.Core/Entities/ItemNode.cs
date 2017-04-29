namespace Sitecore.Helix.ConstGenerator.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class ItemNode
    {
        public Item Value { get; set; }
        public Guid ParentId { get; set; }
        public List<ItemNode> Children { get; set; }
    }
}