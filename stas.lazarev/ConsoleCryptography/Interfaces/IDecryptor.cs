

namespace ConsoleCryptography.Interfaces
{
    interface IDecryptor
    {
        string Decrypt(string text, string key);
    }
}
