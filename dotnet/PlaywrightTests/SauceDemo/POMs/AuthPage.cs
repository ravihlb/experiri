using Microsoft.Playwright;

public class AuthPage
{
    public string Url = "https://www.saucedemo.com/";
    private readonly IPage _page;

    public record AuthPageElements(
        ILocator UsernameInput,
        ILocator PasswordInput,
        ILocator LoginButton,
        ILocator ErrorMessage
    );

    public readonly AuthPageElements Elements;

    public AuthPage(IPage page)
    {
        _page = page;
        Elements = new(
            _page.GetByPlaceholder("Username"),
            _page.GetByPlaceholder("Password"),
            _page.GetByRole(AriaRole.Button, new() { Name = "Login" }),
            _page.Locator("[data-test='error']")
        );
    }

    public async Task LoginAsync(string username, string password)
    {
        await Elements.UsernameInput.FillAsync(username);
        await Elements.PasswordInput.FillAsync(password);
        await Elements.LoginButton.ClickAsync();
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync(Url);
    }
}
