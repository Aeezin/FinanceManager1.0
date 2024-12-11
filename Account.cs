public class Account
{
    public static string createUsername = "";
    public static string createPassword = "";

    public static void CreateAccount()
    {
        Console.Write("Username: ");
        createUsername = Console.ReadLine()!;
        Console.Write("Password: ");
        createPassword = Console.ReadLine()!;
    }


    public static bool Login()
    {
        Console.Write("Username: ");
        string loginUsername = Console.ReadLine()!;
        Console.Write("Password: ");
        string loginPassword = Console.ReadLine()!;

        if (!loginUsername.Equals(createUsername) || !loginPassword.Equals(createPassword))
        {
            Console.WriteLine("Failed to login.");
            Console.ReadKey();
            return false;
        }
        Console.WriteLine("Succesfully logged in.");
        return true;
    }
}
