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
    public partial class EncryptionDecryptionController : ControllerBase
    {
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
    }
}
