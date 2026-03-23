using System.Threading.Tasks;
using Microsoft.Playwright;

public class AuthPage
{
    public string url = "https://www.saucedemo.com/";
    private readonly IPage _page;
    private readonly Dictionary<string, ILocator> elements;

    public AuthPage(IPage page)
    {
        this._page = page;

        this.elements = new Dictionary<string, ILocator>() {
            { "usernameInput", this._page.GetByPlaceholder("Username") },
            { "passwordInput", this._page.GetByPlaceholder("Password") },
            { "loginButton", this._page.GetByRole(AriaRole.Button, new() { Name = "Login" }) },
        };
    }

    public async Task LoginAsync(string username, string password)
    {
        await this.elements["usernameInput"].FillAsync(username);
        await this.elements["passwordInput"].FillAsync(password);
        await this.elements["loginButton"].ClickAsync();
    }

    public async Task GotoAsync()
    {
        await this._page.GotoAsync(this.url);
    }
}
