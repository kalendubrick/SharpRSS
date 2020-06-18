// <copyright file="FeedParser.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Parses XML into a <see cref="Feed"/> object.
    /// </summary>
    public class FeedParser
    {
        /// <summary>
        /// Parses a string representation of an XML feed into a <see cref="Feed"/> object.
        /// </summary>
        /// <param name="rssFeed">The string representation of the XML feed.</param>
        /// <returns>The parsed <see cref="Feed"/> object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="rssFeed"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="rssFeed"/> is empty or rss version is not 2.</exception>
        public Feed Parse(string rssFeed)
        {
            var xmlDoc = new XmlDocument();

            if (rssFeed is null)
            {
                throw new ArgumentNullException(nameof(rssFeed));
            }

            if (string.IsNullOrWhiteSpace(rssFeed))
            {
                throw new ArgumentException(Properties.Resources.ErrorMessage_EmptyFeedString, nameof(rssFeed));
            }

            try
            {
                xmlDoc.LoadXml(rssFeed);
            }
            catch (XmlException e)
            {
                throw new FormatException(Properties.Resources.ErrorMessage_InvalidXmlString, e);
            }

            var root = xmlDoc.DocumentElement;

            if (!string.Equals(root.Name, "rss", StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException(Properties.Resources.ErrorMessage_NonRssRootString);
            }

            if (root.HasAttribute("version") &&
                !string.Equals(root.GetAttribute("version"), "2.0", StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException(Properties.Resources.ErrorMessage_NonRss2RootString);
            }

            if (root.ChildNodes.Count < 1 || root.FirstChild.Name != "channel")
            {
                throw new FormatException(Properties.Resources.ErrorMessage_MissingChannel);
            }

            var feedElement = root.FirstChild as XmlElement;
            var feed = new Feed();
            var dateFmt = new CultureInfo("en-us").DateTimeFormat;
            var numFmt = new CultureInfo("en-us").NumberFormat;

            foreach (XmlElement child in feedElement.ChildNodes)
            {
                switch (child.Name)
                {
                    case "title":
                        feed.Title = child.InnerText;
                        break;
                    case "link":
                        feed.Link = child.InnerText;
                        break;
                    case "description":
                        feed.Description = child.InnerText;
                        break;
                    case "language":
                        feed.Language = child.InnerText;
                        break;
                    case "copyright":
                        feed.Copyright = child.InnerText;
                        break;
                    case "managingEditor":
                        feed.ManagingEditor = new FeedPerson() { EmailAddress = child.InnerText };
                        break;
                    case "webmaster":
                        feed.Webmaster = new FeedPerson() { EmailAddress = child.InnerText };
                        break;
                    case "publishDate":
                        if (!string.IsNullOrWhiteSpace(feed.Language))
                        {
                            dateFmt = new CultureInfo(feed.Language).DateTimeFormat;
                        }

                        feed.PublishDate = DateTimeOffset.Parse(child.InnerText, dateFmt);
                        break;
                    case "lastBuildDate":
                        if (!string.IsNullOrWhiteSpace(feed.Language))
                        {
                            dateFmt = new CultureInfo(feed.Language).DateTimeFormat;
                        }

                        feed.LastBuildDate = DateTimeOffset.Parse(child.InnerText, dateFmt);
                        break;
                    case "category":
                        feed.Categories.Add(child.InnerText);
                        break;
                    case "generator":
                        feed.Generator = child.InnerText;
                        break;
                    case "docs":
                        feed.Docs = child.InnerText;
                        break;
                    case "cloud":
                        if (!string.IsNullOrWhiteSpace(feed.Language))
                        {
                            numFmt = new CultureInfo(feed.Language).NumberFormat;
                        }

                        feed.Cloud = ParseCloud(child, numFmt);
                        break;
                    case "ttl":
                        if (int.TryParse(child.InnerText, out var ttl))
                        {
                            feed.TimeToLive = ttl;
                        }

                        break;
                    case "image":
                        feed.Image = ParseImage(child);
                        break;
                    case "textInput":
                        feed.TextInput = ParseTextInput(child);
                        break;
                    case "skipHours":
                        if (!string.IsNullOrWhiteSpace(feed.Language))
                        {
                            numFmt = new CultureInfo(feed.Language).NumberFormat;
                        }

                        foreach (XmlElement hour in child.ChildNodes)
                        {
                            try
                            {
                                feed.SkipHours.Add(int.Parse(hour.InnerText, numFmt));
                            }
                            catch (FormatException) { }
                            catch (OverflowException) { }
                        }

                        break;
                    case "skipDays":
                        foreach (XmlElement day in child.ChildNodes)
                        {
                            feed.SkipDays.Add(day.InnerText);
                        }

                        break;
                    case "item":
                        feed.Items.Add(ParseItem(child));
                        break;
                }
            }

            return feed;
        }

        private static FeedItem ParseItem(XmlElement el, string language = "en-us")
        {
            var dateFmt = new CultureInfo(language).DateTimeFormat;
            var item = new FeedItem();

            foreach (XmlElement child in el.ChildNodes)
            {
                switch (child.Name)
                {
                    case "title":
                        item.Title = child.InnerText;
                        break;
                    case "link":
                        item.Link = child.InnerText;
                        break;
                    case "description":
                        item.Description = child.InnerText;
                        break;
                    case "author":
                        item.Author = new FeedPerson() { EmailAddress = child.InnerText };
                        break;
                    case "category":
                        item.Categories.Add(child.InnerText);
                        break;
                    case "comments":
                        item.Comments = child.InnerText;
                        break;
                    case "enclosure":
                        item.Enclosure = ParseEnclosure(child);
                        break;
                    case "itemGuid":
                        item.ItemGuid = child.InnerText;
                        break;
                    case "publishDate":
                        item.PublishDate = DateTimeOffset.Parse(child.InnerText, dateFmt);
                        break;
                    case "source":
                        if (child.HasAttribute("url"))
                        {
                            item.Source = new FeedItemSource()
                            {
                                Name = child.InnerText,
                                Url = child.GetAttribute("url"),
                            };
                        }

                        break;
                }
            }

            return item;
        }

        private static FeedItemEnclosure ParseEnclosure(XmlElement child)
        {
            var enclosure = new FeedItemEnclosure();

            if (child.HasAttribute("url"))
            {
                enclosure.Url = child.GetAttribute("url");
            }

            return enclosure;
        }

        private static FeedTextInput ParseTextInput(XmlElement el)
        {
            var textInput = new FeedTextInput();

            foreach (XmlAttribute attr in el.Attributes)
            {
                switch (attr.Name)
                {
                    case "title":
                        textInput.Title = el.InnerText;
                        break;
                    case "description":
                        textInput.Description = el.InnerText;
                        break;
                    case "name":
                        textInput.Name = el.InnerText;
                        break;
                    case "link":
                        textInput.Link = el.InnerText;
                        break;
                }
            }

            return textInput;
        }

        private static FeedCloud ParseCloud(XmlElement el, NumberFormatInfo fmt)
        {
            var feedCloud = new FeedCloud();

            foreach (XmlAttribute attr in el.Attributes)
            {
                switch (attr.Name)
                {
                    case "domain":
                        feedCloud.Domain = attr.Value;
                        break;
                    case "port":
                        try
                        {
                            feedCloud.Port = int.Parse(attr.Value, fmt);
                        }
                        catch (FormatException)
                        {
                            feedCloud.Port = 0;
                        }

                        break;
                    case "path":
                        feedCloud.Path = attr.Value;
                        break;
                    case "registerProcedure":
                        feedCloud.RegisterProcedure = attr.Value;
                        break;
                    case "protocol":
                        feedCloud.Protocol = attr.Value;
                        break;
                }
            }

            return feedCloud;
        }

        private static FeedImage ParseImage(XmlElement el)
        {
            var feedImage = new FeedImage();

            foreach (XmlElement child in el.ChildNodes)
            {
                switch (child.Name)
                {
                    case "url":
                        feedImage.Url = child.InnerText;
                        break;
                    case "title":
                        feedImage.Title = child.InnerText;
                        break;
                    case "link":
                        feedImage.Link = child.InnerText;
                        break;
                    case "description":
                        feedImage.Description = child.InnerText;
                        break;
                    case "height":
                        if (int.TryParse(child.InnerText, out var height))
                        {
                            feedImage.Height = height;
                        }

                        break;
                    case "width":
                        if (int.TryParse(child.InnerText, out var width))
                        {
                            feedImage.Width = width;
                        }

                        break;
                }
            }

            return feedImage;
        }
    }
}
