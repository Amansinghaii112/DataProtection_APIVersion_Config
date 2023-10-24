namespace DataProtection
{
    public interface IDataProtectionService
    {
        public string Encrypt(string input);
        public string Decrypt(string cipher);
    }
}
