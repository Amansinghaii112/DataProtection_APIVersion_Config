using Microsoft.AspNetCore.DataProtection;

namespace DataProtection
{
    public class DataProtectionService : IDataProtectionService
    {
        private IDataProtector _protector { get; set; }
        private ITimeLimitedDataProtector _timeLimitedProtector { get; set; }

        public DataProtectionService(IDataProtector protector)
        {
            _protector = protector;
            _timeLimitedProtector = _protector.ToTimeLimitedDataProtector();
        }

        public string Decrypt(string cipher)
        {
            return _protector.Unprotect(cipher);
        }

        public string Encrypt(string input)
        {
            return _protector.Protect(input);
        }

        public string TimeLimited_Decrypt(string cipher)
        {
            return _timeLimitedProtector.Unprotect(cipher);
        }

        public string TimeLimited_Encrypt(string input, TimeSpan lifetime)
        {
            return _timeLimitedProtector.Protect(input, lifetime); //TimeSpan.FromSeconds(20)
        }

    }
}
