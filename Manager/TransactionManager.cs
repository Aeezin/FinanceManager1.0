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

    private static async Task ShowTransactionByPeriod(int accountId, DateTime startDate, DateTime endDate, bool isIncome)
    {
        string typeCondition = isIncome ? "amount > 0" : "amount < 0";

        try
        {
            await using NpgsqlConnection connection = new NpgsqlConnection(DatabaseConnection.GetConnectionString());
            await connection.OpenAsync();

            NpgsqlCommand cmd = new(
                $@"
                SELECT transaction_id, amount, description, transaction_date
                FROM transactions
                WHERE account_id = @account_id AND transaction_date BETWEEN @start_date AND @end_date AND {typeCondition}
                ORDER BY transaction_date ASC", connection);

            cmd.Parameters.AddWithValue("@account_id", accountId);
            cmd.Parameters.AddWithValue("@start_date", startDate);
            cmd.Parameters.AddWithValue("@end_date", endDate);

            await using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            bool hasTransactions = false;
            while (await reader.ReadAsync())
            {
                hasTransactions = true;
                int transactionId = reader.GetInt32(0);
                decimal amount = reader.GetDecimal(1);
                string description = reader.GetString(2);
                DateTime date = reader.GetDateTime(3);

                Console.WriteLine($"{date.ToShortDateString()} | {amount:C} | {description}");

                
            }
            if (!hasTransactions)
            {
                Console.WriteLine(isIncome ? "No income found for the specified period." : "No expense found for the specified period.");
            }

            await connection.CloseAsync();
        }
        catch (Exception execption)
        {
            Console.WriteLine($"An error has occurred: {execption.Message}");
        }
    }

    public static async Task ShowIncomeByPeriod(int accountId, DateTime startDate, DateTime endDate)
    {
        Console.WriteLine("Income Transactions:");
        await ShowTransactionByPeriod(accountId, startDate, endDate, true);
    }

    public static async Task ShowExpenseByPeriod(int accountId, DateTime startDate, DateTime endDate)
    {
        Console.WriteLine("Expense Transactions:");
        await ShowTransactionByPeriod(accountId, startDate, endDate, false);
    }

    public static async Task FilterTransactionsMenu(int accountId, bool isIncome)
    {
        Console.WriteLine("Choose a period to filter by:");
        Console.WriteLine("1. Day");
        Console.WriteLine("2. Week");
        Console.WriteLine("3. Month");
        Console.WriteLine("4. Year");
        Console.Write("Your choice: ");
        string choice = Console.ReadLine()!;

        DateTime startDate, endDate;

        switch (choice)
        {
            case "1": // day
                Console.Write("Enter day (YYYY-MM-DD): ");
                if (!DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }
                endDate = startDate.AddDays(1).AddSeconds(-1);
                break;

            case "2": // week
                Console.Write("Enter week number (1-53): ");
                if (!int.TryParse(Console.ReadLine(), out int weekNumber) || weekNumber < 1 || weekNumber > 53)
                {
                    Console.WriteLine("Invalid week number.");
                    return;
                }
                Console.Write("Enter year: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    Console.WriteLine("Invalid year.");
                    return;
                }
                startDate = FirstDateOfWeek(year, weekNumber);
                endDate = startDate.AddDays(7).AddSeconds(-1);
                break;

            case "3": // month
                Console.Write("Enter month (1-12): ");
                if (!int.TryParse(Console.ReadLine(), out int month) || month < 1 || month > 12)
                {
                    Console.WriteLine("Invalid month.");
                    return;
                }
                Console.Write("Enter year: ");
                if (!int.TryParse(Console.ReadLine(), out year))
                {
                    Console.WriteLine("Invalid year.");
                    return;
                }
                startDate = new DateTime(year, month, 1);
                endDate = startDate.AddMonths(1).AddSeconds(-1);
                break;

            case "4": // year
                Console.Write("Enter year: ");
                if (!int.TryParse(Console.ReadLine(), out year))
                {
                    Console.WriteLine("Invalid year.");
                    return;
                }
                startDate = new DateTime(year, 1, 1);
                endDate = startDate.AddYears(1).AddSeconds(-1);
                break;

            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        if (isIncome)
        {
            await ShowIncomeByPeriod(accountId, startDate, endDate);
        }
        else
        {
            await ShowExpenseByPeriod(accountId, startDate, endDate);
        }
    }

    private static DateTime FirstDateOfWeek(int year, int weekOfYear)
    {
        DateTime jan1 = new DateTime(year, 1, 1);
        int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

        DateTime firstThursday = jan1.AddDays(daysOffset);
        var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
        int firstWeek = cal.GetWeekOfYear(firstThursday, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        if (firstWeek <= 1)
        {
            weekOfYear -= 1;
        }

        return firstThursday.AddDays(weekOfYear * 7 - 10).Date;
    }
}