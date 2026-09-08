using RBP.Business.Ui.Pages;
using RBP.Tests.Ui.Assertions;
using RBP.Tests.Ui.Base;

namespace RBP.Tests.Ui.HomePageFixtures
{
    public class HomePageFixture : BaseFixture
    {
        [Test]
        public async Task HomePage_Should_Open()
        {
            HomePage homePage = await CreatePage<HomePage>()
                .OpenAsync();

            await homePage.ShouldBeOpen();
        }
    }
}
