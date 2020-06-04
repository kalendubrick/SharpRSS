// <copyright file="FeedParser.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser
{
    using System;
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
            if (rssFeed is null)
            {
                throw new ArgumentNullException(nameof(rssFeed));
            }

            return new Feed();
        }
    }
}
