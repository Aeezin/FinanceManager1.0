using System.Security.Cryptography.X509Certificates;
using Npgsql;

public class Account
{
    public static string username;
    public static string password;

    public static void CreateAccount()
    {
        Console.Write("Username: ");
        username = Console.ReadLine()!;
        Console.Write("Password: ");
        password = Console.ReadLine()!;
    }

    public static void Login()
    {
        Console.Write("Username: ");
        string loginUsername = Console.ReadLine()!;
        Console.Write("Password: ");
        string loginPassword = Console.ReadLine()!;

        if (!loginUsername.Equals(username) && loginPassword.Equals(password))
        {
            Console.WriteLine("Failed to login.");
        }
    }

    public static void Logout() { }
}
