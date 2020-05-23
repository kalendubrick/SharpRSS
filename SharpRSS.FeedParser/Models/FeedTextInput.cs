// <copyright file="FeedTextInput.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Models
{
    using System;

    /// <summary>
    /// Represents a text input elemnent for channel.
    /// </summary>
    public class FeedTextInput
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the text object
        /// in the text input area.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL of the CGI script that
        /// processes text input requests.
        /// </summary>
        public string Link { get; set; }
    }
}
