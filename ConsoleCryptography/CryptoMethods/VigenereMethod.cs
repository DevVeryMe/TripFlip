using System;
using ConsoleCryptography.Interfaces;

namespace ConsoleCryptography.CryptoMethods
{
    class VigenereMethod : CryptoMetod, IEncryptor, IDecryptor
    {
        readonly int _characterCount = 128; // ASCII character count

        public VigenereMethod(string text, string key)
            : base(text, key)
        {

        }

        public string Encrypt()
        {
            string encryptedText = string.Empty;
            string encryptionReadyKey = GetReadyKey();

            for (int i = 0; i < Text.Length; i++)
            {
                int keyCharCode = encryptionReadyKey[i];
                int textCharCode = Text[i];

                // According to Vigenere encryption formula
                encryptedText += (char)((keyCharCode + textCharCode) % _characterCount);
            }

            return encryptedText;
        }

        public string Decrypt()
        {
            string decryptedText = string.Empty;
            string decryptionReadyKey = GetReadyKey();

            for (int i = 0; i < Text.Length; i++)
            {
                int keyCharCode = decryptionReadyKey[i];
                int textCharCode = Text[i];

                // According to Vigenere decryption formula
                decryptedText += (char)((textCharCode + _characterCount - keyCharCode) % _characterCount);
            }

            return decryptedText;
        }
    }
}
