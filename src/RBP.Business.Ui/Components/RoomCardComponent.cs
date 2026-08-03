using Microsoft.Playwright;
using RBP.Core.Helpers;
using RBP.Data.DTO.Room;
using System.Xml.Linq;

namespace RBP.Business.Ui.Components;

public sealed class RoomCardComponent
{
    private readonly ILocator _root;

    public RoomCardComponent(ILocator root)
    {
        _root = root;
    }

    public ILocator Type =>
        _root.Locator(".card-title");

    private ILocator Description => _root.Locator("p.card-text");

    public ILocator Price =>
        _root.Locator(".fw-bold.fs-5");

    public ILocator Image =>
        _root.Locator("img");

    public ILocator Features =>
        _root.Locator(".badge");

    public ILocator BookButton =>
        _root.Locator(".btn-primary");

    public async Task<RoomCardDto> GetDataAsync()
    {
        string href = await BookButton.GetAttributeAsync("href");

        return new RoomCardDto
        {
            Id = int.Parse(
                href.Split("/reservation/")[1]
                    .Split("?")[0]),

            Type = StringNormalizer.Normalize(await Type.InnerTextAsync()),

            Description = StringNormalizer.Normalize(await Description.InnerTextAsync()),

            Image = StringNormalizer.Normalize(await Image.GetAttributeAsync("src")!),

            Price = decimal.Parse(
                StringNormalizer.Normalize((await Price.InnerTextAsync())
                        .Replace("£", "")
                        .Replace("per night", ""))),

            Features = StringNormalizer.Normalize(await Features.AllInnerTextsAsync())
        };
    }
}
