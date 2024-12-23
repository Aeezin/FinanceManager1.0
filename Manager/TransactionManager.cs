using System.Globalization;

public class TransactionManager
{
    private static List<Transactions> transactions = new List<Transactions>();

    public static List<Transactions> GetAllTransactions(List<Transactions> transactions)
    {
        return transactions;
    }

    public static void LoadTransactions(List<Transactions> loadedTransactions)
    {
        transactions = loadedTransactions;
    }

    public static void AddTransaction(decimal amount, string description)
    {
        if (amount > 0)
        {
            transactions.Add(new Income(amount, description));
        }
        else
        {
            transactions.Add(new Expense(amount, description));
        }
        Console.WriteLine("Transaction added.");
        Console.ReadKey();
    }

    public static void RemoveTransaction(int userInputId)
    {
        int id = userInputId - 1;

        if (id < 0 || id >= transactions.Count)
        {
            Console.WriteLine("Invalid input.");
            Console.ReadKey();
            return;
        }

        transactions.RemoveAt(id);
        Console.WriteLine("Transaction removed.");
        Console.ReadKey();
    }

    public static void PrintAllTransactions()
    {
        Console.WriteLine("All transactions:");
        for (int i = 0; i < transactions.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {transactions[i]}");
            Console.WriteLine();
        }
    }

    public static decimal PrintBalance()
    {
        decimal balance = 0;
        for (int i = 0; i < transactions.Count; i++)
        {
            Transactions transaction = transactions[i];
            balance += transaction.Amount;
        }

        return balance;
    }

