using System.Data;
using System.Linq.Expressions;
using Npgsql;

public class TransactionManager
{
    public static async Task AddTransaction(int account_id)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(
                DatabaseConnection.GetConnectionString()
            );
            await connection.OpenAsync();

            Console.Write("Enter an amount (positive for income, negative for expense): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount. Please try again.");
                return;
            }

            Console.Write("Enter a description (optional): ");
            string? description = Console.ReadLine();

            NpgsqlCommand cmd = new(
                @"
                INSERT INTO transactions (account_id, amount, description)
                VALUES (@account_id, @amount, @description);", connection
            );

            cmd.Parameters.AddWithValue("@account_id", account_id);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@description", (object?)description ?? DBNull.Value);

            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine("Transaction added.");

            await connection.CloseAsync();

        }
        catch (Exception execption)
        {
                
            Console.WriteLine($"An error has occured: {execption.Message}");
        }
    }

    public static async Task RemoveTransaction(int transactionId)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(DatabaseConnection.GetConnectionString());
            await connection.OpenAsync();

            NpgsqlCommand cmd = new(
                @"DELETE FROM transactions WHERE transaction_id = @transaction_id", connection);
            
            cmd.Parameters.AddWithValue("@transaction_id", transactionId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine(rowsAffected > 0 ? "Transaction deleted." : "Transactions not found.");

            await connection.CloseAsync();
        }
        catch (Exception execption)
        {
            Console.WriteLine($"An error has occurred: {execption.Message}");
        }
    }

    public static async Task ShowBalance(int accountId)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(DatabaseConnection.GetConnectionString());
            await connection.OpenAsync();

            NpgsqlCommand cmd = new(
                @"SELECT COALESCE(SUM(amount), 0) AS balance FROM transactions WHERE account_id = @account_id", connection);

            cmd.Parameters.AddWithValue("@account_id", accountId);

            await connection.CloseAsync();
        }
        catch (Exception execption)
        {
            Console.WriteLine($"An error has occurred: {execption.Message}");
        }
    }

    public static async Task ShowBalanceByPeriod(int accountId, DateTime startDate, DateTime endDate)
    {
        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(DatabaseConnection.GetConnectionString());
            await connection.OpenAsync();

            NpgsqlCommand cmd = new(
                @"
                SELECT transaction_id, amount, description, transaction_date
                FROM transactions
                WHERE account_id = @account_id AND transaction_date BETWEEN @start_date AND @end_date", connection);

            cmd.Parameters.AddWithValue("@account_id", accountId);
            cmd.Parameters.AddWithValue("@start_date", startDate);
            cmd.Parameters.AddWithValue("@end_date", endDate);

            await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int transactionId = reader.GetInt32(0);
                decimal amount = reader.GetDecimal(1);
                string description = reader.GetString(2);
                DateTime date = reader.GetDateTime(3);

                Console.WriteLine($"{date.ToShortDateString()} | {amount:C} | {description}");

                await connection.CloseAsync();
            }
        }
        catch (Exception execption)
        {
            Console.WriteLine($"An error has occurred: {execption.Message}");
        }
    }
}