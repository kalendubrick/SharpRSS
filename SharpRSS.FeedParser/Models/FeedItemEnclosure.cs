// <copyright file="FeedItemEnclosure.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Models
{
    using System;

    /// <summary>
    /// Represents an enclosure in a feed item.
    /// </summary>
    public class FeedItemEnclosure
    {
        /// <summary>
        /// Gets or sets the URL where the enclosure is located.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the length of the enclosure, in bytes.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the enclosure.
        /// </summary>
        public string MimeType { get; set; }
    }
}
