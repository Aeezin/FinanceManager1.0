using Npgsql;

public class AccountManager
{
    public static void Accounts()
    {
        NpgsqlConnection connection = new NpgsqlConnection(
            DatabaseConnection.GetConnectionString()
        );
        connection.Open();

        NpgsqlCommand cmd = new NpgsqlCommand(
            @"
            CREATE TABLE Account IF NOT EXIST (
            AccountID SERIAL PRIMARY KEY,
            UserID INT REFERENCES Users(UserID),
            AccountName VARCHAR(50) NOT NULL,
            Balance DECIMAL(10, 2) NOT NULL
            );

            CREATE TABLE Transactions IF NOT EXIST (
            TransactionID SERIAL PRIMARY KEY,
            AccountID INT REFERENCES Accounts(AccountID),
            Amount DECIMAL (10, 2) NOT NULL,
            Description TEXT,
            TransactionDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
            );
            "
        );
    }
}