    public static void PrintIncomeByPeriod(string period)
    {
        List<Transactions> incomes = new List<Transactions>();

        if (transactions.Count == 0)
        {
            Console.WriteLine("There's no transactions to list.\nPress any key to continue.");
            return;
        }

        for (int i = 0; i < transactions.Count; i++)
        {
            Transactions t = transactions[i];
            if (t.Type == TransactionType.Income)
            {
                incomes.Add(t);
            }
        }

        if (incomes.Count == 0)
        {
            Console.WriteLine("There's no incomes to list.\nPress any key to continue.");
            return;
        }

        if (period.Equals("year"))
        {
            List<int> years = new List<int>();

            foreach (Transactions income in incomes)
            {
                int year = income.Date.Year;
                if (!years.Contains(year))
                {
                    years.Add(year);
                }
            }

            Console.WriteLine("Years with incomes:");
            foreach (int year in years)
            {
                Console.WriteLine(year);
                Console.WriteLine();
            }

            int selectedYear;
            while (true)
            {
                Console.Write("Please enter a year: ");
                if (
                    int.TryParse(Console.ReadLine(), out selectedYear)
                    && selectedYear >= 1900
                    && selectedYear <= 2100
                    && years.Contains(selectedYear)
                )
                {
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Invalid year or no incomes found for this year.\nPress any key to go back"
                    );
                }
            }

            Console.Clear();
            DisplayTransactionsForPeriod(incomes, selectedYear, period);
        }
        else if (period.Equals("month"))
        {
            List<int> months = new List<int>();

            foreach (Transactions income in incomes)
            {
                int month = income.Date.Month;
                if (!months.Contains(month))
                {
                    months.Add(month);
                }
            }

            Console.WriteLine("Months with incomes:");
            foreach (int month in months)
            {
                Console.WriteLine(month);
                Console.WriteLine();
            }

            int selectedMonth;
            while (true)
            {
                Console.WriteLine("Please select a month (1-12):");
                if (
                    int.TryParse(Console.ReadLine(), out selectedMonth)
                    && selectedMonth >= 1
                    && selectedMonth <= 12
                    && months.Contains(selectedMonth)
                )
                {
                    break;
                }
                Console.WriteLine(
                    "Invalid month or no incomes found for this month. Please try again."
                );
            }

            Console.Clear();
            DisplayTransactionsForPeriod(incomes, selectedMonth, period);
        }
        else if (period.Equals("week"))
        {
            List<int> weeks = new List<int>();

            Calendar calendar = CultureInfo.CurrentCulture.Calendar;
            foreach (Transactions income in incomes)
            {
                int week = calendar.GetWeekOfYear(
                    income.Date,
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday
                );
                if (!weeks.Contains(week))
                {
                    weeks.Add(week);
                }
            }

            Console.WriteLine("Weeks with incomes:");
            foreach (int week in weeks)
            {
                Console.WriteLine(week);
                Console.WriteLine();
            }

            int selectedWeek;
            while (true)
            {
                Console.WriteLine("Please select a week (1-53):");
                if (
                    int.TryParse(Console.ReadLine(), out selectedWeek)
                    && selectedWeek >= 1
                    && selectedWeek <= 53
                    && weeks.Contains(selectedWeek)
                )
                {
                    break;
                }
                Console.WriteLine(
                    "Invalid week or no incomes found for this week. Please try again."
                );
            }

            Console.Clear();
            DisplayTransactionsForPeriod(incomes, selectedWeek, period);
        }
        else if (period.Equals("day"))
        {
            List<int> days = new List<int>();

            foreach (Transactions income in incomes)
            {
                int day = income.Date.Day;
                if (!days.Contains(day))
                {
                    days.Add(day);
                }
            }

            Console.WriteLine("Days with incomes:");
            foreach (int day in days)
            {
                Console.WriteLine(day);
                Console.WriteLine();
            }

            int selectedDay;
            while (true)
            {
                Console.WriteLine("Please select a day (1-31):");
                if (
                    int.TryParse(Console.ReadLine(), out selectedDay)
                    && selectedDay >= 1
                    && selectedDay <= 31
                    && days.Contains(selectedDay)
                )
                {
                    break;
                }
                Console.WriteLine(
                    "Invalid day or no incomes found for this day. Please try again."
                );
            }

            Console.Clear();
            DisplayTransactionsForPeriod(incomes, selectedDay, period);
        }
        else
        {
            Console.WriteLine("Invalid choice. Please select 'year', 'month', 'week', or 'day'.");
        }
    }

    public static void PrintExpenseByPeriod(string period)
    {
        List<Transactions> expenses = new List<Transactions>();

        for (int i = 0; i < transactions.Count; i++)
        {
            Transactions t = transactions[i];
            if (t.Type == TransactionType.Expense)
            {
                expenses.Add(t);
            }
        }

        if (expenses.Count == 0)
        {
            Console.WriteLine("There's no expenses to list.\nPress any key to continue.");
            return;
        }

        if (period.Equals("year"))
        {
            List<int> years = new List<int>();

            foreach (Transactions expense in expenses)
            {
                int year = expense.Date.Year;

                if (!years.Contains(year))
                {
                    years.Add(year);
                }
            }

            Console.WriteLine("Years with expenses:");
            foreach (int year in years)
            {
                Console.WriteLine(year);
                Console.WriteLine();
            }

            int selectedYear;
            while (true)
            {
                Console.Write("Please enter a year: ");
                if (
                    int.TryParse(Console.ReadLine(), out selectedYear)
                    && selectedYear >= 1900
                    && selectedYear <= 2124
                    && years.Contains(selectedYear)
                )
                {
                    break;
                }
                else
                {
                    Console.WriteLine(
                        "Invalid year or no expenses found for this year. Please try again."
                    );
                }
            }

            Console.Clear();
            DisplayTransactionsForPeriod(expenses, selectedYear, period);
        }
        else if (period.Equals("month"))
        {
            List<int> months = new List<int>();

            foreach (Transactions expense in expenses)
            {
                int month = expense.Date.Month;
                if (!months.Contains(month))
                {
                    months.Add(month);
                }
            }

            Console.WriteLine("Months with expenses:");
            foreach (int month in months)
            {
                Console.WriteLine(month);
                Console.WriteLine();
            }

            int selectedMonth;
            while (true)
            {
                Console.Write("Please select a month (1-12): ");
                if (
                    int.TryParse(Console.ReadLine(), out selectedMonth)
                    && selectedMonth >= 1
                    && selectedMonth <= 12
                    && months.Contains(selectedMonth)
                )
                {
                    break;
                }
                Console.WriteLine(
                    "Invalid month or no expenses found for this month. Please try again."
                );
            }

            Console.Clear();
            DisplayTransactionsForPeriod(expenses, selectedMonth, period);
        }
        else if (period.Equals("week"))
        {
            List<int> weeks = new List<int>();

            Calendar calendar = CultureInfo.CurrentCulture.Calendar;
            foreach (Transactions expense in expenses)
            {
                int week = calendar.GetWeekOfYear(
                    expense.Date,
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday
                );
                if (!weeks.Contains(week))
                {
                    weeks.Add(week);
                }
            }

            Console.WriteLine("Weeks with expenses:");
            foreach (int week in weeks)
            {
                Console.WriteLine(week);
                Console.WriteLine();
            }

            int selectedWeek;
            while (true)
            {
                Console.Write("Please select a week (1-53): ");
                if (
                    int.TryParse(Console.ReadLine(), out selectedWeek)
                    && selectedWeek >= 1
                    && selectedWeek <= 53
                    && weeks.Contains(selectedWeek)
                )
                {
                    break;
                }
                Console.WriteLine(
                    "Invalid week or no expenses found for this week. Please try again."
                );
            }

            Console.Clear();
            DisplayTransactionsForPeriod(expenses, selectedWeek, period);
        }
        else if (period.Equals("day"))
        {
            List<int> days = new List<int>();

            foreach (Transactions expense in expenses)
            {
                int day = expense.Date.Day;
                if (!days.Contains(day))
                {
                    days.Add(day);
                }
            }

            Console.WriteLine("Days with expenses:");
            foreach (int day in days)
            {
                Console.WriteLine(day);
                Console.WriteLine();
            }

            int selectedDay;
            while (true)
            {
                Console.Write("Please select a day (1-31): ");
                if (
                    int.TryParse(Console.ReadLine(), out selectedDay)
                    && selectedDay >= 1
                    && selectedDay <= 31
                    && days.Contains(selectedDay)
                )
                {
                    break;
                }
                Console.WriteLine(
                    "Invalid day or no expenses found for this day. Please try again."
                );
            }

            Console.Clear();
            DisplayTransactionsForPeriod(expenses, selectedDay, period);
        }
        else
        {
            Console.WriteLine("Invalid choice. Please select 'year', 'month', 'week', or 'day'.");
        }
    }

    private static void DisplayTransactionsForPeriod(
        List<Transactions> transactions,
        int selectedValue,
        string period
    )
    {
        Console.WriteLine($"Transactions for {period} {selectedValue}:");
        foreach (Transactions transaction in transactions)
        {
            bool match = false;
            if (period.Equals("year") && transaction.Date.Year == selectedValue)
            {
                match = true;
            }
            else if (period.Equals("month") && transaction.Date.Month == selectedValue)
            {
                match = true;
            }
            else if (period.Equals("week"))
            {
                Calendar calendar = CultureInfo.CurrentCulture.Calendar;
                int week = calendar.GetWeekOfYear(
                    transaction.Date,
                    CalendarWeekRule.FirstFourDayWeek,
                    DayOfWeek.Monday
                );
                if (week == selectedValue)
                {
                    match = true;
                }
            }
            else if (period.Equals("day") && transaction.Date.Day == selectedValue)
            {
                match = true;
            }

            if (match)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
