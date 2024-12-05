namespace FinanceManagerV5;

class Program
{
    static void Main(string[] args)
    {
        bool loginMenuRunning = true;
        bool transactionMenu = true;

        string? userChoice = Console.ReadLine();

        while (loginMenuRunning)
        {
            switch (userChoice)
            {
                case "1":

                    break;
                case "2":

                    break;
                case "3":
                    loginMenuRunning = false;
                    break;
            }
        }

        while (transactionMenu)
        {
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
                
                    break;
            }
        }
    }
}
