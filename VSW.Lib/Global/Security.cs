using System;
using System.Security.Cryptography;
using System.Text;

namespace VSW.Lib.Global
{
    public class Security
    {
        public static string MD5(string Str)
        {
            Byte[] _Bytes = new UnicodeEncoding().GetBytes(Str);
            Byte[] _HasBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(_Bytes);
            return BitConverter.ToString(_HasBytes);
        }
    }
}
