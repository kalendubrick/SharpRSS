// <copyright file="FeedItem.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an item in a feed.
    /// </summary>
    public class FeedItem
    {
        /// <summary>
        /// Gets or sets the title of the item.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL of the item.
        /// </summary>
        public Uri Link { get; set; }

        /// <summary>
        /// Gets or sets the item synopsis.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the item author.
        /// </summary>
        public FeedPerson Author { get; set; }

        /// <summary>
        /// Gets the categories the item is included in.
        /// </summary>
        public IList<string> Categories { get; }
    }
}
