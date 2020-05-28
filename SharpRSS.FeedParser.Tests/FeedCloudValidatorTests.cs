namespace SharpRSS.FeedParser.Tests
{
    using FluentValidation.TestHelper;
    using NUnit.Framework;
    using SharpRSS.FeedParser.Models;
    using SharpRSS.FeedParser.Validators;

    public class FeedCloudValidatorTests
    {
        private FeedCloudValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FeedCloudValidator();
        }

        [Test]
        public void ValidFeedCloudShouldNotReturnValidatorErrors()
        {
            var feedCloud = new FeedCloud()
            {
                Domain = "rpc.sys.com",
                Port = 80,
                Path = "/RPC2",
                RegisterProcedure = "myCloud.rssPleaseNotify",
                Protocol = "xml-rpc"
            };

            var result = validator.TestValidate(feedCloud);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void EmptyFeedCloudDomainShouldReturnValidatorError()
        {
            var feedCloud = new FeedCloud()
            {
                Domain = "",
                Port = 80,
                Path = "/RPC2",
                RegisterProcedure = "myCloud.rssPleaseNotify",
                Protocol = "xml-rpc"
            };

            var result = validator.TestValidate(feedCloud);

            result.ShouldHaveValidationErrorFor(feedCloud => feedCloud.Domain);
        }

        [Test]
        public void FeedCloudPortLessThanMinShouldReturnValidatorError()
        {
            var feedCloud = new FeedCloud()
            {
                Domain = "rpc.sys.com",
                Port = -1,
                Path = "/RPC2",
                RegisterProcedure = "myCloud.rssPleaseNotify",
                Protocol = "xml-rpc"
            };

            var result = validator.TestValidate(feedCloud);

            result.ShouldHaveValidationErrorFor(feedCloud => feedCloud.Port);
        }

        [Test]
        public void FeedCloudPortGreaterThanMaxShouldReturnValidatorError()
        {
            var feedCloud = new FeedCloud()
            {
                Domain = "rpc.sys.com",
                Port = 80000,
                Path = "/RPC2",
                RegisterProcedure = "myCloud.rssPleaseNotify",
                Protocol = "xml-rpc"
            };

            var result = validator.TestValidate(feedCloud);

            result.ShouldHaveValidationErrorFor(feedCloud => feedCloud.Port);
        }

        [Test]
        public void EmptyFeedCloudPathShouldReturnValidatorError()
        {
            var feedCloud = new FeedCloud()
            {
                Domain = "rpc.sys.com",
                Port = 80,
                Path = "",
                RegisterProcedure = "myCloud.rssPleaseNotify",
                Protocol = "xml-rpc"
            };

            var result = validator.TestValidate(feedCloud);

            result.ShouldHaveValidationErrorFor(feedCloud => feedCloud.Path);
        }

        [Test]
        public void EmptyFeedCloudRegisterProcedureShouldReturnValidatorError()
        {
            var feedCloud = new FeedCloud()
            {
                Domain = "rpc.sys.com",
                Port = 80,
                Path = "/RPC2",
                RegisterProcedure = "",
                Protocol = "xml-rpc"
            };

            var result = validator.TestValidate(feedCloud);

            result.ShouldHaveValidationErrorFor(feedCloud => feedCloud.RegisterProcedure);
        }

        [Test]
        public void EmptyFeedCloudProtocolShouldReturnValidatorError()
        {
            var feedCloud = new FeedCloud()
            {
                Domain = "rpc.sys.com",
                Port = 80,
                Path = "/RPC2",
                RegisterProcedure = "myCloud.rssPleaseNotify",
                Protocol = ""
            };

            var result = validator.TestValidate(feedCloud);

            result.ShouldHaveValidationErrorFor(feedCloud => feedCloud.Protocol);
        }
    }
}