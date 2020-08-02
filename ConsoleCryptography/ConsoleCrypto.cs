using System;
using System.Text;
using ConsoleCryptography.CryptoMethods;

namespace ConsoleCryptography
{
    class ConsoleCrypto
    {
        UserActionChoice ActionChoice { get; set; }
        UserCryptoMethodChoice CryptoMethodChoice { get; set; }
        CryptoMetod CryptoMetod { get; set; }

        string UserText { get; set; }
        string UserKey { get; set; }

        /// <summary>
        /// Method that runs the whole encryption/decryption process
        /// </summary>
        public void Run()
        {
            int actionsCount = Enum.GetNames(typeof(UserActionChoice)).Length;
            int cryptoMethodsCount = Enum.GetNames(typeof(UserCryptoMethodChoice)).Length;

            do
            {
                // choose an action (encrypt/decrypt)
                PrintChooseActionMsg();
                SelectAction( GetUserMenuChoice(actionsCount) );

                // choose crypto method
                PrintChooseCryptoMethodMsg();
                SelectCryptoMethod( GetUserMenuChoice(cryptoMethodsCount) );

                // enter text and key
                SetUserData();

                // run the process
                RunCryptoProcess();

            } while (true);
        }

        /// <summary>
        /// Prints 2 options for user to choose from (encrypt and decrypt)
        /// </summary>
        void PrintChooseActionMsg()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Choose an action:");

            string[] actionNames = Enum.GetNames(typeof(UserActionChoice));
            int counter = 1; // counting menu items starting with 1
            foreach (var actionName in actionNames)
            {
                stringBuilder.AppendLine($"  {counter}. {actionName}");
                counter++;
            }

            Console.Clear();
            Console.WriteLine(stringBuilder.ToString());
        }

        /// <summary>
        /// Makes user to reenter the input console string until it's valid (matches one of the menu item's number)
        /// </summary>
        int GetUserMenuChoice(int menuOptionsCount)
        {
            string userInput;
            int userChoice;

            bool inputIsInvalid;
            do
            {
                userInput = Console.ReadLine();

                inputIsInvalid = int.TryParse(userInput, out userChoice) == false;

                if (!inputIsInvalid)
                {
                    if (userChoice < 1 || userChoice > menuOptionsCount)
                    {
                        inputIsInvalid = true;
                    }
                    else
                    {
                        inputIsInvalid = false;
                    }
                }
            } while (inputIsInvalid);

            return userChoice;
        }

        /// <summary>
        /// Sets the corresponding action based on the menu item number chosen by user
        /// </summary>
        void SelectAction(int menuChoice)
        {
            switch (menuChoice)
            {
                case 1:
                    ActionChoice = UserActionChoice.Encrypt;
                    break;
                case 2:
                    ActionChoice = UserActionChoice.Decrypt;
                    break;
            }
        }

        /// <summary>
        /// Prints 3 crypto methods for user to choose from (Caesar, Vigenere, XOR)
        /// </summary>
        void PrintChooseCryptoMethodMsg()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Choose a method:");

            string[] cryptoMethodNames = Enum.GetNames(typeof(UserCryptoMethodChoice));
            int counter = 1; // counting menu items starting with 1
            foreach (var methodName in cryptoMethodNames)
            {
                stringBuilder.AppendLine($"  {counter}. {methodName} cipher");
                counter++;
            }

            Console.Clear();
            Console.WriteLine(stringBuilder.ToString());
        }

        /// <summary>
        /// Sets the chosen crypto method based on the menu item number chosen by user
        /// </summary>
        void SelectCryptoMethod(int menuChoice)
        {
            switch (menuChoice)
            {
                case 1:
                    CryptoMethodChoice = UserCryptoMethodChoice.Caesar;
                    break;
                case 2:
                    CryptoMethodChoice = UserCryptoMethodChoice.XOR;
                    break;
                case 3:
                    CryptoMethodChoice = UserCryptoMethodChoice.Vigenere;
                    break;
                default:
                    CryptoMethodChoice = UserCryptoMethodChoice.Caesar;
                    break;
            }
        }

        /// <summary>
        /// Makes user enter Text and Key values
        /// </summary>
        void SetUserData()
        {
            string action = ActionChoice == UserActionChoice.Encrypt ? "encryprion" : "decryption";

            string message = $"Enter text for {action}:";
            Console.Clear();
            Console.WriteLine(message);
            UserText = Console.ReadLine();

            message = $"Enter key for {action}:";
            Console.Clear();
            Console.WriteLine(message);
            UserKey = Console.ReadLine();
        }

        /// <summary>
        /// Creates an instance of a chosen crypto method class and calls their encryption/decryption method
        /// </summary>
        void RunCryptoProcess()
        {
            string result = string.Empty;

            switch (CryptoMethodChoice)
            {
                case UserCryptoMethodChoice.Caesar:
                    CryptoMetod = new CaesarMethod(UserText);
                    break;
                case UserCryptoMethodChoice.Vigenere:
                    CryptoMetod = new VigenereMethod(UserText, UserKey);
                    break;
                case UserCryptoMethodChoice.XOR:
                    CryptoMetod = new XorMethod(UserText, UserKey);
                    break;
                default:
                    CryptoMetod = new CaesarMethod(UserText);
                    break;
            }

            switch (ActionChoice)
            {
                case UserActionChoice.Encrypt:
                    result = CryptoMetod.Encrypt();
                    break;
                case UserActionChoice.Decrypt:
                    result = CryptoMetod.Decrypt();
                    break;
            }

            Console.WriteLine("Result:");
            Console.WriteLine(result);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        enum UserCryptoMethodChoice
        {
            Caesar,
            Vigenere,
            XOR
        }

        enum UserActionChoice
        {
            Encrypt,
            Decrypt
        }
    }
}
