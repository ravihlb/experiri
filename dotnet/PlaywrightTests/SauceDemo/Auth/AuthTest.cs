using System.Text.RegularExpressions;
using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class AuthenticatePageTests : PageTest
{

  const string password = "secret_sauce";
  AuthPage authPage = null!;

  override public async Task InitializeAsync()
  {
    await base.InitializeAsync();
    authPage = new AuthPage(Page);
  }

  public override async Task DisposeAsync()
  {
    await base.DisposeAsync();
  }

  [Fact]
  public async Task Positive_AcceptedUserNameLogin()
  {
    // Arrange
    const string username = "standard_user";

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

    await Expect(authPage.Elements.errorMessage).ToBeVisibleAsync();
    await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/inventory"));
  }

  [Fact]
  public async Task Negative_LockedOutUser()
  {
    const string username = "locked_out_user";

    await authPage.GotoAsync();
    await authPage.LoginAsync(username, password);

    await Expect(authPage.Elements.errorMessage).ToBeVisibleAsync();
    await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/inventory"));
  }
}
