using Microsoft.Playwright;

public class InventoryPage
{
  public string Url = "https://www.saucedemo.com/inventory";
  private readonly IPage _page;
  public readonly InventoryPageElements Elements;

  public class InventoryPageElements
  {
    public ILocator sidebarButton { get; init; }
    public ILocator logoutButton { get; init; }

    public InventoryPageElements(IPage page)
    {
      sidebarButton = page.GetByRole(AriaRole.Button, new() { Name = "Open Menu" });
      logoutButton = page.Locator("[data-test='logout-sidebar-link']");
    }
  }

  public InventoryPage(IPage page)
  {
    _page = page;
    Elements = new InventoryPageElements(_page);
  }

  public async Task Logout()
  {
    await Elements.sidebarButton.ClickAsync();
    await Elements.logoutButton.ClickAsync();
  }
}
