using System;
using System.Text;
using System.Xml;
using Xunit;
using OPMLCore.NET;

namespace test
{
    public class UnitTest1
    {
        [Fact]
        public void NormalTest()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            xml.Append("<opml version=\"2.0\">");
            xml.Append("<head>");
            xml.Append("<title>mySubscriptions.opml</title>");
            xml.Append("<dateCreated>Sat, 18 Jun 2005 12:11:52 GMT</dateCreated>");
            xml.Append("<dateModified>Tue, 02 Aug 2005 21:42:48 GMT</dateModified>");
            xml.Append("<ownerName>fnya</ownerName>");
            xml.Append("<ownerEmail>fnya@example.com</ownerEmail>");
            xml.Append("</head>");
            xml.Append("<body>");
            xml.Append("<outline ");
            xml.Append("text=\"CNET News.com\" ");
            xml.Append("description=\"Tech news and business reports by CNET News.com.\" ");
            xml.Append("htmlUrl=\"http://news.com.com/\" ");
            xml.Append("language=\"unknown\" ");
            xml.Append("title=\"CNET News.com\" ");
            xml.Append("type=\"rss\" ");
            xml.Append("version=\"RSS2\" ");
            xml.Append("xmlUrl=\"http://news.com.com/2547-1_3-0-5.xml\" ");
            xml.Append("/>");           
            xml.Append("</body>");
            xml.Append("</opml>");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.ToString());
            Opml opml = new Opml(doc);

