// <copyright file="FeedItemValidator.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Validators
{
    using System;

    using FluentValidation;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Validator for the <see cref="FeedItem"/> class.
    /// </summary>
    public class FeedItemValidator : AbstractValidator<FeedItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedItemValidator"/> class.
        /// </summary>
        public FeedItemValidator()
        {
            // FeedItem-specific field validators
            this.RuleFor(feed => feed.Title)
                .NotEmpty()
                .When(feed => string.IsNullOrEmpty(feed.Description));
            this.RuleFor(feed => feed.Description)
                .NotEmpty()
                .When(feed => string.IsNullOrEmpty(feed.Title));
            this.RuleFor(feedItem => feedItem.Link)
                .NotNull()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(feedItem => !string.IsNullOrWhiteSpace(feedItem.Link), ApplyConditionTo.CurrentValidator);
            this.RuleForEach(feedItem => feedItem.Categories).NotEmpty();
            this.RuleFor(feedItem => feedItem.Comments)
                .NotNull()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(feedItem => !string.IsNullOrWhiteSpace(feedItem.Comments), ApplyConditionTo.CurrentValidator);

            // Validators from sub-elements
            this.RuleFor(feedItem => feedItem.Author).SetValidator(new FeedPersonValidator());
            this.RuleFor(feedItem => feedItem.Source).SetValidator(new FeedItemSourceValidator());
        }
    }
}
