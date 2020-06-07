namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;

    class FeedPersonValidatorTests
    {
        private static readonly FeedPerson[] validFeedPeople =
        {
            new FeedPerson()
            {
                Name = "Kalen Dubrick",
                EmailAddress = "kalen@kalendubrick.com"
            },
            new FeedPerson()
            {
                Name = null,
                EmailAddress = "kalen@kalendubrick.com"
            },
            new FeedPerson()
            {
                Name = "Kalen Dubrick",
                EmailAddress = null
            }
        };
        private FeedPersonValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedPersonValidator();
        }

        [Test]
        [TestCaseSource("validFeedPeople")]
        public void ValidFeedPersonShouldNotReturnValidatorErrors(FeedPerson feedPerson)
        {
            var result = validator.TestValidate(feedPerson);

            result.ShouldNotHaveAnyValidationErrors();
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

        [Test]
        public void NullFeedPersonNameAndEmailAddressShouldReturnValidatorErrors()
        {
            var feedPerson = new FeedPerson()
            {
                Name = null,
                EmailAddress = null
            };

            var result = validator.TestValidate(feedPerson);

            result.ShouldHaveValidationErrorFor(feedPerson => feedPerson.Name);
            result.ShouldHaveValidationErrorFor(feedPerson => feedPerson.EmailAddress);
        }
    }
}
