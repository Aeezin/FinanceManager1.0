// using System.Security.Cryptography;
// using System.Text;

// public class SaltGenerating
// {
//     public static string SaltPassword()
//     {
//         byte[] saltArray = new byte[32];
//         RandomNumberGenerator.Fill(saltArray);
//         return Convert.ToBase64String(saltArray);
//     }

//     public static string HashPassword(string password, string salt)
//     {
//         using var sha256 = SHA256.Create();
//         var saltedPassword = password + salt;
//         byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
//         return Convert.ToBase64String(hashArray);
//     }
// }