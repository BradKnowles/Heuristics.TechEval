using NUnit.Framework;

namespace Tests.Integration
{
    [SetUpFixture]
    public class SetupFixture
    {
        [OneTimeSetUp]
        public void Setup()
        {
            var context = new TestDataContext();
            context.Database.Initialize(true);
        }
    }
}
