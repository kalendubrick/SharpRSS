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
    }
}
