public static class UserCredentials
{
  // default ones for successful login
  public static string Username = "standard_user";
  public static string Password = "secret_sauce";

  public static (string, string) Default()
  {
    return (Username, Password)
  }
}
