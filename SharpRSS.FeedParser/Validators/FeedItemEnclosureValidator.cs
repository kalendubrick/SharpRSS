// <copyright file="FeedItemEnclosureValidator.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Validators
{
    using System;

    using FluentValidation;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Validator for the <see cref="FeedItemEnclosure"/> class.
    /// </summary>
    public class FeedItemEnclosureValidator : AbstractValidator<FeedItemEnclosure>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedItemEnclosureValidator"/> class.
        /// </summary>
        public FeedItemEnclosureValidator()
        {
            this.RuleFor(feedItemEnclosure => feedItemEnclosure.Url)
                .NotEmpty()
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(feedItemEnclosure => !string.IsNullOrWhiteSpace(feedItemEnclosure.Url), ApplyConditionTo.CurrentValidator);
            this.RuleFor(feedItemEnclousure => feedItemEnclousure.Length).NotEmpty();
            this.RuleFor(feedItemEnclosure => feedItemEnclosure.MimeType).NotEmpty();
        }
    }
}
