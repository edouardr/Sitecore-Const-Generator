using System;
using System.Linq;
using SitecoreConstGenerator.Core.Interfaces.Entities;

namespace SitecoreConstGenerator.Core.Entities
{
    public class Item : IWebApiItem
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
                if(string.IsNullOrEmpty(this.LongID))
                    throw new ArgumentNullException(@"LongID");

                if (this._parentId.Equals(Guid.Empty))
                {
                    String[] ids = this.LongID.Split('/');

                    this._parentId = new Guid(ids[ids.Count() - 2]);
                }

                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        public string Template { get; set; }

        public Guid TemplateId { get; set; }

        public string TemplateName { get; set; }

        public string Url { get; set; }

        public int Version { get; set; }

        public object Fields { get; set; }

    }
}
