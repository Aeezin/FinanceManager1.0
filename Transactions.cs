public class Transactions
{
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; private set; }
    public string Description { get; set; }


    public Transactions(decimal amount, string description, TransactionType type)
    {
        Date = DateTime.Now;
        Amount = amount;
        Description = description;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} - {Type} - {Amount} - {Description}";
    }
}

public enum TransactionType
{
    Income,
    Expense
}

public class Income : Transactions
{
    public Income(decimal amount, string description) : base(amount, description, TransactionType.Income)
    {
    }
}

public class Expense : Transactions
{
    public Expense(decimal amount, string description) : base(amount, description, TransactionType.Expense)
    {
    }
}


