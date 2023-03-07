using System.Security.Cryptography;
using System.Text;

namespace JobApplicationManagement.Utils
{
    public class HashUtil
    {
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public static bool IsValid(string? str, string? hashStr)
        {
            if (str == null || hashStr == null)
            {
                return false;
            }
            return Object.Equals(hashStr, GetMD5(str));
        }
    }
}
