﻿// <copyright file="FeedItemSource.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Models
{
    using System;

    /// <summary>
    /// Represents a source for a feed item.
    /// </summary>
    public class FeedItemSource
    {
        /// <summary>
        /// Gets or sets the name of the source.
        /// Should be derived from <see cref="Feed.Title"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL that links to the
        /// XMLization of the source.
        /// </summary>
        public string Url { get; set; }
    }
}
