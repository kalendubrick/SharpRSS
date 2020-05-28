namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;

    class FeedItemSourceValidatorTests
    {
        private FeedItemSourceValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedItemSourceValidator();
        }

        [Test]
        public void ValidFeedItemSourceShouldNotReturnValidatorErrors()
        {
            var feedItemSource = new FeedItemSource()
            {
                Name = "Tomalak's Realm",
                Url = "http://www.tomalak.org/links2.xml"
            };

            var result = validator.TestValidate(feedItemSource);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void EmptyFeedItemSourceUrlShoudReturnValidatorError()
        {
            var feedItemSource = new FeedItemSource()
            {
                Name = "Tomalak's Realm",
                Url = ""
            };

            var result = validator.TestValidate(feedItemSource);

            result.ShouldHaveValidationErrorFor(feedItemSource => feedItemSource.Url);
        }

        [Test]
        public void InvalidFeedItemSourceUrlShoudReturnValidatorError()
        {
            var feedItemSource = new FeedItemSource()
            {
                Name = "Tomalak's Realm",
                Url = "tomalak/"
            };

            var result = validator.TestValidate(feedItemSource);

            result.ShouldHaveValidationErrorFor(feedItemSource => feedItemSource.Url);
        }
    }
}
