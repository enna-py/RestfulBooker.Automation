using RBP.Business.Ui.Pages;
using RBP.Tests.Ui.Assertions;
using RBP.Tests.Ui.Base;
using RestfulBooker.Core.Constants;

namespace RBP.Tests.Ui.HomePageFixtures
{
    public class HomePageFixture : BaseFixture
    {
        [Test]
        [Category(TestType.UI)]
        // TODO: missing [Property("JiraKey", "RBP-XX")] — no corresponding Jira issue could be
        // determined from the repository; assign manually.
        public async Task HomePage_Should_Open()
        {
            HomePage homePage = await CreatePage<HomePage>()
                .OpenAsync();

            await homePage.ShouldBeOpen();
        }
    }
}
