// <copyright file="FeedPerson.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Models
{
    /// <summary>
    /// Represents a person for the use in several
    /// elements such as managing editor, item author, etc.
    /// </summary>
    public class FeedPerson
    {
        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the person.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Generates a single line string with name and email address.
        /// </summary>
        /// <returns>A string representing the person.</returns>
        public override string ToString()
        {
            return $"{this.EmailAddress} ({this.Name})";
        }
    }
}
