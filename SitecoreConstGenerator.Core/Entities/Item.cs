using SitecoreConstGenerator.Core.Constants;
using SitecoreConstGenerator.Core.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SitecoreConstGenerator.Core.Entities
{
    public class Item : IWebApiItem
    {
        public string Category { get; set; }

        public string Database { get; set; }

        public string DisplayName { get; set; }

        public bool HasChildren { get; set; }

        public string Icon { get; set; }

        public System.Guid ID { get; set; }

        public string Language { get; set; }

        public string LongID { get; set; }

        public string MediaUrl { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public System.Guid ParentId 
        {
            get
            { 
                if(string.IsNullOrEmpty(this.LongID))
                    throw new ArgumentNullException("LongID");

                String[] ids = this.LongID.Split('/');

                return new Guid(ids[ids.Count() - 2]);
            }
            set
            {
                this.ParentId = value;
            }
        }

        public string Template { get; set; }

        public System.Guid TemplateId { get; set; }

        public string TemplateName { get; set; }

        public string Url { get; set; }

        public int Version { get; set; }

        public object Fields { get; set; }

    }
}
