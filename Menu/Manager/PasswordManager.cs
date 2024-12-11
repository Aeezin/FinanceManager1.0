public class SaltGenerating
{
    public static string SaltPassword()
    {
        byte[] saltArray = new byte[16];
        using (var random = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            random.GetBytes(saltArray);
        }
        return Convert.ToBase64String(saltArray);
    }

    public static string HashPassword(string password, string salt)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var saltedPassword = password + salt;
            byte[] hashArray = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedPassword));
            return Convert.ToBase64String(hashArray);
        }
    }
}