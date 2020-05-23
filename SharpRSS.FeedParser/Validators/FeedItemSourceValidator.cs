// <copyright file="FeedItemSourceValidator.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Validators
{
    using System;

    using FluentValidation;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Validator for the <see cref="FeedItemSource"/> class.
    /// </summary>
    public class FeedItemSourceValidator : AbstractValidator<FeedItemSource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedItemSourceValidator"/> class.
        /// </summary>
        public FeedItemSourceValidator()
        {
            this.RuleFor(feedItemSource => feedItemSource.Url)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(feedItemSource => !string.IsNullOrWhiteSpace(feedItemSource.Url));
        }
    }
}
