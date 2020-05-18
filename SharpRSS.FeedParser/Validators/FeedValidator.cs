// <copyright file="FeedValidator.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Validators
{
    using FluentValidation;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Validator for the <see cref="Feed"/> class.
    /// </summary>
    public class FeedValidator : AbstractValidator<Feed>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedValidator"/> class.
        /// </summary>
        public FeedValidator()
        {
            this.RuleFor(feed => feed.Title).NotEmpty();
            this.RuleFor(feed => feed.Link).NotNull();
            this.RuleFor(feed => feed.Description).NotEmpty();
        }
    }
}
