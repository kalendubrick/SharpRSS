namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;

    class FeedItemValidatorTests
    {
        private static readonly FeedItem[] validFeedItems =
        {
            new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged."
            },
            new FeedItem()
            {
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged."
            },
            new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
            },
            new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
            },
            new FeedItem()
            {
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged."
            },
            new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged.",
                Author = new FeedPerson()
                {
                    Name = "Kalen Dubrick",
                    EmailAddress = "kalen@kalendubrick.com"
                },
                Comments = "http://ekzemplo.com/entry/4403/comments",
                Source = new FeedItemSource()
                {
                    Name = "Tomalak's Realm",
                    Url = "http://www.tomalak.org/links2.xml"
                }
            }
        };
        private FeedItemValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedItemValidator();
        }

        [Test]
        [TestCaseSource("validFeedItems")]
        public void ValidFeedItemShouldNotReturnValidatorErrors(FeedItem feedItem)
        {
            var result = validator.TestValidate(feedItem);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void EmptyTitleAndDescriptionShouldReturnValidatorError()
        {
            var feedItem = new FeedItem()
            {
                Link = "http://nytimes.com/2004/12/07FEST.html"
            };

            var result = validator.TestValidate(feedItem);

            result.ShouldHaveAnyValidationError();
        }

        [Test]
        public void InvalidLinkShouldReturnValidatorError()
        {
            var feedItem = new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "nytimes/"
            };

            var result = validator.TestValidate(feedItem);

            result.ShouldHaveValidationErrorFor(feedItem => feedItem.Link);
        }

        [Test]
        public void EmptyCategoryShouldReturnValidatorError()
        {
            var feedItem = new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html"
            };
            feedItem.Categories.Add("");

            var result = validator.TestValidate(feedItem);

            result.ShouldHaveValidationErrorFor(feedItem => feedItem.Categories);
        }

        [Test]
        public void InvalidCommentsShouldReturnValidatorError()
        {
            var feedItem = new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Comments = "ekzemplo/"
            };

            var result = validator.TestValidate(feedItem);

            result.ShouldHaveValidationErrorFor(feedItem => feedItem.Comments);
        }

        [Test]
        public void FeedItemWithAuthorShouldHaveFeedPersonValidator()
        {
            var feedItem = new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged.",
                Author = new FeedPerson()
                {
                    Name = "Kalen Dubrick",
                    EmailAddress = "kalen@kalendubrick.com"
                }
            };

            var _ = validator.TestValidate(feedItem);

            validator.ShouldHaveChildValidator(feedItem => feedItem.Author, typeof(FeedPersonValidator));
        }

        [Test]
        public void FeedItemWithInvalidAuthorEmailAddressShouldReturnValidatorError()
        {
            var feedItem = new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged.",
                Author = new FeedPerson()
                {
                    Name = "Kalen Dubrick",
                    EmailAddress = "kalen"
                }
            };

            var result = validator.TestValidate(feedItem);

            result.ShouldHaveValidationErrorFor(feedItem => feedItem.Author.EmailAddress);
        }

        [Test]
        public void FeedItemWithSourceShouldHaveFeedItemSourceValidator()
        {
            var feedItem = new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged.",
                Source = new FeedItemSource()
                {
                    Name = "Tomalak's Realm",
                    Url = "http://www.tomalak.org/links2.xml"
                }
            };

            var _ = validator.TestValidate(feedItem);

            validator.ShouldHaveChildValidator(feedItem => feedItem.Source, typeof(FeedItemSourceValidator));
        }

        [Test]
        public void FeedItemWithEnclosureShouldHaveFeedItemEnclosureValidator()
        {
            var feedItem = new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged.",
                Enclosure = new FeedItemEnclosure()
                {
                    Url = "http://www.scripting.com/mp3s/weatherReportSuite.mp3",
                    Length = 12216320,
                    MimeType = "audio/mpeg"
                }
            };

            var _ = validator.TestValidate(feedItem);

            validator.ShouldHaveChildValidator(feedItem => feedItem.Enclosure, typeof(FeedItemEnclosureValidator));
        }

        [Test]
        public void FeedItemWithEmptySourceUrlShouldReturnValidatorError()
        {
            var feedItem = new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged.",
                Source = new FeedItemSource()
                {
                    Name = "Tomalak's Realm",
                    Url = ""
                }
            };

            var result = validator.TestValidate(feedItem);

            result.ShouldHaveValidationErrorFor(feedItem => feedItem.Source.Url);
        }

        [Test]
        public void FeedItemWithInvalidSourceUrlShouldReturnValidatorError()
        {
            var feedItem = new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged.",
                Source = new FeedItemSource()
                {
                    Name = "Tomalak's Realm",
                    Url = "tomalak/"
                }
            };

            var result = validator.TestValidate(feedItem);

            result.ShouldHaveValidationErrorFor(feedItem => feedItem.Source.Url);
        }
    }
}
