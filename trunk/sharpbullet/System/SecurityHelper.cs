using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace System
{
    public class SecurityHelper
    {
        public static string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            string result = "";

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                result += data[i].ToString("x2");
            }

            // Return the hexadecimal string.
            return result;
        }
    }
}
