using ConsoleCryptography.Enums;
using System.Text;

namespace ConsoleCryptography.CryptoMethods
{
    class CaesarMethod : CryptoMetod
    {
        const int DefaultCaesarShiftValue = 3;

        int _characterShiftValue;
        CharacterShiftDirection _shiftDirection;

        public CaesarMethod(int characterShiftValue = DefaultCaesarShiftValue,
            CharacterShiftDirection direction = CharacterShiftDirection.Right)
        {
            _characterShiftValue = characterShiftValue;
            _shiftDirection = direction;
        }

        public override string Encrypt(string text, string key) =>
            EncryptDecrypt(text, key, UserActionChoice.Encrypt);

        public override string Decrypt(string text, string key) =>
            EncryptDecrypt(text, key, UserActionChoice.Decrypt);

        /// <param name="characterShiftValue">In the context of this crypto-method class, the user-given key is
        /// the value of characters it's going to shift to encrypt/decrypt data.</param>
        string EncryptDecrypt(string text,
            string characterShiftValue,
            UserActionChoice actionChoice)
        {
            SetValidKey(characterShiftValue);

            if (actionChoice == UserActionChoice.Decrypt)
            {
                _characterShiftValue *= -1;
            }

            var resultTextBuilder = new StringBuilder();
            foreach (var character in text)
            {
                resultTextBuilder.Append( (char)(character + _characterShiftValue) );
            }

            return resultTextBuilder.ToString(); 
        }

        /// <summary>
        /// Sets the user-given key as a characterShiftValue, and then changes it to a positive or negative
        /// value, depending on the chosen character shift direction.
        /// If user-given key is not a positive integer - it sets the default shiftValue.
        /// </summary>
        void SetValidKey(string userKey)
        {
            bool userKeyIsInteger = int.TryParse(userKey, out _characterShiftValue);
            bool keyIsPositive = _characterShiftValue > 0;

            if ( !(userKeyIsInteger && keyIsPositive) )
            {
                _characterShiftValue = DefaultCaesarShiftValue;
            }

            if (_shiftDirection == CharacterShiftDirection.Left)
            {
                _characterShiftValue *= -1;
            }
        }
    }
}
