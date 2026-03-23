using System.Text.RegularExpressions;
using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class AuthenticatePageTests : PageTest
{

  // same password for all users
  const string password = "secret_sauce";
  AuthPage authPage = null!;

  override public async Task InitializeAsync()
  {
    Console.WriteLine("Test Setup step");
    await base.InitializeAsync();
    authPage = new AuthPage(Page);
  }

  public override async Task DisposeAsync()
  {
    Console.WriteLine("After each test cleanup");
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
    // Arrange
    const string username = "locked_out_user";
    const string wrongPassword = "randomwrongpassword";

    // Act
    await authPage.GotoAsync();
    await authPage.LoginAsync(username, wrongPassword);

    // Asssert
    await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/inventory"));
    await Expect(authPage.Elements.errorMessage).ToBeVisibleAsync();
  }

  [Fact]
  public async Task Negative_LockedOutUser()
  {
    // Arrange
    const string username = "locked_out_user";

    // Act
    await authPage.GotoAsync();
    await authPage.LoginAsync(username, password);

    // Asssert
    await Expect(Page).Not.ToHaveURLAsync(new Regex(".*/inventory"));
  }
}
