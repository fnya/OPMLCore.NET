using System;
using System.Text;
using System.Xml;

namespace OPMLCore.NET {
    public class Opml {
        ///<summary>
        /// Version of OPML
        ///</summary>
        public string Version { get; set;}

        ///<summary>
        /// Encoding of OPML
        ///</summary>
        public string Encoding { get; set;}

        ///<summary>
        /// Head of OPML
        ///</summary>
        public Head Head { get; set;} = new Head();

        ///<summary>
        /// Body of OPML
        ///</summary>        
        public Body Body { get; set;} = new Body();

        ///<summary>
        /// Constructor
        ///</summary>
        public Opml() 
        {

        }

        ///<summary>
        /// Constructor
        ///</summary>
        /// <param name="location">Location of the OPML file</param>
        public Opml(string location) 
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(location);
            readOpmlNodes(doc);
        }

          ///<summary>
        /// Constructor
        ///</summary>
        /// <param name="doc">XMLDocument of the OPML</param>
        public Opml(XmlDocument doc) 
        {
            readOpmlNodes(doc);
        }      


        private void readOpmlNodes(XmlDocument doc) {
            foreach (XmlNode nodes in doc) 
            {
                if (nodes.Name.Equals("opml", StringComparison.CurrentCultureIgnoreCase)) 
                {
                    foreach (XmlNode childNode in nodes)
                    {

                        if (childNode.Name.Equals("head", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Head = new Head((XmlElement) childNode);
                        }

                        if (childNode.Name.Equals("body", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Body = new Body((XmlElement) childNode);
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();
            String ecoding = string.IsNullOrEmpty(Encoding)?"UTF-8":Encoding;
            buf.Append($"<?xml version=\"1.0\" encoding=\"{ecoding}\" ?>\r\n");
            String version = string.IsNullOrEmpty(Version)?"2.0":Version;
            buf.Append($"<opml version=\"{version}\">\r\n");
            buf.Append(Head.ToString());
            buf.Append(Body.ToString());
            buf.Append("</opml>");

            return buf.ToString();
        }

    }
}