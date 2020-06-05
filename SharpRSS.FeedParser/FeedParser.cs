// <copyright file="FeedParser.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser
{
    using System;
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
            catch (XmlException)
            {
                throw new ArgumentException(Properties.Resources.ErrorMessage_InvalidXmlString, nameof(rssFeed));
            }

            var root = xmlDoc.DocumentElement;

            if (!string.Equals(root.Name, "rss", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(Properties.Resources.ErrorMessage_NonRssRootString, nameof(rssFeed));
            }

            if (root.HasAttribute("version") &&
                !string.Equals(root.GetAttribute("version"), "2.0", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(Properties.Resources.ErrorMessage_NonRss2RootString, nameof(rssFeed));
            }

            if (root.ChildNodes.Count < 1 || root.FirstChild.Name != "channel")
            {
                throw new ArgumentException(Properties.Resources.ErrorMessage_MissingChannel, nameof(rssFeed));
            }

            var feedElement = root.FirstChild as XmlElement;
            var feed = new Feed();

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
                }
            }

            return feed;
        }
    }
}
