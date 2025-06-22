using First.Samples;
using Xunit;

namespace First.EntityFrameworkCore.Domains;

[Collection(FirstTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<FirstEntityFrameworkCoreTestModule>
{

}
