class Program
{
    private static string username = "";
    private static string password = "";

    static void Main(string[] args)
    {
        AccountManager.CreateTables();

        Console.Clear();
        MenuManager.LoginMenu();
    }
}