namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;
    using System;

    class FeedParserTests
    {
        [Test]
        public void ParseMethodShouldParseValidXMLString()
        {
            var xmlString = @"<rss version=""2.0"">
                                <channel>
                                    <title>Liftoff News</title>
                                    <link>http://liftoff.msfc.nasa.gov/</link>
                                    <description>Liftoff to Space Exploration.</description>
                                    <language>en-us</language>
                                    <pubDate>Tue, 10 Jun 2003 04:00:00 GMT</pubDate>
                                    <lastBuildDate>Tue, 10 Jun 2003 09:41:01 GMT</lastBuildDate>
                                    <docs>http://blogs.law.harvard.edu/tech/rss</docs>
                                    <generator>Weblog Editor 2.0</generator>
                                    <managingEditor>editor@example.com</managingEditor>
                                    <webMaster>webmaster@example.com</webMaster>
                                    <item>
                                        <title>Star City</title>
                                        <link>
                                        http://liftoff.msfc.nasa.gov/news/2003/news-starcity.asp
                                        </link>
                                        <description>
                                        How do Americans get ready to work with Russians aboard the International Space Station? They take a crash course in culture, language and protocol at Russia's <a href=""http://howe.iki.rssi.ru/GCTC/gctc_e.htm"">Star City</a>.
                                        </description>
                                        <pubDate>Tue, 03 Jun 2003 09:39:21 GMT</pubDate>
                                        <guid>
                                        http://liftoff.msfc.nasa.gov/2003/06/03.html#item573
                                        </guid>
                                    </item>
                                    <item>
                                        <description>
                                        Sky watchers in Europe, Asia, and parts of Alaska and Canada will experience a <a href=""http://science.nasa.gov/headlines/y2003/30may_solareclipse.htm"">partial eclipse of the Sun</a> on Saturday, May 31st.
                                        </description>
                                        <pubDate>Fri, 30 May 2003 11:06:42 GMT</pubDate>
                                        <guid>
                                        http://liftoff.msfc.nasa.gov/2003/05/30.html#item572
                                        </guid>
                                        </item>
                                        <item>
                                        <title>The Engine That Does More</title>
                                        <link>
                                        http://liftoff.msfc.nasa.gov/news/2003/news-VASIMR.asp
                                        </link>
                                        <description>
                                        Before man travels to Mars, NASA hopes to design new engines that will let us fly through the Solar System more quickly. The proposed VASIMR engine would do that.
                                        </description>
                                        <pubDate>Tue, 27 May 2003 08:37:32 GMT</pubDate>
                                        <guid>
                                        http://liftoff.msfc.nasa.gov/2003/05/27.html#item571
                                        </guid>
                                    </item>
                                    <item>
                                        <title>Astronauts' Dirty Laundry</title>
                                        <link>
                                        http://liftoff.msfc.nasa.gov/news/2003/news-laundry.asp
                                        </link>
                                        <description>
                                        Compared to earlier spacecraft, the International Space Station has many luxuries, but laundry facilities are not one of them. Instead, astronauts have other options.
                                        </description>
                                        <pubDate>Tue, 20 May 2003 08:56:02 GMT</pubDate>
                                        <guid>
                                        http://liftoff.msfc.nasa.gov/2003/05/20.html#item570
                                        </guid>
                                    </item>
                                  </channel>
                                </rss>";

            var feedParser = new FeedParser();
            var feed = feedParser.Parse(xmlString);
            var feedValidator = new FeedValidator();

            var result = feedValidator.TestValidate(feed);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void NullXmlStringShouldThrowArgumentNullException()
        {
            var feedParser = new FeedParser();

            var ex = Assert.Throws<ArgumentNullException>(() => feedParser.Parse(null));

            Assert.AreEqual(ex.Message, $"Value cannot be null. (Parameter '{ex.ParamName}')");
        }

        [Test]
        public void EmptyXmlStringShouldThrowArgumentException()
        {
            var feedParser = new FeedParser();

            var ex = Assert.Throws<ArgumentException>(() => feedParser.Parse(""));

            Assert.AreEqual(ex.Message, $"Cannot be empty (Parameter '{ex.ParamName}')");
        }

        [Test]
        public void InvalidXmlStringShouldThrowArgumentException()
        {
            var invalidXml = "this is not valid xml";
            var feedParser = new FeedParser();

            var ex = Assert.Throws<ArgumentException>(() => feedParser.Parse(invalidXml));

            Assert.AreEqual(ex.Message, $"Is not valid XML (Parameter '{ex.ParamName}')");
        }

        [Test]
        public void NonRssRootElementShouldThrowArgumentException()
        {
            var nonRssFeed = @"<notrss></notrss>";
            var feedParser = new FeedParser();

            var ex = Assert.Throws<ArgumentException>(() => feedParser.Parse(nonRssFeed));

            Assert.AreEqual(ex.Message, $"Root node is not <rss> (Parameter '{ex.ParamName}')");
        }

        [Test]
        public void NonRss2ShouldThrowArgumentException()
        {
            var nonRss2Feed = @"<rss version=""0.91""></rss>";
            var feedParser = new FeedParser();

            var ex = Assert.Throws<ArgumentException>(() => feedParser.Parse(nonRss2Feed));

            Assert.AreEqual(ex.Message, $"RSS feed is not version 2 (Parameter '{ex.ParamName}')");
        }
    }
}
