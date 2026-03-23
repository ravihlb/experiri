using Microsoft.Playwright;

public class AuthPage
{
    public string url = "https://www.saucedemo.com/";
    private readonly IPage _page;
    public readonly AuthPageElements Elements;

    public class AuthPageElements
    {
        public ILocator usernameInput { get; init; }
        public ILocator passwordInput { get; init; }
        public ILocator loginButton { get; init; }
        public ILocator errorMessage { get; init; }

        public AuthPageElements(IPage page)
        {
            usernameInput = page.GetByPlaceholder("Username");
            passwordInput = page.GetByPlaceholder("Password");
            loginButton = page.GetByRole(AriaRole.Button, new() { Name = "Login" });
            errorMessage = page.Locator("[data-test='error']");
        }
    }

    public AuthPage(IPage page)
    {
        _page = page;
        Elements = new AuthPageElements(_page);
    }

    public async Task LoginAsync(string username, string password)
    {
        await Elements.usernameInput.FillAsync(username);
        await Elements.passwordInput.FillAsync(password);
        await Elements.loginButton.ClickAsync();
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync(url);
    }
}
