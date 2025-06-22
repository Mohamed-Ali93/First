using Xunit;

namespace First.EntityFrameworkCore;

[CollectionDefinition(FirstTestConsts.CollectionDefinitionName)]
public class FirstEntityFrameworkCoreCollection : ICollectionFixture<FirstEntityFrameworkCoreFixture>
{

}
