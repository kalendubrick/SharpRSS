// <copyright file="FeedImage.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Models
{
    using System;

    /// <summary>
    /// Represents an image in a feed.
    /// </summary>
    public class FeedImage
    {
        #region Required Elements

        /// <summary>
        /// Gets or sets the URL for the image.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the title for the image.
        /// Note: in practice, often the same value as <see cref="Feed.Title"/>.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the link for the image.
        /// Note: in practice, often the same value as <see cref="Feed.Link"/>.
        /// </summary>
        public string Link { get; set; }

        #endregion

        #region Optional Elements

        /// <summary>
        /// Gets or sets the width of the image, in pixels.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the image, in pixels.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the description for the image.
        /// </summary>
        public string Description { get; set; }

        #endregion
    }
}
