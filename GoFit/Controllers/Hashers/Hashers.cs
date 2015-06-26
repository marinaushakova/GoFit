using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GoFit.Controllers.Hashers
{
    public class Hashers
    {
        /// <summary>
        /// Hashes the given string
        /// </summary>
        /// <param name="data">The string to hash</param>
        /// <returns>The hashed string</returns>
        private static string HashData(string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                throw new Exception("HashData can't be empty string or null");
            }

            SHA256 hasher = SHA256Managed.Create();
            byte[] hashedData = hasher.ComputeHash(Encoding.Unicode.GetBytes(data));

            StringBuilder sb = new StringBuilder(hashedData.Length * 2);
            foreach (byte b in hashedData)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Hashes the user login credentials
        /// </summary>
        /// <param name="userName">The username to hash the password with</param>
        /// <param name="password">The password to hash</param>
        /// <returns>the hashed password</returns>
        public static string HashPassword(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password can't be empty string or null");
            }
            string hashedString = HashData(String.Format("{0}{1}", userName.Substring(0, 4), password));
            return hashedString;
        }

    }
}