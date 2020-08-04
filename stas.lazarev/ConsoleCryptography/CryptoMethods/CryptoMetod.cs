using ConsoleCryptography.Interfaces;

namespace ConsoleCryptography.CryptoMethods
{
    abstract class CryptoMetod : IEncryptor, IDecryptor
    {
        abstract public string Encrypt(string text, string key);

        abstract public string Decrypt(string text, string key);
    }
}
