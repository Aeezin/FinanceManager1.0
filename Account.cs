using Npgsql;

public class Account
{
    public static void CreateAccount()
    {
        NpgsqlConnection shoffConnection = new NpgsqlConnection(DatabaseConnection.GetConnectionString());
        shoffConnection.Open();
    }

    public static void Login()
    {

    }

    public static void Logout()
    {

    }
}