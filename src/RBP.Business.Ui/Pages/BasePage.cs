using Microsoft.Playwright;

namespace RBP.Business.Ui.Pages;
public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page)
    {
        Page = page;
    }

    public string Url => Page.Url;

    public IPage PlaywrightPage => Page;
}
