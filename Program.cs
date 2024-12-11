namespace FinanceManagerV5;

class Program
{
    private static string username = "";
    private static string password = "";

    static void Main(string[] args)
    {
<<<<<<< HEAD
        Console.Clear();
        bool loginMenuRunning = true;
        bool transactionMenuRunning = false;

        string userChoice;
=======
        bool loginMenuRunning = true;
        bool transactionMenuRunning = true;

        string userChoice = Console.ReadLine();
>>>>>>> 449228e3d9196ac3386390ed965383b997a1779d

        while (loginMenuRunning)
        {
            LoginMenu.Execute();

<<<<<<< HEAD
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
                    if (Account.Login().Equals(true))
                    {
                        transactionMenuRunning = true;
                    }
                    break;
                case "3":
                    loginMenuRunning = false;
=======
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
>>>>>>> 449228e3d9196ac3386390ed965383b997a1779d
                    break;
            }
        }

        while (transactionMenuRunning)
        {
            TransactionMenu.Execute();

<<<<<<< HEAD
            userChoice = Console.ReadLine()!;

=======
>>>>>>> 449228e3d9196ac3386390ed965383b997a1779d
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
<<<<<<< HEAD
                    transactionMenuRunning = false;
                    break;
=======
                    
                    transactionMenuRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
>>>>>>> 449228e3d9196ac3386390ed965383b997a1779d
            }
        }
    }
}
