using System.Text;
using ConsoleCryptography.Enums;

namespace ConsoleCryptography.CryptoMethods
{
    class VigenereMethod : CryptoMetod
    {
        readonly int _characterCount = 128; // ASCII character count

        public override string Encrypt(string text, string key)
            => EncryptDecrypt(text, key, UserActionChoice.Encrypt);

        public override string Decrypt(string text, string key)
            => EncryptDecrypt(text, key, UserActionChoice.Decrypt);

        string EncryptDecrypt(string text,
            string key,
            UserActionChoice actionChoice)
        {
            var resultTextBuilder = new StringBuilder();
            string readyKey = CryptoHelper.ExtendKeyToTextLength(text.Length, key);

            for (int i = 0; i < text.Length; i++)
            {
                int keyCharCode = readyKey[i];
                int textCharCode = text[i];

                if (actionChoice == UserActionChoice.Decrypt)
                {
                    keyCharCode *= -1;
                }

                // According to Vigenere encryption/decryption formula
                int characterCode = (textCharCode + _characterCount + keyCharCode) % _characterCount;
                resultTextBuilder.Append( (char)characterCode );
            }

            return resultTextBuilder.ToString();
        }
    }
}
