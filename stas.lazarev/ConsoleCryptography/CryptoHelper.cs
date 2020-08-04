using System.Text;

namespace ConsoleCryptography
{
    static class CryptoHelper
    {
        /// <summary>
        /// Returns a key that is the same length as the text that is to be encrypted/decrypted.
        /// </summary>
        public static string ExtendKeyToTextLength(int textLength, string key)
        {
            if (key.Length == textLength)
            {
                return key;
            }
            else if (key.Length > textLength)
            {
                return key.Substring(0, textLength);
            }

            // then we need to extend the key to be of the same length as the text
            var resultKeyBuilder = new StringBuilder();

            int keyIndex = 0;
            while (resultKeyBuilder.Length < textLength)
            {
                resultKeyBuilder.Append(key[keyIndex]);
                keyIndex++;

                if (keyIndex == key.Length)
                    keyIndex = 0;
            }

            return resultKeyBuilder.ToString();
        }
    }
}
