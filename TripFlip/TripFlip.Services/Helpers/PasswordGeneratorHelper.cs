using System;

namespace TripFlip.Services.Helpers
{
    public static class PasswordGeneratorHelper
    {
        private static readonly string LowerCase = "abcdefghijklmnopqursuvwxyz";
        private static readonly string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string Numbers = "123456789";
        private static readonly string Specials = @"!@£$%^&*()#€";

        /// <summary>
        /// Generates random password, use of symbols noted in parameters is not guaranteed.
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
            char[] password = new char[passwordSize];
            string charSet = string.Empty;
            Random random = new Random();
            int counter;

            if (useLowercase)
            {
                charSet += LowerCase;
            }

            if (useUppercase)
            {
                charSet += UpperCase;
            }

            if (useNumbers)
            {
                charSet += Numbers;
            }

            if (useSpecial)
            {
                charSet += Specials;
            }

            for (counter = 0; counter < passwordSize; counter++)
            {
                password[counter] = charSet[random.Next(charSet.Length - 1)];
            }

            return String.Join(null, password);
        }
    }
}

