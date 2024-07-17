using System.Security.Cryptography;
using System.Text;

namespace API.Helpers
{
    public class ZaloPayHelper
    {
        public static string CreateHMACSHA256Signature(string data, string key)
        {
            using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
            }
        }
    }

}

