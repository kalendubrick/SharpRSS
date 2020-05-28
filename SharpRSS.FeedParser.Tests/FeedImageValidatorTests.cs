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

        [Test]
        public void EmptyFeedImageUrlShouldReturnValidatorError()
        {
            var feedImage = new FeedImage()
            {
                Url = "",
                Title = "Liftoff News",
                Link = "http://liftoff.msfc.nasa.gov/"
            };

            var result = validator.TestValidate(feedImage);

            result.ShouldHaveValidationErrorFor(feedImage => feedImage.Url);
        }

        [Test]
        public void InvalidFeedImageUrlShouldReturnValidatorError()
        {
            var feedImage = new FeedImage()
            {
                Url = "liftoff/",
                Title = "Liftoff News",
                Link = "http://liftoff.msfc.nasa.gov/"
            };

            var result = validator.TestValidate(feedImage);

            result.ShouldHaveValidationErrorFor(feedImage => feedImage.Url);
        }

        [Test]
        public void EmptyFeedImageTitleShouldReturnValidatorError()
        {
            var feedImage = new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "",
                Link = "http://liftoff.msfc.nasa.gov/"
            };

            var result = validator.TestValidate(feedImage);

            result.ShouldHaveValidationErrorFor(feedImage => feedImage.Title);
        }

        [Test]
        public void EmptyFeedImageLinkShouldReturnValidatorError()
        {
            var feedImage = new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "Liftoff News",
                Link = ""
            };

            var result = validator.TestValidate(feedImage);

            result.ShouldHaveValidationErrorFor(feedImage => feedImage.Link);
        }

        [Test]
        public void InvalidFeedImageLinkShouldReturnValidatorError()
        {
            var feedImage = new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "Liftoff News",
                Link = "nasagov/"
            };

            var result = validator.TestValidate(feedImage);

            result.ShouldHaveValidationErrorFor(feedImage => feedImage.Link);
        }

        [Test]
        public void NullFeedImageWidthAndNonNullHeightShouldReturnValidatorError()
        {
            var feedImage = new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "Liftoff News",
                Link = "nasagov/",
                Width = null,
                Height = 300
            };

            var result = validator.TestValidate(feedImage);

            result.ShouldHaveValidationErrorFor(feedImage => feedImage.Width);
        }

        [Test]
        public void NullFeedImageHeightAndNonNullWidthShouldReturnValidatorError()
        {
            var feedImage = new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "Liftoff News",
                Link = "nasagov/",
                Width = 100,
                Height = null
            };

            var result = validator.TestValidate(feedImage);

            result.ShouldHaveValidationErrorFor(feedImage => feedImage.Height);
        }

        [Test]
        public void FeedImageWidthGreaterThanMaxShouldReturnValidatorError()
        {
            var feedImage = new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "Liftoff News",
                Link = "nasagov/",
                Width = 1000,
                Height = 300
            };

            var result = validator.TestValidate(feedImage);

            result.ShouldHaveValidationErrorFor(feedImage => feedImage.Width);
        }

        [Test]
        public void FeedImageHeightGreaterThanMaxShouldReturnValidatorError()
        {
            var feedImage = new FeedImage()
            {
                Url = "https://via.placeholder.com/150",
                Title = "Liftoff News",
                Link = "nasagov/",
                Width = 100,
                Height = 3000
            };

            var result = validator.TestValidate(feedImage);

            result.ShouldHaveValidationErrorFor(feedImage => feedImage.Height);
        }
    }
}
