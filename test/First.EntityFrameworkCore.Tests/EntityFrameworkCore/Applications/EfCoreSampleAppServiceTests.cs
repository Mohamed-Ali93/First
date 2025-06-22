using First.Samples;
using Xunit;

namespace First.EntityFrameworkCore.Applications;

[Collection(FirstTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<FirstEntityFrameworkCoreTestModule>
{

}
