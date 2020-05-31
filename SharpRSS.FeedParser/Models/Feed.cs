// <copyright file="Feed.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an RSS feed.
    /// </summary>
    public class Feed
    {
        #region Required Elements

        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL to the corresponding website.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the phrase or description describing the channel.
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Optional Elements

        /// <summary>
        /// Gets or sets the language the channel is written in.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the copyright notice for content in the channel.
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FeedPerson"/>
        /// responsible for editorial content.
        /// </summary>
        public FeedPerson ManagingEditor { get; set; }

        /// <summary>
        /// Gets or sets the email address for the person
        /// responsible for technical issues relating to
        /// the channel.
        /// </summary>
        public FeedPerson Webmaster { get; set; }

        /// <summary>
        /// Gets or sets the publication date for content
        /// in the channel.
        /// </summary>
        public DateTimeOffset? PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the last time the content of the
        /// channel changed.
        /// </summary>
        public DateTimeOffset? LastBuildDate { get; set; }

        /// <summary>
        /// Gets the category/categories the channel belongs to.
        /// </summary>
        public IList<string> Categories { get; } = new List<string>();

        /// <summary>
        /// Gets or sets a string indicating the program
        /// used to generate the channel.
        /// </summary>
        public string Generator { get; set; }

        /// <summary>
        /// Gets or sets a URL that points to the documentation
        /// for the format used in the RSS file.
        /// </summary>
        public string Docs { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FeedCloud"/>
        /// registered to be notified of updates to the
        /// channel.
        /// </summary>
        public FeedCloud Cloud { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes that indicates
        /// how long a channel can be cached before refreshing from
        /// source.
        /// </summary>
        public int? TimeToLive { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FeedImage"/> to be
        /// displayed in the channel.
        /// </summary>
        public FeedImage Image { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FeedTextInput"/> that
        /// can be displayed with the channel.
        /// </summary>
        public FeedTextInput TextInput { get; set; }

        /// <summary>
        /// Gets the hours (0-23) when aggregators
        /// may not read the channel.
        /// </summary>
        public IList<int> SkipHours { get; } = new List<int>();

        /// <summary>
        /// Gets the days (Monday-Sunday) when aggregators
        /// may not read the channel.
        /// </summary>
        public IList<string> SkipDays { get; } = new List<string>();

        /// <summary>
        /// Gets the items in the feed.
        /// </summary>
        public IList<FeedItem> Items { get; } = new List<FeedItem>();

        #endregion
    }
}
