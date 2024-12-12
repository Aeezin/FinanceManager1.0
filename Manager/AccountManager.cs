using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Npgsql;

public class AccountManager
{
    public static void CreateTables()
    {
        NpgsqlConnection connection = new NpgsqlConnection(
            DatabaseConnection.GetConnectionString()
        );
        connection.Open();

        NpgsqlCommand cmd = new(
            @"
            BEGIN;
            CREATE TABLE IF NOT EXISTS accounts (
            account_id SERIAL PRIMARY KEY,
            account_name VARCHAR(50) UNIQUE NOT NULL,
            password VARCHAR(50) NOT NULL
            );

            CREATE TABLE IF NOT EXISTS transactions (
            transaction_id SERIAL PRIMARY KEY,
            account_id INT REFERENCES accounts(account_id),
            amount DECIMAL (10, 2) NOT NULL,
            description TEXT,
            transaction_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            COMMIT;
            );", connection);
        
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public static async Task CreateAccount()
    {   
        Console.Write("Username: ");
        string username = Console.ReadLine()!;
        Console.Write("Password: ");
        string password = Console.ReadLine()!;

        await using var connection = new NpgsqlConnection(
            DatabaseConnection.GetConnectionString()
        );
        await connection.OpenAsync();

        NpgsqlCommand cmd = new(
            @"
            BEGIN;
            INSERT INTO accounts(account_name, password) (
            VALUES(
            @account_name,
            @password)
            COMMIT;
            );", connection);

        cmd.Parameters.AddWithValue("account_name", $"{username}");
        cmd.Parameters.AddWithValue("password", $"{password}");

        await cmd.ExecuteNonQueryAsync();
        await connection.CloseAsync();
    }

    public static async Task Login()
    {
        string account_name = "";
        string password = "";

        Console.Write("Username: ");
        string username = Console.ReadLine()!;
        Console.Write("Password: ");
        string userPassword = Console.ReadLine()!;

        NpgsqlConnection connection = new NpgsqlConnection(
            DatabaseConnection.GetConnectionString()
        );
        await connection.OpenAsync();

        NpgsqlCommand cmd = new(
            @"
            SELECT account_name, password FROM accounts
            WHERE account_name = @account_name;", connection);

            cmd.Parameters.AddWithValue("account_name", $"{username}");

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                account_name = reader.GetString(0);
                password = reader.GetString(1);
            }
            if (!account_name.Equals(username) || !password.Equals(userPassword))
            {
                Console.WriteLine("Wrong login, try again.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Successfully logged in.");
            Console.ReadKey();
            MenuManager.loginMenuRunning = false;
            MenuManager.transactionMenuRunning = true;
            await connection.CloseAsync();
            await MenuManager.TransactionMenu();
    }
}