            Assert.True(opml.Head.Title == "mySubscriptions.opml");
            Assert.True(opml.Head.DateCreated == DateTime.Parse("Sat, 18 Jun 2005 12:11:52 GMT"));
            Assert.True(opml.Head.DateModified == DateTime.Parse("Tue, 02 Aug 2005 21:42:48 GMT"));
            Assert.True(opml.Head.OwnerName == "fnya");
            Assert.True(opml.Head.OwnerEmail == "fnya@example.com");
            Assert.True(opml.Body.Outlines[0].Text == "CNET News.com");
            Assert.True(opml.Body.Outlines[0].Description == "Tech news and business reports by CNET News.com.");
            Assert.True(opml.Body.Outlines[0].HTMLUrl == "http://news.com.com/");
            Assert.True(opml.Body.Outlines[0].Language == "unknown");
            Assert.True(opml.Body.Outlines[0].Title == "CNET News.com");
            Assert.True(opml.Body.Outlines[0].Type == "rss");
            Assert.True(opml.Body.Outlines[0].Version == "RSS2");
            Assert.True(opml.Body.Outlines[0].XMLUrl == "http://news.com.com/2547-1_3-0-5.xml");
        }

        [Fact]
        public void ChildNodeTest()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            xml.Append("<opml version=\"2.0\">");
            xml.Append("<head>");
            xml.Append("<title>mySubscriptions.opml</title>");
            xml.Append("</head>");
            xml.Append("<body>");
            xml.Append("<outline ");
            xml.Append("text=\"IT\" >");  
            xml.Append("<outline ");
            xml.Append("text=\"washingtonpost.com\" ");
            xml.Append("htmlUrl=\"http://www.washingtonpost.com\" ");
            xml.Append("xmlUrl=\"http://www.washingtonpost.com/rss.xml\" ");
            xml.Append("/>");
            xml.Append("</outline>");           
            xml.Append("</body>");
            xml.Append("</opml>");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.ToString());
            Opml opml = new Opml(doc);

            Assert.True(opml.Body.Outlines[0].Outlines[0].Text == "washingtonpost.com");
            Assert.True(opml.Body.Outlines[0].Outlines[0].HTMLUrl == "http://www.washingtonpost.com");
            Assert.True(opml.Body.Outlines[0].Outlines[0].XMLUrl == "http://www.washingtonpost.com/rss.xml");
        }

       [Fact]
        public void CreateNormalOpmlTest()
        {
            Opml opml = new Opml();
            opml.Encoding = "UTF-8";
            opml.Version = "2.0";

            Head head = new Head();
            head.Title = "mySubscriptions.opml";
            head.DateCreated = DateTime.Parse("Sat, 18 Jun 2005 12:11:52 GMT");
            head.DateModified = DateTime.Parse("Tue, 02 Aug 2005 21:42:48 GMT");
            head.OwnerName = "fnya";
            head.OwnerEmail = "fnya@example.com";
            opml.Head = head;

            Outline outline = new Outline();
            outline.Text = "CNET News.com";
            outline.Description = "Tech news and business reports by CNET News.com.";
            outline.HTMLUrl = "http://news.com.com/";
            outline.Language = "unknown";
            outline.Title = "CNET News.com";
            outline.Type = "rss";
            outline.Version = "RSS2";
            outline.XMLUrl = "http://news.com.com/2547-1_3-0-5.xml";

            Body body = new Body();
            body.Outlines.Add(outline);
            opml.Body = body;

            StringBuilder xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n");
            xml.Append("<opml version=\"2.0\">\r\n");
            xml.Append("<head>\r\n");
            xml.Append("<title>mySubscriptions.opml</title>\r\n");
            xml.Append("<dateCreated>Sat, 18 Jun 2005 21:11:52 GMT</dateCreated>\r\n");
            xml.Append("<dateModified>Wed, 03 Aug 2005 06:42:48 GMT</dateModified>\r\n");
            xml.Append("<ownerName>fnya</ownerName>\r\n");
            xml.Append("<ownerEmail>fnya@example.com</ownerEmail>\r\n");
            xml.Append("</head>\r\n");
            xml.Append("<body>\r\n");
            xml.Append("<outline ");
            xml.Append("text=\"CNET News.com\" ");
            xml.Append("description=\"Tech news and business reports by CNET News.com.\" ");
            xml.Append("htmlUrl=\"http://news.com.com/\" ");
            xml.Append("language=\"unknown\" ");
            xml.Append("title=\"CNET News.com\" ");
            xml.Append("type=\"rss\" ");
            xml.Append("version=\"RSS2\" ");
            xml.Append("xmlUrl=\"http://news.com.com/2547-1_3-0-5.xml\" ");
            xml.Append("/>\r\n");
            xml.Append("</body>\r\n");
            xml.Append("</opml>");

            Assert.True(opml.ToString() == xml.ToString());
        }

       [Fact]
        public void CreateChildOpmlTest()
        {
            Opml opml = new Opml();
            opml.Encoding = "UTF-8";
            opml.Version = "2.0";

            Head head = new Head();
            head.Title = "mySubscriptions.opml"; 
            opml.Head = head;

            Outline outline = new Outline();
            outline.Text = "IT";

            Outline childOutline = new Outline();
            childOutline.Text = "CNET News.com";
            childOutline.HTMLUrl = "http://news.com.com/";
            childOutline.XMLUrl = "http://news.com.com/2547-1_3-0-5.xml";

            outline.Outlines.Add(childOutline);

            Body body = new Body();
            body.Outlines.Add(outline);
            opml.Body = body;

            StringBuilder xml = new StringBuilder();
            xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n");
            xml.Append("<opml version=\"2.0\">\r\n");
            xml.Append("<head>\r\n");
            xml.Append("<title>mySubscriptions.opml</title>\r\n");
            xml.Append("</head>\r\n");
            xml.Append("<body>\r\n");
            xml.Append("<outline text=\"IT\">\r\n");
            xml.Append("<outline text=\"CNET News.com\" ");
            xml.Append("htmlUrl=\"http://news.com.com/\" ");
            xml.Append("xmlUrl=\"http://news.com.com/2547-1_3-0-5.xml\" />\r\n");
            xml.Append("</outline>\r\n");
            xml.Append("</body>\r\n");
            xml.Append("</opml>");

            Assert.True(opml.ToString() == xml.ToString());
        }
    }
}
