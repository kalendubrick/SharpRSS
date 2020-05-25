// <copyright file="FeedValidator.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Validators
{
    using System;

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
            // Feed-specific field validators
            this.RuleFor(feed => feed.Title).NotEmpty();
            this.RuleFor(feed => feed.Link)
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(feed => !string.IsNullOrWhiteSpace(feed.Link));
            this.RuleFor(feed => feed.Description).NotEmpty();

            // Validators from sub-elements
            this.RuleFor(feed => feed.Cloud).SetValidator(new FeedCloudValidator());
            this.RuleFor(feed => feed.Image).SetValidator(new FeedImageValidator());
            this.RuleFor(feed => feed.ManagingEditor).SetValidator(new FeedPersonValidator());
            this.RuleFor(feed => feed.TextInput).SetValidator(new FeedTextInputValidator());
        }
    }
}
