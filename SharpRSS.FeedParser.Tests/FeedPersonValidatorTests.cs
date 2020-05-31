namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;

    class FeedPersonValidatorTests
    {
        private FeedPersonValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedPersonValidator();
        }

        [Test]
        public void ValidFeedPersonShouldNotReturnValidatorErrors()
        {
            var feedPerson = new FeedPerson()
            {
                Name = "Kalen Dubrick",
                EmailAddress = "kalen@kalendubrick.com"
            };

            var result = validator.TestValidate(feedPerson);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void NullFeedPersonNameShouldReturnValidatorError()
        {
            var feedPerson = new FeedPerson()
            {
                Name = null,
                EmailAddress = "kalen@kalendubrick.com"
            };

            var result = validator.TestValidate(feedPerson);

            result.ShouldHaveValidationErrorFor(feedPerson => feedPerson.Name);
        }

        [Test]
        public void EmptyFeedPersonNameShoudReturnValidatorError()
        {
            var feedPerson = new FeedPerson()
            {
                Name = "",
                EmailAddress = "kalen@kalendubrick.com"
            };

            var result = validator.TestValidate(feedPerson);

            result.ShouldHaveValidationErrorFor(feedPerson => feedPerson.Name);
        }

        [Test]
        public void NullFeedPersonEmailAddressShoudReturnValidatorError()
        {
            var feedPerson = new FeedPerson()
            {
                Name = "Kalen Dubrick",
                EmailAddress = null
            };

            var result = validator.TestValidate(feedPerson);

            result.ShouldHaveValidationErrorFor(feedPerson => feedPerson.EmailAddress);
        }

        [Test]
        public void InvalidFeedPersonEmailAddressShoudReturnValidatorError()
        {
            var feedPerson = new FeedPerson()
            {
                Name = "Kalen Dubrick",
                EmailAddress = "kalenkalendubrick.com"
            };

            var result = validator.TestValidate(feedPerson);

            result.ShouldHaveValidationErrorFor(feedPerson => feedPerson.EmailAddress);
        }
    }
}
