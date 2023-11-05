namespace DataProtection
{
    public interface IDataProtectionService
    {
        public string Encrypt(string input);
        public string Decrypt(string cipher);
        public string TimeLimited_Encrypt(string input, TimeSpan lifetime);
        public string TimeLimited_Decrypt(string cipher);
    }
}
