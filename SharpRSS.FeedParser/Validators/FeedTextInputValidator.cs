// <copyright file="FeedTextInputValidator.cs" company="Kalen Dubrick">
// Copyright (c) Kalen Dubrick. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SharpRSS.FeedParser.Validators
{
    using FluentValidation;
    using SharpRSS.FeedParser.Models;

    /// <summary>
    /// Validator for the <see cref="FeedTextInput"/> class.
    /// </summary>
    public class FeedTextInputValidator : AbstractValidator<FeedTextInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedTextInputValidator"/> class.
        /// </summary>
        public FeedTextInputValidator()
        {
            this.RuleFor(feedTextInput => feedTextInput.Title).NotEmpty();
            this.RuleFor(feedTextInput => feedTextInput.Description).NotEmpty();
            this.RuleFor(feedTextInput => feedTextInput.Name).NotEmpty();
            this.RuleFor(feedTextInput => feedTextInput.Link).NotNull();
        }
    }
}
