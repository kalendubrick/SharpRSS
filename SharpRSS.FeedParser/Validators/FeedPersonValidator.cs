// <copyright file="FeedPersonValidator.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Validators
{
    using FluentValidation;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Validator for the <see cref="FeedPerson"/> class.
    /// </summary>
    public class FeedPersonValidator : AbstractValidator<FeedPerson>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedPersonValidator"/> class.
        /// </summary>
        public FeedPersonValidator()
        {
            this.RuleFor(feedPerson => feedPerson.Name)
                .NotEmpty()
                .When(feedPerson => string.IsNullOrWhiteSpace(feedPerson.EmailAddress));
            this.RuleFor(feedPerson => feedPerson.EmailAddress)
                .NotEmpty()
                .When(feedPerson => string.IsNullOrWhiteSpace(feedPerson.Name), ApplyConditionTo.CurrentValidator)
                .EmailAddress()
                .When(feedPerson => !string.IsNullOrWhiteSpace(feedPerson.EmailAddress), ApplyConditionTo.CurrentValidator);
        }
    }
}
