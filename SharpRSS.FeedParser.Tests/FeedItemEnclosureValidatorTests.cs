namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;

    class FeedItemEnclosureValidatorTests
    {
        private FeedItemEnclosureValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedItemEnclosureValidator();
        }

        [Test]
        public void ValidFeedItemEnclosureShouldNotReturnValidatorErrors()
        {
            var enclosure = new FeedItemEnclosure()
            {
                Url = "http://www.scripting.com/mp3s/weatherReportSuite.mp3",
                Length = 12216320,
                MimeType = "audio/mpeg"
            };

            var result = validator.TestValidate(enclosure);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void InvalidFeedItemEnclosureUrlShouldReturnValidatorError()
        {
            var enclosure = new FeedItemEnclosure()
            {
                Url = "http:/.mp3",
                Length = 12216320,
                MimeType = "audio/mpeg"
            };

            var result = validator.TestValidate(enclosure);

            result.ShouldHaveValidationErrorFor(enclosure => enclosure.Url);
        }

        [Test]
        public void InvalidFeedItemEnclosureLengthShouldReturnValidatorError()
        {
            var enclosure = new FeedItemEnclosure()
            {
                Url = "http://www.scripting.com/mp3s/weatherReportSuite.mp3",
                Length = 0,
                MimeType = "audio/mpeg"
            };

            var result = validator.TestValidate(enclosure);

            result.ShouldHaveValidationErrorFor(enclosure => enclosure.Length);
        }

        [Test]
        public void InvalidFeedItemEnclosureMimeTypeShouldReturnValidatorError()
        {
            var enclosure = new FeedItemEnclosure()
            {
                Url = "http://www.scripting.com/mp3s/weatherReportSuite.mp3",
                Length = 12216320,
                MimeType = null
            };

            var result = validator.TestValidate(enclosure);

            result.ShouldHaveValidationErrorFor(enclosure => enclosure.MimeType);
        }
    }
}
