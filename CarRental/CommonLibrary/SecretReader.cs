using Newtonsoft.Json;

namespace CarRental.Common;
public static class SecretReader
{
    public static string Email { get; set; }
    public static string Password { get; set; }

    public static void ReadSecrects()
    {
        string filePath = "appsecrets.json";

        string content = File.ReadAllText(filePath);
        var result = JsonConvert.DeserializeObject<EmailSecret>(content);
        Email = result.Email;
        Password = result.Password;
    }
}

internal class EmailSecret
{
    public string Email { get; set; }
    public string Password { get; set; }
}
