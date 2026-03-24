using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class SidebarTests : PageTest
{
  InventoryPage inventoryPage = null!;

  override public async Task InitializeAsync()
  {
    await base.InitializeAsync();
    inventoryPage = new InventoryPage(Page);
  }

  [Fact]
  public async Task Positive_ToggleSidebar()
  {

  }
}
