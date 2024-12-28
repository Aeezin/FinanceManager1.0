using System.Runtime.Intrinsics.Arm;

public class MenuManager
{
    public static int currentAccountId = -1;
    public static bool loginMenuRunning = true;
    public static bool transactionMenuRunning = false;
    public static string userChoice = "";
    public static async Task LoginChoice()
    {
        while (loginMenuRunning)
        {
            Console.Clear();
            LoginMenu.Execute();

            userChoice = Console.ReadLine()!;

            switch (userChoice)
            {
                case "1":
                    Console.Clear();
                    await AccountManager.CreateAccount();
                    break;
                case "2":
                    Console.Clear();
                    await AccountManager.Login();
                    break;
                case "3":
                    loginMenuRunning = false;
                    break;
            }
        }
    }

    public static async Task TransactionChoice()
    {

        while (transactionMenuRunning)
        {
            Console.Clear();
            TransactionMenu.Execute();

            userChoice = Console.ReadLine()!;

            switch (userChoice)
            {
                case "1":
                    await TransactionManager.AddTransaction(currentAccountId);
                    break;
                case "2":
                    Console.Write("Enter transaction ID to delete: ");
                    if (int.TryParse(Console.ReadLine(), out int transactionId))
                    {
                        await TransactionManager.RemoveTransaction(transactionId);
                    }
                    break;
                case "3":
                    await TransactionManager.ShowBalance(currentAccountId);
                    break;
                case "4":

                    break;
                case "5":

                    break;
                case "6":
                    transactionMenuRunning = false;
                    loginMenuRunning = true;
                    await LoginChoice();
                    break;
            }
        }
    }
}