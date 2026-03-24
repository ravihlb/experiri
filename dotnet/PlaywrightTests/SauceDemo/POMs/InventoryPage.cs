using Microsoft.Playwright;

public class InventoryPage
{
  public const string Url = "https://www.saucedemo.com/inventory.html";
  private readonly IPage _page;

  public record InventoryPageElements(
    ILocator OpenSidebarButton,
    ILocator CloseSidebarButton,
    ILocator LogoutButton
  );

  public readonly InventoryPageElements Elements;

  public InventoryPage(IPage page)
  {
    _page = page;
    Elements = new(
        _page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" }),
        _page.GetByRole(AriaRole.Button, new() { Name = "Close Menu" }),
        _page.Locator("[data-test='logout-sidebar-link']")
    );
  }

  public async Task OpenSidebarMenu()
  {
    await Elements.OpenSidebarButton.ClickAsync();
  }

  public async Task CloseSidebarMenu()
  {
    await Elements.CloseSidebarButton.ClickAsync();
  }

  public async Task Logout()
  {
    await Elements.OpenSidebarButton.ClickAsync();
    await Elements.LogoutButton.ClickAsync();
  }
}
