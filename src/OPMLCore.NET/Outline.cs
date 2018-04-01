using System;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace OPMLCore.NET {
    public class Outline 
    {
        ///<summary>
        /// Text of the XML file (required)
        ///</summary>
        public string Text { get; set; }

        ///<summary>
        /// Description
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        /// HTML URL
        ///</summary>
        public string HTMLUrl { get; set; }

        ///<summary>
        /// Language
        ///</summary>
        public string Language { get; set; }

        ///<summary>
        /// Title 
        ///</summary>
        public string Title { get; set; }

        ///<summary>
        /// Type (rss/atom)
        ///</summary>
        public string Type { get; set; }

        ///<summary>
        /// Version
        ///</summary>
        public string Version { get; set; }

        ///<summary>
        /// URL of the XML file
        ///</summary>
        public string XMLUrl { get; set; }

        ///<summary>
        /// Outline list
        ///</summary>
        public List<Outline> Outlines { get; set; }  = new List<Outline>();

        ///<summary>
        /// Constructor
        ///</summary>
        public Outline() 
        {

        }
    

        ///<summary>
        /// Constructor
        ///</summary>
        /// <param name="element">element of Head</param>
        public Outline(XmlElement element) 
        {
            Text        = element.GetAttribute("text");
            Description = element.GetAttribute("description");
            HTMLUrl     = element.GetAttribute("htmlUrl");
            Language    = element.GetAttribute("language");
            Title       = element.GetAttribute("title");
            Type        = element.GetAttribute("type");
            Version     = element.GetAttribute("version");
            XMLUrl      = element.GetAttribute("xmlUrl");
   
            if (element.HasChildNodes) {
                foreach (XmlNode child in element.ChildNodes)
                {
                    if (child.Name.Equals("outline", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Outlines.Add(new Outline((XmlElement) child));
                    }
                }
            }
        }

        public override string ToString() 
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("<outline");
            buf.Append(GetAtrributeString("text", Text));
            buf.Append(GetAtrributeString("description", Description));
            buf.Append(GetAtrributeString("htmlUrl", HTMLUrl));
            buf.Append(GetAtrributeString("language", Language));
            buf.Append(GetAtrributeString("title", Title));
            buf.Append(GetAtrributeString("type", Type));
            buf.Append(GetAtrributeString("version", Version));
            buf.Append(GetAtrributeString("xmlUrl", XMLUrl));

            if (Outlines.Count > 0) 
            {
                buf.Append(">\r\n");
                foreach (Outline outline in Outlines)
                {
                    buf.Append(outline.ToString());        
                }
                buf.Append("</outline>\r\n");   
            } else {
                buf.Append(" />\r\n");    
            }
            return buf.ToString();
        }

        private string GetAtrributeString(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            } else {
                return $" {name}=\"{value}\"";
            }
        }          
    }
}