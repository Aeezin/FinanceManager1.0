using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = DatabaseConnection.GetConnectionString();
        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);

        bool loginMenuRunning = true;
        bool transactionMenuRunning = true;

        string userChoice = Console.ReadLine()!;

        while (loginMenuRunning)
        {
            connection.Open();
            LoginMenu.Execute();

            switch (userChoice)
            {
                case "1":
                    // Logga in
                    break;
                case "2":
                    // Skapa account
                    break;
                case "3":
                    loginMenuRunning = false;
                    transactionMenuRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
            connection.Close();
        }

        while (transactionMenuRunning)
        {
            connection.Open();
            TransactionMenu.Execute();

            switch (userChoice)
            {
                case "1":

                    break;
                case "2":

                    break;
                case "3":

                    break;
                case "4":

                    break;
                case "5":

                    break;
                case "6":

                    transactionMenuRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}
