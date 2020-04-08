using Xunit;

namespace Tests
{
    [CollectionDefinition("MainFixtureCollection")]
    public class MainFixtureCollection : ICollectionFixture<TestFixture>
    {
    }
}