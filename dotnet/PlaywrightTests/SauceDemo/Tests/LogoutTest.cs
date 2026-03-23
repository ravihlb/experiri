using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class LogoutTests : PageTest
{
  AuthPage authPage = null!;
  InventoryPage inventoryPage = null!;

  override public async Task InitializeAsync()
  {
    await base.InitializeAsync();
    authPage = new AuthPage(Page);
    inventoryPage = new InventoryPage(Page);

    await authPage.GotoAsync();
    await authPage.LoginAsync(UserCredentials.username, UserCredentials.password);
  }

  override public async Task DisposeAsync()
  {
    await base.DisposeAsync();
  }

  [Fact]
  public async Task Positive_Logout()
  {
    await inventoryPage.Logout();
    await Expect(Page).Not.ToHaveURLAsync(inventoryPage.Url);
  }
}
