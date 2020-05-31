namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;

    class FeedValidatorTests
    {
        private static readonly Feed[] validFeedCases =
        {
            new Feed()
            {
                Title = "GoUpstate.com News Headlines",
                Link = "http://www.goupstate.com/",
                Description = "The latest news from GoUpstate.com, a Spartanburg Herald-Journal Web site."
            },
            new Feed()
            {
                Title = "GoUpstate.com News Headlines",
                Link = "http://www.goupstate.com/",
                Description = "The latest news from GoUpstate.com, a Spartanburg Herald-Journal Web site.",
                Cloud = new FeedCloud()
                {
                    Domain = "rpc.sys.com",
                    Port = 80,
                    Path = "/RPC2",
                    RegisterProcedure = "myCloud.rssPleaseNotify",
                    Protocol = "xml-rpc"
                },
                Image = new FeedImage()
                {
                    Url = "https://via.placeholder.com/150",
                    Title = "Liftoff News",
                    Link = "http://liftoff.msfc.nasa.gov/",
                    Width = 144,
                    Height = 400,
                    Description = "A placeholder image"
                },
                ManagingEditor = new FeedPerson()
                {
                    Name = "Kalen Dubrick",
                    EmailAddress = "kalen@kalendubrick.com"
                },
                Webmaster = new FeedPerson()
                {
                    Name = "Kalen Dubrick",
                    EmailAddress = "kalen@kalendubrick.com"
                }
            }
        };
        private FeedValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedValidator();
        }

        [Test]
        [TestCaseSource("validFeedCases")]
        public void ValidFeedShouldNotReturnValidatorErrors(Feed feed)
        {
            feed.Categories.Add("Newspapers");
            feed.SkipHours.Add(3);
            feed.SkipHours.Add(10);
            feed.SkipHours.Add(22);
            feed.SkipDays.Add("Monday");
            feed.SkipDays.Add("Friday");
            feed.Items.Add(new FeedItem()
            {
                Title = "Venice Film Festival Tries to Quit Sinking",
                Link = "http://nytimes.com/2004/12/07FEST.html",
                Description = @"Some of the most heated chatter at the Venice Film Festival
                                this week was about the way that the arrival of the stars at
                                the Palazzo del Cinema was being staged."
            });
            feed.Items.Add(new FeedItem()
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
            });

            var result = validator.TestValidate(feed);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void NullFeedTitleShouldReturnValidatorError()
        {
            var feed = new Feed()
            {
                Title = null,
                Link = "http://www.goupstate.com/",
                Description = "The latest news from GoUpstate.com, a Spartanburg Herald-Journal Web site."
            };

            var result = validator.TestValidate(feed);

            result.ShouldHaveValidationErrorFor(feed => feed.Title);
        }

        [Test]
        public void EmptyFeedTitleShouldReturnValidatorError()
        {
            var feed = new Feed()
            {
                Title = "",
                Link = "http://www.goupstate.com/",
                Description = "The latest news from GoUpstate.com, a Spartanburg Herald-Journal Web site."
            };

            var result = validator.TestValidate(feed);

            result.ShouldHaveValidationErrorFor(feed => feed.Title);
        }

        [Test]
        public void NullFeedLinkShouldReturnValidatorError()
        {
            var feed = new Feed()
            {
                Title = "GoUpstate.com News Headlines",
                Link = null,
                Description = "The latest news from GoUpstate.com, a Spartanburg Herald-Journal Web site."
            };

            var result = validator.TestValidate(feed);

            result.ShouldHaveValidationErrorFor(feed => feed.Link);
        }

        [Test]
        public void InvalidFeedLinkShouldReturnValidatorError()
        {
            var feed = new Feed()
            {
                Title = "GoUpstate.com News Headlines",
                Link = "goupstate/",
                Description = "The latest news from GoUpstate.com, a Spartanburg Herald-Journal Web site."
            };

            var result = validator.TestValidate(feed);

            result.ShouldHaveValidationErrorFor(feed => feed.Link);
        }

        [Test]
        public void NullFeedDescriptionShouldReturnValidatorError()
        {
            var feed = new Feed()
            {
                Title = "GoUpstate.com News Headlines",
                Link = "http://www.goupstate.com/",
                Description = null
            };

            var result = validator.TestValidate(feed);

            result.ShouldHaveValidationErrorFor(feed => feed.Description);
        }

        [Test]
        public void EmptyFeedDescriptionShouldReturnValidatorError()
        {
            var feed = new Feed()
            {
                Title = "GoUpstate.com News Headlines",
                Link = "http://www.goupstate.com/",
                Description = ""
            };

            var result = validator.TestValidate(feed);

            result.ShouldHaveValidationErrorFor(feed => feed.Description);
        }
    }
}
