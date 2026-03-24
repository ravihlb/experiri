using System.Text.RegularExpressions;
using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class LoginTests : PageTest
{

  const string password = "secret_sauce";
  AuthPage authPage = null!;

  override public async Task InitializeAsync()
  {
    await base.InitializeAsync();
    authPage = new AuthPage(Page);
  }

  [Fact]
  public async Task Positive_AcceptedUserNameLogin()
  {
    // Arrange
    (string username, string password) = UserCredentials.Default();

    // Act
    await authPage.GotoAsync();
    await authPage.LoginAsync(username, password);

    // Asssert
    await Expect(Page).ToHaveURLAsync(new Regex(".*/inventory"));
  }

  [Fact]
  public async Task Negative_WrongPassword()
  {
    const string username = "locked_out_user";
    const string wrongPassword = "randomwrongpassword";

    await authPage.GotoAsync();
    await authPage.LoginAsync(username, wrongPassword);

    await Expect(authPage.Elements.ErrorMessage).ToBeVisibleAsync();
    await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/inventory"));
  }

  [Fact]
  public async Task Negative_LockedOutUser()
  {
    const string username = "locked_out_user";

    await authPage.GotoAsync();
    await authPage.LoginAsync(username, password);

    await Expect(authPage.Elements.ErrorMessage).ToBeVisibleAsync();
    await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/inventory"));
  }

  [Fact]
  public async Task Positive_Logout()
  {
    InventoryPage inventoryPage = new InventoryPage(Page);

    await authPage.GotoAsync();
    await authPage.LoginAsync(UserCredentials.Username, UserCredentials.Password);

    await inventoryPage.Logout();
    await Expect(Page).Not.ToHaveURLAsync(inventoryPage.Url);
  }
}
