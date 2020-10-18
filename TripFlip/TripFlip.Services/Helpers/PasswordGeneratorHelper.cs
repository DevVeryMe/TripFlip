using System;

namespace TripFlip.Services.Helpers
{
    public static class PasswordGeneratorHelper
    {
        const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        const string UPPER_CASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBERS = "123456789";
        const string SPECIALS = @"!@£$%^&*()#€";

        /// <summary>
        /// Generates random password.
        /// </summary>
        /// <param name="useLowercase">Parameter to note a use of lower case letters.</param>
        /// <param name="useUppercase">Parameter to note a use of upper case letters.</param>
        /// <param name="useNumbers">Parameter to note a use of numbers.</param>
        /// <param name="useSpecial">Parameter to note a use of special symbols.</param>
        /// <param name="passwordSize">Size of password.</param>
        /// <returns>Generated password.</returns>
        public static string GeneratePassword(bool useLowercase, bool useUppercase,
            bool useNumbers, bool useSpecial, int passwordSize)
        {
            char[] _password = new char[passwordSize];
            string charSet = ""; // Initialise to blank
            Random _random = new Random();
            int counter;

            // Build up the character set to choose from
            if (useLowercase) charSet += LOWER_CASE;

            if (useUppercase) charSet += UPPER_CASE;

            if (useNumbers) charSet += NUMBERS;

            if (useSpecial) charSet += SPECIALS;

            for (counter = 0; counter < passwordSize; counter++)
            {
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }

            return String.Join(null, _password);
        }
    }
}

