namespace FinanceManagerV5;

class Program
{
    private static string username = "";
    private static string password = "";

    static void Main(string[] args)
    {
        Console.Clear();
        bool loginMenuRunning = true;
        bool transactionMenuRunning = false;

        string userChoice;

        while (loginMenuRunning)
        {
            LoginMenu.Execute();

            userChoice = Console.ReadLine()!;

            switch (userChoice)
            {
                case "1":
                    Console.Clear();
                    Account.CreateAccount();
                    break;
                case "2":
                    Console.Clear();
                    Account.Login();
                    // logik
                    break;
                case "3":
                    loginMenuRunning = false;
                    break;
            }
        }

        while (transactionMenuRunning)
        {
            TransactionMenu.Execute();

            userChoice = Console.ReadLine()!;

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
            }
        }
    }
}
