using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FunnyNet.Authentication
{
    public static class GuestToken
    {
        public static string Create()
        {
            Span<char> guid = stackalloc char[36];
            Guid.NewGuid().TryFormat(guid, out _, "D");
            Span<char> guidHex = stackalloc char[72];
            Span<byte> guidBytes = stackalloc byte[100];

            for (int i = 0; i < guid.Length; i++)
                ((int)guid[i]).TryFormat(guidHex.Slice(i * 2), out _, "X2");

            Encoding.ASCII.GetBytes(guidHex, guidBytes);
            Encoding.ASCII.GetBytes(":MsOIJ39Q28:PTDc3H8a)Vi=UYap", guidBytes.Slice(72));

            using SHA1 sha = new SHA1CryptoServiceProvider();
            Span<byte> hash = stackalloc byte[20];
            sha.TryComputeHash(guidBytes, hash, out _);

            Span<char> hashHex = stackalloc char[40];
            for (int i = 0; i < hash.Length; i++)
                hash[i].TryFormat(hashHex.Slice(i * 2), out _, "x2");

            Span<byte> result = stackalloc byte[124];
            guidBytes.Slice(0, 72).CopyTo(result);
            Encoding.ASCII.GetBytes("_MsOIJ39Q28:", result.Slice(72));
            Encoding.ASCII.GetBytes(hashHex, result.Slice(84));

            return Convert.ToBase64String(result);
        }
    }
}
