// <copyright file="FeedCloudValidator.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Validators
{
    using System;

    using FluentValidation;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Validator for the <see cref="FeedCloud"/> class.
    /// </summary>
    public class FeedCloudValidator : AbstractValidator<FeedCloud>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedCloudValidator"/> class.
        /// </summary>
        public FeedCloudValidator()
        {
            this.RuleFor(feedCloud => feedCloud.Domain)
                .NotEmpty()
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute))
                .When(feedCloud => !string.IsNullOrWhiteSpace(feedCloud.Domain), ApplyConditionTo.CurrentValidator);
            this.RuleFor(feedCloud => feedCloud.Port).InclusiveBetween(0, 65535);
            this.RuleFor(feedCloud => feedCloud.Path).NotEmpty();
            this.RuleFor(feedCloud => feedCloud.RegisterProcedure).NotEmpty();
            this.RuleFor(feedCloud => feedCloud.Protocol).NotEmpty();
        }
    }
}
