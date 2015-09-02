namespace LH.TestObjects.Tests.Compare.Logging.Logger
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;
    using TestObjects.Compare.Logging;

    [TestFixture]
    public class WhenLogging
    {
        [Test]
        public void ThenShouldNotLogEventIfLevelIsHigher()
        {
            var logger = new Logger();
            logger.SetLevel(LogLevel.Error);
            logger.Callback(x => Assert.Fail("The callback should not be called"));

            Log(logger, LogLevel.Info);
        }

        [Test]
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public void ThenShouldLogIfLevelIsSameOrLower()
        {
            var hasBeenCalled = false;

            var logger = new Logger();
            logger.SetLevel(LogLevel.Info);
            logger.Callback(x => hasBeenCalled = true);

            Log(logger, LogLevel.Error);
            hasBeenCalled.Should().BeTrue();

            hasBeenCalled = false;
            Log(logger, LogLevel.Info);            
            hasBeenCalled.Should().BeTrue();
        }

        [Test]
        public void ThenLogEventShouldHavePropertiesSet()
        {
            var context = new ComparisonContext(null, null, null);

            var logger = new Logger();
            logger.Callback(x =>
            {
                x.Message.Should().Be("Hello, world!");
                x.Level.Should().Be(LogLevel.Error);
                x.Context.Should().NotBeNull();
            });

            logger.Log(LogLevel.Error, context, "Hello, {0}!", "world");
        }

        private static void Log(Logger logger, LogLevel level)
        {
            logger.Log(level, new ComparisonContext(null, null, null), "Message");
        }
    }
}
