using ConsoleCryptography.Interfaces;

namespace ConsoleCryptography.CryptoMethods
{
    abstract class CryptoMetod : IEncryptor, IDecryptor
    {
        public string Key { get; set; }
        public string Text { get; set; }

        public CryptoMetod(string text, string key)
        {
            Key = key;
            Text = text;
        }

        /// <summary>
        /// Returns a key that is the same length as the text that is to be encrypted/decrypted
        /// </summary>
        /// <param name="TextLength">A length of a text to be encrypted/decrypted</param>
        protected string GetReadyKey()
        {
            if (Key.Length == Text.Length)
                return Key;
            else if (Key.Length > Text.Length)
                return Key.Substring(0, Text.Length);

            // then we need to extend the key to be of the same length as the text
            string resultKey = string.Empty;

            int keyIndex = 0;
            while (resultKey.Length < Text.Length)
            {
                resultKey += Key[keyIndex];
                keyIndex++;

                if (keyIndex == Key.Length)
                    keyIndex = 0;
            }

            return resultKey;
        }

        abstract public string Encrypt();

        abstract public string Decrypt();
    }
}
