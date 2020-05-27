namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;

    class FeedImageValidatorTests
    {
        private static readonly FeedImage[] validFeedImages =
        {
            new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "Liftoff News",
                Link = "http://liftoff.msfc.nasa.gov/"
            },
            new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "Liftoff News",
                Link = "http://liftoff.msfc.nasa.gov/",
                Width = 100,
                Height = 300
            },
            new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "Liftoff News",
                Link = "http://liftoff.msfc.nasa.gov/",
                Width = 144,
                Height = 400,
                Description = "A placeholder image"
            }
        };

        private FeedImageValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedImageValidator();
        }

        [Test]
        [TestCaseSource("validFeedImages")]
        public void ValidFeedImageShouldNotReturnValidatorErrors(FeedImage feedImage)
        {
            var result = validator.TestValidate(feedImage);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
