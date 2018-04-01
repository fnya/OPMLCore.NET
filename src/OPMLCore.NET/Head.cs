using System;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace OPMLCore.NET {
    public class Head 
    {
        ///<summary>
        /// Title
        ///</summary>
        public string Title { get; set; }

        ///<summary>
        /// Created date
        ///</summary>
        public DateTime? DateCreated { get; set; } = null;

        ///<summary>
        /// Modified date
        ///</summary>
        public DateTime? DateModified { get; set; } = null;

        ///<summary>
        /// OwnerName
        ///</summary>
        public string OwnerName { get; set; }

        ///<summary>
        /// OwnerEmail
        ///</summary>
        public string OwnerEmail { get; set; }

        ///<summary>
        /// Constructor
        ///</summary>
        public Head()
        {

        }

        ///<summary>
        /// Constructor
        ///</summary>
        /// <param name="element">element of Head</param>
        public Head(XmlElement element) 
        {
            if (element.Name.Equals("head", StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (XmlNode node in element.ChildNodes) 
                {
                    Title        = GetStringValue(node, "title", Title);
                    DateCreated  = GetDateTimeValue(node, "dateCreated", DateCreated);
                    DateModified = GetDateTimeValue(node, "dateModified", DateModified);
                    OwnerName    = GetStringValue(node, "ownerName", OwnerName);
                    OwnerEmail   = GetStringValue(node, "ownerEmail", OwnerEmail);
                }
            }
        }

        private string GetStringValue(XmlNode node, string name, string value)
        {
            if (node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
            {
                return node.InnerText;
            } else if (!node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)) {
                return value;
            } else {
                return string.Empty;
            }
        }

        private DateTime? GetDateTimeValue(XmlNode node, string name, DateTime? value) 
        {
            if (node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
            {
                try {
                     return DateTime.Parse(node.InnerText);
                } catch {
                    return null;
                }
            } else if (!node.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)){
                return value;
            } else {    
                return null;
            }
        }    

        public override string ToString() 
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("<head>\r\n");
            buf.Append(GetNodeString("title", Title));
            buf.Append(GetNodeString("dateCreated", DateCreated));
            buf.Append(GetNodeString("dateModified", DateModified));
            buf.Append(GetNodeString("ownerName", OwnerName));
            buf.Append(GetNodeString("ownerEmail", OwnerEmail));
            buf.Append("</head>\r\n");

            return buf.ToString();
        }

        private string GetNodeString(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            } else {
                return $"<{name}>{value}</{name}>\r\n";
            }
        }
        private string GetNodeString(string name, DateTime? value)
        {
            if (value == null)
            {
                return string.Empty;
            } else {
                return $"<{name}>{value?.ToString("R")}</{name}>\r\n";
            }
        }           
    }
}