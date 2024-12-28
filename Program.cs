using Npgsql;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionString = DatabaseConnection.GetConnectionString();
        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);

        AccountManager.CreateTables();
        Console.Clear();
        await MenuManager.LoginChoice();

        
    }
}
