class Program
{
    private static string username = "";
    private static string password = "";

    static async Task Main(string[] args)
    {
        AccountManager.CreateTables();

        Console.Clear();
        await MenuManager.MainMenu();
    }
}