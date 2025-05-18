using System.Text;

namespace PaymentTransactionSample.Extensions
{
    public static class CustomStringExtension
    {
        public static string ToBase64(this string input)
        {
           return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }
        public static string ToSha256(this string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hashBytes = sha256.ComputeHash(bytes);

                var sb = new System.Text.StringBuilder();
                foreach (var b in hashBytes)
                    sb.Append(b.ToString("x2"));
                Console.WriteLine(sb.ToString());
                return sb.ToString();
            }
        }
    }
}
