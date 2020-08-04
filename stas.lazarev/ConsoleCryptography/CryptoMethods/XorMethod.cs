using System.Text;

namespace ConsoleCryptography.CryptoMethods
{
    class XorMethod : CryptoMetod
    {
        public override string Encrypt(string text, string key) => EncryptDecrypt(text, key);

        public override string Decrypt(string text, string key) => EncryptDecrypt(text, key);

        string EncryptDecrypt(string text, string key)
        {
            var resultTextBuilder = new StringBuilder();
            string readyKey = CryptoHelper.ExtendKeyToTextLength(text.Length, key);

            for (int i = 0; i < text.Length; i++)
            {
                resultTextBuilder.Append( (char)(readyKey[i] ^ text[i]) );
            }

            return resultTextBuilder.ToString();
        }
    }
}
