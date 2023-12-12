using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace WPF3.Services
{
    interface IPassHasher
    {
        public string GetSecureHash(SecureString secureString);
    }

    
    public class PassHasher : IPassHasher
    {
        public string GetSecureHash(SecureString secureString)
        {
            SHA256 sha256 = SHA256.Create();
            Span<byte> hashBytes = stackalloc byte[sha256.HashSize >> 3];
            IntPtr ptr = Marshal.SecureStringToBSTR(secureString);
            unsafe
            {
                ReadOnlySpan<byte> source = new ReadOnlySpan<byte>((void*)ptr, secureString.Length * sizeof(char));
                sha256.TryComputeHash(source, hashBytes, out _);
            }
            Marshal.ZeroFreeBSTR(ptr);
            return HashToString(hashBytes);
        }

        private string HashToString(ReadOnlySpan<byte> bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                sb.Append(bytes[i].ToString("x2"));
            return sb.ToString();
        }


    }
}
