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

    }
}
