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
    using Microsoft.Extensions.Logging;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Parses XML into a <see cref="Feed"/> object.
    /// </summary>
    public class FeedParser
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedParser"/> class.
        /// </summary>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public FeedParser(ILogger<FeedParser> logger)
        {
            this.logger = logger;
        }

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
                logger.LogDebug(Properties.Resources.FeedParser_Logger_NullString);
                throw new ArgumentNullException(nameof(rssFeed));
            }

            if (string.IsNullOrWhiteSpace(rssFeed))
            {
                logger.LogDebug(Properties.Resources.FeedParser_Logger_EmptyString);
                throw new ArgumentException(Properties.Resources.ErrorMessage_EmptyFeedString, nameof(rssFeed));
            }

            try
            {
                logger.LogDebug(Properties.Resources.FeedParser_Logger_AttemptXmlParsing, rssFeed);
                xmlDoc.LoadXml(rssFeed);
                logger.LogDebug(Properties.Resources.FeedParser_Logger_XmlParsed, xmlDoc);
            }
            catch (XmlException e)
            {
                logger.LogDebug(Properties.Resources.FeedParser_Logger_XmlParseFailed, rssFeed, e);
                throw new FormatException(Properties.Resources.ErrorMessage_InvalidXmlString, e);
            }

            var root = xmlDoc.DocumentElement;
            logger.LogDebug(Properties.Resources.FeedParser_Logger_GetRootElement, root);

            if (!string.Equals(root.Name, "rss", StringComparison.OrdinalIgnoreCase))
            {
                logger.LogDebug(Properties.Resources.FeedParser_Logger_NonRssRootElement, root.Name);
                throw new FormatException(Properties.Resources.ErrorMessage_NonRssRootString);
            }

            if (root.HasAttribute("version") &&
                !string.Equals(root.GetAttribute("version"), "2.0", StringComparison.OrdinalIgnoreCase))
            {
                logger.LogDebug(Properties.Resources.FeedParser_Logger_UnknownRssVersion, root.GetAttribute("version"));
                throw new FormatException(Properties.Resources.ErrorMessage_NonRss2RootString);
            }

            if (root.ChildNodes.Count < 1 || root.FirstChild.Name != "channel")
            {
                logger.LogDebug(Properties.Resources.FeedParser_Logger_MissingChannel);
                throw new FormatException(Properties.Resources.ErrorMessage_MissingChannel);
            }

            logger.LogDebug(Properties.Resources.FeedParser_Logger_BeginParsing);
            var feedElement = root.FirstChild as XmlElement;
            var feed = new Feed();
            var dateFmt = new CultureInfo("en-us").DateTimeFormat;
            var numFmt = new CultureInfo("en-us").NumberFormat;

            foreach (XmlElement child in feedElement.ChildNodes)
            {
                logger.LogDebug(Properties.Resources.FeedParser_Logger_CurrentElement, child);

                switch (child.Name)
                {
                    case "title":
                        feed.Title = child.InnerText;
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Title), feed.Title);
                        break;
                    case "link":
                        feed.Link = child.InnerText;
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Link), feed.Link);
                        break;
                    case "description":
                        feed.Description = child.InnerText;
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Description), feed.Description);
                        break;
                    case "language":
                        feed.Language = child.InnerText;
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Language), feed.Language);
                        break;
                    case "copyright":
                        feed.Copyright = child.InnerText;
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Copyright), feed.Copyright);
                        break;
                    case "managingEditor":
                        feed.ManagingEditor = new FeedPerson() { EmailAddress = child.InnerText };
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.ManagingEditor), feed.ManagingEditor);
                        break;
                    case "webmaster":
                        feed.Webmaster = new FeedPerson() { EmailAddress = child.InnerText };
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Webmaster), feed.Webmaster);
                        break;
                    case "publishDate":
                        if (!string.IsNullOrWhiteSpace(feed.Language))
                        {
                            dateFmt = new CultureInfo(feed.Language).DateTimeFormat;
                        }

                        feed.PublishDate = DateTimeOffset.Parse(child.InnerText, dateFmt);
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.PublishDate), feed.PublishDate);
                        break;
                    case "lastBuildDate":
                        if (!string.IsNullOrWhiteSpace(feed.Language))
                        {
                            dateFmt = new CultureInfo(feed.Language).DateTimeFormat;
                        }

                        feed.LastBuildDate = DateTimeOffset.Parse(child.InnerText, dateFmt);
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.LastBuildDate), feed.LastBuildDate);
                        break;
                    case "category":
                        feed.Categories.Add(child.InnerText);
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_ListItemAdded, child.InnerText, nameof(feed.Categories));
                        break;
                    case "generator":
                        feed.Generator = child.InnerText;
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Generator), feed.Generator);
                        break;
                    case "docs":
                        feed.Docs = child.InnerText;
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Docs), feed.Docs);
                        break;
                    case "cloud":
                        if (!string.IsNullOrWhiteSpace(feed.Language))
                        {
                            numFmt = new CultureInfo(feed.Language).NumberFormat;
                        }

                        feed.Cloud = ParseCloud(child, numFmt);
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Cloud), feed.Cloud);
                        break;
                    case "ttl":
                        if (int.TryParse(child.InnerText, out var ttl))
                        {
                            feed.TimeToLive = ttl;
                            logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.TimeToLive), feed.TimeToLive);
                        }
                        else
                        {
                            logger.LogDebug(
                                Properties.Resources.FeedParser_Logger_TryParseFailed,
                                child.InnerText,
                                typeof(int),
                                nameof(feed.TimeToLive));
                        }

                        break;
                    case "image":
                        feed.Image = ParseImage(child);
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.Image), feed.Image);
                        break;
                    case "textInput":
                        feed.TextInput = ParseTextInput(child);
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(feed.TextInput), feed.TextInput);
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
                                logger.LogDebug(Properties.Resources.FeedParser_Logger_ListItemAdded, child.InnerText, nameof(feed.SkipHours));
                            }
                            catch (FormatException)
                            {
                                logger.LogDebug(
                                    Properties.Resources.FeedParser_Logger_TryParseFailed,
                                    hour.InnerText,
                                    typeof(int),
                                    nameof(feed.SkipHours));
                            }
                            catch (OverflowException)
                            {
                                logger.LogDebug(
                                    Properties.Resources.FeedParser_Logger_TryParseFailed,
                                    hour.InnerText,
                                    typeof(int),
                                    nameof(feed.SkipHours));
                            }
                        }

                        break;
                    case "skipDays":
                        foreach (XmlElement day in child.ChildNodes)
                        {
                            feed.SkipDays.Add(day.InnerText);
                            logger.LogDebug(Properties.Resources.FeedParser_Logger_ListItemAdded, child.InnerText, nameof(feed.SkipDays));
                        }

                        break;
                    case "item":
                        var item = ParseItem(child);

                        feed.Items.Add(item);
                        logger.LogDebug(Properties.Resources.FeedParser_Logger_ListItemAdded, item, nameof(feed.Items));
                        break;
                }
            }

            return feed;
        }

        private FeedItem ParseItem(XmlElement el, string language = "en-us")
        {
            using (logger.BeginScope(el))
            {
                var dateFmt = new CultureInfo(language).DateTimeFormat;
                var numFmt = new CultureInfo(language).NumberFormat;
                var item = new FeedItem();

                foreach (XmlElement child in el.ChildNodes)
                {
                    switch (child.Name)
                    {
                        case "title":
                            item.Title = child.InnerText;
                            logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(item.Title), item.Title);
                            break;
                        case "link":
                            item.Link = child.InnerText;
                            logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(item.Link), item.Link);
                            break;
                        case "description":
                            item.Description = child.InnerText;
                            logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(item.Description), item.Description);
                            break;
                        case "author":
                            item.Author = new FeedPerson() { EmailAddress = child.InnerText };
                            logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(item.Author), item.Author);
                            break;
                        case "category":
                            item.Categories.Add(child.InnerText);
                            break;
                        case "comments":
                            item.Comments = child.InnerText;
                            logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(item.Comments), item.Comments);
                            break;
                        case "enclosure":
                            item.Enclosure = ParseEnclosure(child, numFmt);
                            break;
                        case "itemGuid":
                            item.ItemGuid = child.InnerText;
                            logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(item.ItemGuid), item.ItemGuid);
                            break;
                        case "publishDate":
                            item.PublishDate = DateTimeOffset.Parse(child.InnerText, dateFmt);
                            logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(item.PublishDate), item.PublishDate);
                            break;
                        case "source":
                            if (child.HasAttribute("url"))
                            {
                                item.Source = new FeedItemSource()
                                {
                                    Name = child.InnerText,
                                    Url = child.GetAttribute("url"),
                                };

                                logger.LogDebug(Properties.Resources.FeedParser_Logger_BasicSet, nameof(item.Source), item.Source);
                            }
                            else
                            {
                                logger.LogDebug(Properties.Resources.FeedParser_Logger_SourceUrlMissing, child);
                            }

                            break;
                    }
                }

                return item;
            }
        }

        private FeedItemEnclosure ParseEnclosure(XmlElement el, NumberFormatInfo fmt)
        {
            var enclosure = new FeedItemEnclosure();

            using (logger.BeginScope(el))
            {
                if (el.HasAttribute("url"))
                {
                    enclosure.Url = el.GetAttribute("url");
                    logger.LogDebug(
                        Properties.Resources.FeedParser_Logger_BasicSet,
                        nameof(enclosure.Url),
                        enclosure.Url);
                }

                if (el.HasAttribute("length"))
                {
                    try
                    {
                        enclosure.Length = long.Parse(el.GetAttribute("length"), fmt);
                        logger.LogDebug(
                            Properties.Resources.FeedParser_Logger_BasicSet,
                            nameof(enclosure.Length),
                            enclosure.Length);
                    }
                    catch (FormatException)
                    {
                        logger.LogDebug(
                            Properties.Resources.FeedParser_Logger_TryParseFailed,
                            enclosure.Length,
                            typeof(long),
                            nameof(enclosure.Length));
                    }
                    catch (OverflowException)
                    {
                        logger.LogDebug(
                            Properties.Resources.FeedParser_Logger_TryParseFailed,
                            enclosure.Length,
                            typeof(long),
                            nameof(enclosure.Length));
                    }
                }

                if (el.HasAttribute("mimeType"))
                {
                    enclosure.MimeType = el.GetAttribute("mimeType");
                    logger.LogDebug(
                        Properties.Resources.FeedParser_Logger_BasicSet,
                        nameof(enclosure.MimeType),
                        enclosure.MimeType);
                }
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
