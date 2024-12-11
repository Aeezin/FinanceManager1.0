namespace FinanceManagerV5;

class Program
{
    static void Main(string[] args)
    {
        bool loginMenuRunning = true;
        bool transactionMenu = true;

        string userChoice;

        while (loginMenuRunning)
        {
            LoginMenu.Execute();

            userChoice = Console.ReadLine()!;

            switch (userChoice)
            {
                case "1":
                    Account.CreateAccount();
                    break;
                case "2":

                    break;
                case "3":
                    loginMenuRunning = false;
                    transactionMenu = false;
                    break;
            }
        }

        while (transactionMenu)
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
                    transactionMenu = false;
                    break;
            }
        }
    }
}
