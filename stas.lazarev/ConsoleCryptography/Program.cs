using System;

namespace ConsoleCryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Legit Cryptography App";

            ConsoleCrypto consoleCrypto = new ConsoleCrypto();
            consoleCrypto.Run();
        }
    }
}
