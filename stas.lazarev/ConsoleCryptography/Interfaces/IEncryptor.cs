

namespace ConsoleCryptography.Interfaces
{
    interface IEncryptor
    {
        string Encrypt(string text, string key);
    }
}
