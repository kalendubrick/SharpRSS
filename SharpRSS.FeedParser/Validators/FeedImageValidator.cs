// <copyright file="FeedImageValidator.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Validators
{
    using System;

    using FluentValidation;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Validator for the <see cref="FeedImage"/> class.
    /// </summary>
    public class FeedImageValidator : AbstractValidator<FeedImage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedImageValidator"/> class.
        /// </summary>
        public FeedImageValidator()
        {
            this.RuleFor(feedImage => feedImage.Url)
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(feedImage => !string.IsNullOrWhiteSpace(feedImage.Url));
            this.RuleFor(feedImage => feedImage.Title).NotEmpty();
            this.RuleFor(feedImage => feedImage.Link)
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(feedImage => !string.IsNullOrWhiteSpace(feedImage.Link));
            this.RuleFor(feedImage => feedImage.Height)
                .LessThanOrEqualTo(400)
                .When(feedImage => feedImage.Height.HasValue);
            this.RuleFor(feedImage => feedImage.Width)
                .LessThanOrEqualTo(144)
                .When(feedImage => feedImage.Width.HasValue);
        }
    }
}
