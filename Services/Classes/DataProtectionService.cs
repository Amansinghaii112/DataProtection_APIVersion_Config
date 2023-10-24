using Microsoft.AspNetCore.DataProtection;

namespace DataProtection
{
    public class DataProtectionService : IDataProtectionService
    {
        private IDataProtector _protector { get; set; }

        public DataProtectionService(IDataProtectionProvider provider, IConfiguration config)
        {
            string dataProtectionSecretKey = config.GetValue<string>("DataProtectionSecretKey");
            _protector = provider.CreateProtector(dataProtectionSecretKey);
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
