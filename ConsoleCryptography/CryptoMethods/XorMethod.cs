using System;

namespace ConsoleCryptography.CryptoMethods
{
    class XorMethod : CryptoMetod
    {
        public XorMethod(string text, string key)
            : base(text, key)
        {

        }

        public override string Encrypt() => EncryptDecrypt();

        public override string Decrypt() => EncryptDecrypt();

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
