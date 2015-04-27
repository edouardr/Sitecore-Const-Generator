using SitecoreConstGenerator.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SitecoreConstGenerator.Core.Entities
{
    public class ItemNode
    {
        public Item Value { get; set; }
        public Guid ParentId { get; set; }
        public List<ItemNode> Children { get; set; }
    }
}
