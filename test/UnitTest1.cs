using System;
using System.Linq;
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
            xml.Append("<ownerId>http://news.com.com/</ownerId>");
            xml.Append("<docs>http://news.com.com/</docs>");
            xml.Append("<expansionState>1, 6, 13, 16, 18, 20</expansionState>");
            xml.Append("<vertScrollState>1</vertScrollState>");
            xml.Append("<windowTop>106</windowTop>");
            xml.Append("<windowLeft>106</windowLeft>");
            xml.Append("<windowBottom>558</windowBottom>");
            xml.Append("<windowRight>479</windowRight>");
            xml.Append("</head>");
            xml.Append("<body>");
            xml.Append("<outline ");
            xml.Append("text=\"CNET News.com\" ");
            xml.Append("isComment=\"true\" ");
            xml.Append("isBreakpoint=\"true\" ");
            xml.Append("created=\"Tue, 02 Aug 2005 21:42:48 GMT\" ");
            xml.Append("category=\"/Harvard/Berkman,/Politics\" ");
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
            Assert.True(opml.Head.OwnerId == "http://news.com.com/");
            Assert.True(opml.Head.Docs == "http://news.com.com/");
            Assert.True(opml.Head.ExpansionState.ToArray().SequenceEqual("1,6,13,16,18,20".Split(',')));
            Assert.True(opml.Head.VertScrollState == "1");
            Assert.True(opml.Head.WindowTop == "106");
            Assert.True(opml.Head.WindowLeft == "106");
            Assert.True(opml.Head.WindowBottom == "558");
            Assert.True(opml.Head.WindowRight == "479");

            foreach (var outline in opml.Body.Outlines)
            {
                Assert.True(outline.Text == "CNET News.com");
                Assert.True(outline.IsComment == "true");
                Assert.True(outline.IsBreakpoint == "true");
                Assert.True(outline.Created == DateTime.Parse("Tue, 02 Aug 2005 21:42:48 GMT"));
                Assert.True(outline.Category.ToArray().SequenceEqual("/Harvard/Berkman,/Politics".Split(',')));
                Assert.True(outline.Description == "Tech news and business reports by CNET News.com.");
                Assert.True(outline.HTMLUrl == "http://news.com.com/");
                Assert.True(outline.Language == "unknown");
                Assert.True(outline.Title == "CNET News.com");
                Assert.True(outline.Type == "rss");
                Assert.True(outline.Version == "RSS2");
                Assert.True(outline.XMLUrl == "http://news.com.com/2547-1_3-0-5.xml");
            }
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

            foreach (var outline in opml.Body.Outlines)
            {
                foreach(var childOutline in outline.Outlines) {
                    Assert.True(childOutline.Text == "washingtonpost.com");
                    Assert.True(childOutline.HTMLUrl == "http://www.washingtonpost.com");
                    Assert.True(childOutline.XMLUrl == "http://www.washingtonpost.com/rss.xml");
                }
            }
        }

       [Fact]
        public void CreateNormalOpmlTest()
        {
            Opml opml = new Opml();
            opml.Encoding = "UTF-8";
            opml.Version = "2.0";

            Head head = new Head();
            head.Title = "mySubscriptions.opml";
            head.DateCreated = DateTime.Parse("Sat, 18 Jun 2005 12:11:52 GMT").ToUniversalTime();
            head.DateModified = DateTime.Parse("Tue, 02 Aug 2005 21:42:48 GMT").ToUniversalTime();
            head.OwnerName = "fnya";
            head.OwnerEmail = "fnya@example.com";
            head.OwnerId = "http://news.com.com/";
            head.Docs = "http://news.com.com/";
            head.ExpansionState.Add("1");
            head.ExpansionState.Add("6");
            head.ExpansionState.Add("13");
            head.ExpansionState.Add("16");
            head.ExpansionState.Add("18");
            head.ExpansionState.Add("20");
            head.VertScrollState = "1";
            head.WindowTop = "106";
            head.WindowLeft = "106";
            head.WindowBottom = "558";
            head.WindowRight = "479";
            opml.Head = head;

            Outline outline = new Outline();
            outline.Text = "CNET News.com";
            outline.IsComment = "true";
            outline.IsBreakpoint = "true";
            outline.Created = DateTime.Parse("Tue, 02 Aug 2005 21:42:48 GMT").ToUniversalTime();
            outline.Category.Add("/Harvard/Berkman");
            outline.Category.Add("/Politics");
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
            xml.Append("<dateCreated>Sat, 18 Jun 2005 12:11:52 GMT</dateCreated>\r\n");
            xml.Append("<dateModified>Tue, 02 Aug 2005 21:42:48 GMT</dateModified>\r\n");
            xml.Append("<ownerName>fnya</ownerName>\r\n");
            xml.Append("<ownerEmail>fnya@example.com</ownerEmail>\r\n");
            xml.Append("<ownerId>http://news.com.com/</ownerId>\r\n");
            xml.Append("<docs>http://news.com.com/</docs>\r\n");
            xml.Append("<expansionState>1,6,13,16,18,20</expansionState>\r\n");
            xml.Append("<vertScrollState>1</vertScrollState>\r\n");
            xml.Append("<windowTop>106</windowTop>\r\n");
            xml.Append("<windowLeft>106</windowLeft>\r\n");
            xml.Append("<windowBottom>558</windowBottom>\r\n");
            xml.Append("<windowRight>479</windowRight>\r\n");
            xml.Append("</head>\r\n");
            xml.Append("<body>\r\n");
            xml.Append("<outline ");
            xml.Append("text=\"CNET News.com\" ");
            xml.Append("isComment=\"true\" ");
            xml.Append("isBreakpoint=\"true\" ");
            xml.Append("created=\"Tue, 02 Aug 2005 21:42:48 GMT\" ");
            xml.Append("category=\"/Harvard/Berkman,/Politics\" ");
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

            // Console.WriteLine(xml.ToString());
            // Console.WriteLine(opml.ToString());
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
