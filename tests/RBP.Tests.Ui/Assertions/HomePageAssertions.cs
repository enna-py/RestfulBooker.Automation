using FluentAssertions;
using global::RBP.Business.Ui.Pages;
using RestfulBooker.Core.Configuration;

namespace RBP.Business.Ui.Assertions;

public static class HomePageAssertions
{
    public static void ShouldBeOpen(this HomePage page)
    {
        page.Url.Should().Be(
            ConfigurationService.Current.Ui.BaseUrl);
    }
}
