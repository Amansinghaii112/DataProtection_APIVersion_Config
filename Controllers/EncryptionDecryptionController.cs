using DataProtection_APIVersion_Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DataProtection.Controllers
{
    /// <summary>
    /// "URL-based versioning has been applied in this context. 
    /// To implement route-based versioning, consider using the pattern "[Route("api/v{version:apiVersion}/[controller]")]" 
    /// Ensure that the options.ApiVersionReader is configured accordingly."
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class EncryptionDecryptionController : ControllerBase
    {
        private IDataProtectionService _dataProtectionService { get; }
        public IOptions<FormatSettings> _options { get; }

        public EncryptionDecryptionController(IDataProtectionService dataProtectionService, IOptions<FormatSettings> _option)
        {
            _dataProtectionService = dataProtectionService;
            _options = _option;
            int num = _options.Value.Number.Precision;
        }

        [HttpGet("Encrypt/{input}")]
        public ActionResult<string> Encrypt([FromRoute] string input)
        {
            return _dataProtectionService.Encrypt(input);
        }

        /// <summary>
        /// URL based versioning - http://localhost:5086/api/EncryptionDecryption/Encrypt/Singhai?api-version=2.0
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [ApiVersion("2.0")]
        [HttpGet("Encrypt/{input}")]
        public ActionResult<string> EncryptV2([FromRoute] string input)
        {
            return _dataProtectionService.Encrypt(input);
        }

        [HttpGet("Decrypt/{cipher}")]

        public ActionResult<string> Decrypt([FromRoute] string cipher)
        {
            return _dataProtectionService.Decrypt(cipher);
        }
    }
}
