namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;

    class FeedTextInputValidatorTests
    {
        private FeedTextInputValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedTextInputValidator();
        }

        [Test]
        public void ValidFeedTextInputShouldNotReturnValidatorErrors()
        {
            var feedTextInput = new FeedTextInput()
            {
                Title = "Submit",
                Description = "A text input box",
                Name = "txtInput",
                Link = "http://search.yahoo.com/search?"
            };

            var result = validator.TestValidate(feedTextInput);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void NullFeedTextInputTitleShouldReturnValidatorError()
        {
            var feedTextInput = new FeedTextInput()
            {
                Title = null,
                Description = "A text input box",
                Name = "txtInput",
                Link = "http://search.yahoo.com/search?"
            };

            var result = validator.TestValidate(feedTextInput);

            result.ShouldHaveValidationErrorFor(feedTextInput => feedTextInput.Title);
        }

        [Test]
        public void EmptyFeedTextInputTitleShoudReturnValidatorError()
        {
            var feedTextInput = new FeedTextInput()
            {
                Title = "",
                Description = "A text input box",
                Name = "txtInput",
                Link = "http://search.yahoo.com/search?"
            };

            var result = validator.TestValidate(feedTextInput);

            result.ShouldHaveValidationErrorFor(feedTextInput => feedTextInput.Title);
        }

        [Test]
        public void NullFeedTextInputDescriptionShouldReturnValidatorError()
        {
            var feedTextInput = new FeedTextInput()
            {
                Title = "Submit",
                Description = null,
                Name = "txtInput",
                Link = "http://search.yahoo.com/search?"
            };

            var result = validator.TestValidate(feedTextInput);

            result.ShouldHaveValidationErrorFor(feedTextInput => feedTextInput.Description);
        }

        [Test]
        public void EmptyFeedTextInputDescriptionShoudReturnValidatorError()
        {
            var feedTextInput = new FeedTextInput()
            {
                Title = "Submit",
                Description = "",
                Name = "txtInput",
                Link = "http://search.yahoo.com/search?"
            };

            var result = validator.TestValidate(feedTextInput);

            result.ShouldHaveValidationErrorFor(feedTextInput => feedTextInput.Description);
        }

        [Test]
        public void NullFeedTextInputNameShouldReturnValidatorError()
        {
            var feedTextInput = new FeedTextInput()
            {
                Title = "Submit",
                Description = "A text input box",
                Name = null,
                Link = "http://search.yahoo.com/search?"
            };

            var result = validator.TestValidate(feedTextInput);

            result.ShouldHaveValidationErrorFor(feedTextInput => feedTextInput.Name);
        }

        [Test]
        public void EmptyFeedTextInputNameShoudReturnValidatorError()
        {
            var feedTextInput = new FeedTextInput()
            {
                Title = "Submit",
                Description = "A text input box",
                Name = "",
                Link = "http://search.yahoo.com/search?"
            };

            var result = validator.TestValidate(feedTextInput);

            result.ShouldHaveValidationErrorFor(feedTextInput => feedTextInput.Name);
        }

        [Test]
        public void NullFeedTextInputLinkShouldReturnValidatorError()
        {
            var feedTextInput = new FeedTextInput()
            {
                Title = "Submit",
                Description = "A text input box",
                Name = "txtInput",
                Link = null
            };

            var result = validator.TestValidate(feedTextInput);

            result.ShouldHaveValidationErrorFor(feedTextInput => feedTextInput.Link);
        }

        [Test]
        public void InvalidFeedTextInputLinkShouldReturnValidatorError()
        {
            var feedTextInput = new FeedTextInput()
            {
                Title = "Submit",
                Description = "A text input box",
                Name = "txtInput",
                Link = "yahoo"
            };

            var result = validator.TestValidate(feedTextInput);

            result.ShouldHaveValidationErrorFor(feedTextInput => feedTextInput.Link);
        }
    }
}
