using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace PlaywrightTests;

public class Authenticate : PageTest
{

    [Fact]
    public async Task AcceptedUserNameLogin()
    {
        // Arrange
        AuthPage authPage = new AuthPage(Page);
        const string username = "standard_user";
        const string password = "secret_sauce";

        // Act
        await authPage.GotoAsync();
        await authPage.LoginAsync(username, password);

        // Asssert
        await Expect(Page.GetByText("Products")).ToBeVisibleAsync();
        await Expect(Page).ToHaveURLAsync(new Regex(".*/inventory"));
    }
}
