namespace Core
{
    public interface ICipher
    {
        int Encrypt(int message, int key);
        int Decrypt(int crypt, int key);
    }
}
