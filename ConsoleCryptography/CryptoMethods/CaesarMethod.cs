using System;

namespace ConsoleCryptography.CryptoMethods
{
    class CaesarMethod : CryptoMetod
    {
        public int CharacterShiftValue { get; set; }
        public CharacterShiftDirection ShiftDirection { get; set; }

        public CaesarMethod(string text,
            int characterShiftValue = 3,
            CharacterShiftDirection direction = CharacterShiftDirection.Right,
            string key = "")
            : base(text, key)
        {
            CharacterShiftValue = characterShiftValue;
            ShiftDirection = direction;
        }

        public override string Encrypt()
        {
            string encryptedText = string.Empty;

            if (ShiftDirection == CharacterShiftDirection.Right)
            {
                foreach (var character in Text)
                {
                    encryptedText += (char)(character + CharacterShiftValue);
                }
            }
            else
            {
                foreach (var character in Text)
                {
                    encryptedText += (char)(character - CharacterShiftValue);
                }
            }

            return encryptedText;
        }

        public override string Decrypt()
        {
            string decryptedText = string.Empty;

            if (ShiftDirection == CharacterShiftDirection.Right)
            {
                foreach (var character in Text)
                {
                    decryptedText += (char)(character - CharacterShiftValue);
                }
            }
            else
            {
                foreach (var character in Text)
                {
                    decryptedText += (char)(character + CharacterShiftValue);
                }
            }

            return decryptedText;
        }

        public enum CharacterShiftDirection
        {
            Left,
            Right
        }
    }
}
