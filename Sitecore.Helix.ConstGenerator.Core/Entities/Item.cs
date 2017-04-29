namespace Sitecore.Helix.ConstGenerator.Core.Entities
{
    using System;
    using System.Linq;
    using Sitecore.Helix.ConstGenerator.Core.Interfaces;

    public class Item : ISitecoreWebApiItem
    {
        private Guid _parentId;

        public string Category { get; set; }

        public string Database { get; set; }

        public string DisplayName { get; set; }

        public bool HasChildren { get; set; }

        public string Icon { get; set; }

        public Guid ID { get; set; }

        public string Language { get; set; }

        public string LongID { get; set; }

        public string MediaUrl { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public Guid ParentId
        {
            get
            {
                if (string.IsNullOrEmpty(LongID))
                    throw new ArgumentNullException(nameof(LongID));

                if (!_parentId.Equals(Guid.Empty)) return _parentId;

                var ids = LongID.Split('/');
                _parentId = new Guid(ids[ids.Count() - 2]);

                return _parentId;
            }
            set { _parentId = value; }
        }

        public string Template { get; set; }

        public Guid TemplateId { get; set; }

        public string TemplateName { get; set; }

        public string Url { get; set; }

        public int Version { get; set; }

        public object Fields { get; set; }
    }
}