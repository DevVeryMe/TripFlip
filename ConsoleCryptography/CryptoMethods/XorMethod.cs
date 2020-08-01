using System;
using ConsoleCryptography.Interfaces;

namespace ConsoleCryptography.CryptoMethods
{
    class XorMethod : CryptoMetod, IEncryptor, IDecryptor
    {
        public XorMethod(string text, string key)
            : base(text, key)
        {

        }

        public string Encrypt() => EncryptDecrypt();

        public string Decrypt() => EncryptDecrypt();

        string EncryptDecrypt()
        {
            string resultText = string.Empty;
            string readyKey = GetReadyKey();

            for (int i = 0; i < Text.Length; i++)
            {
                resultText += (char)(readyKey[i] ^ Text[i]);
            }

            return resultText;
        }
    }
}
