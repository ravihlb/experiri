public static class UserCredentials
{
  // default ones for successful login
  public const string Username = "standard_user";
  public const string Password = "secret_sauce";

  public static (string, string) Default()
  {
    return (Username, Password);
  }
}
