using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class SidebarTests : PageTest
{
  InventoryPage inventoryPage = null!;
  AuthPage authPage = null!;

  override public async Task InitializeAsync()
  {
    await base.InitializeAsync();

    authPage = new AuthPage(Page);
    inventoryPage = new InventoryPage(Page);

    await authPage.GotoAsync();
    await authPage.LoginAsync();

    await Expect(Page).ToHaveURLAsync(InventoryPage.Url);
  }

  [Fact]
  public async Task Positive_ToggleSidebar()
  {
    await inventoryPage.OpenSidebarMenu();
    await Expect(inventoryPage.Elements.CloseSidebarButton).ToBeVisibleAsync();

    await inventoryPage.CloseSidebarMenu();
    await Expect(inventoryPage.Elements.OpenSidebarButton).ToBeVisibleAsync();
  }
}
