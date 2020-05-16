// <copyright file="FeedCloud.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Models
{
    /// <summary>
    /// Represents the cloud element of an RSS 2.0 feed.
    /// </summary>
    public class FeedCloud
    {
        /// <summary>
        /// Gets or sets the URL or IP address of the cloud.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the TCP port the cloud is running on.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the location of the responder.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name of the procedure to call
        /// to request notification.
        /// </summary>
        public string RegisterProcedure { get; set; }

        /// <summary>
        /// Gets or sets the protocol to be used.
        /// </summary>
        public string Protocol { get; set; }
    }
}
