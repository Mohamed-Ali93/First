using First.Books;
using Xunit;

namespace First.EntityFrameworkCore.Applications.Books;

[Collection(FirstTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<FirstEntityFrameworkCoreTestModule>
{

}