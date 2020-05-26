namespace SharpRSS.FeedParser.Tests
{
    using System.Threading.Tasks;

    using FluentValidation;
    using FluentValidation.Results;
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;
    
    public class FeedCloudValidatorTests
    {
        private static FeedCloud[] validFeedCloudCases = 
        {
            new FeedCloud()
            {
                Domain = "rpc.sys.com",
                Port = 80,
                Path = "/RPC2",
                RegisterProcedure = "myCloud.rssPleaseNotify",
                Protocol = "xml-rpc"
            }
        };
        private static FeedCloud[] invalidFeedCloudCases =
        {
            new FeedCloud()
            {
                Domain = "",
                Port = 80,
                Path = "/RPC2",
                RegisterProcedure = "myCloud.rssPleaseNotify",
                Protocol = "xml-rpc"
            }
        };
        private FeedCloudValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedCloudValidator();
        }

        [TestCaseSource("validFeedCloudCases")]
        [Test]
        public void ValidFeedCloudShouldNotReturnValidatorErrors(FeedCloud feedCloud)
        {
            var result = validator.TestValidate(feedCloud);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestCaseSource("invalidFeedCloudCases")]
        [Test]
        public void EmptyDomainShouldReturnValidatorError(FeedCloud feedCloud)
        {
            var result = validator.TestValidate(feedCloud);

            result.ShouldHaveValidationErrorFor(feedCloud => feedCloud.Domain);
        }
    }
}