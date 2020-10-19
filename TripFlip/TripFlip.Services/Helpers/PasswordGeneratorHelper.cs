using System;
using System.Text;

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
            StringBuilder charSetBuilder = new StringBuilder();
            Random random = new Random();
            int counter;

            if (useLowercase)
            {
                charSetBuilder.Append(LowerCase);
            }

            if (useUppercase)
            {
                charSetBuilder.Append(UpperCase);
            }

            if (useNumbers)
            {
                charSetBuilder.Append(Numbers);
            }

            if (useSpecial)
            {
                charSetBuilder.Append(Specials);
            }

            var charSet = charSetBuilder.ToString();

            for (counter = 0; counter < passwordSize; counter++)
            {
                password[counter] = charSet[random.Next(charSet.Length - 1)];
            }
            
            var passwordAsString = new string(password);

            return passwordAsString;
        }
    }
}

