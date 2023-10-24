using Microsoft.AspNetCore.DataProtection;

namespace DataProtection
{
    public class DataProtectionService : IDataProtectionService
    {
        private IDataProtector _protector { get; set; }

        public DataProtectionService(IDataProtector protector)
        {
            _protector = protector;
        }

        public string Decrypt(string cipher)
        {
            return _protector.Unprotect(cipher);
        }

        public string Encrypt(string input)
        {
            return _protector.Protect(input);
        }
    }
}
