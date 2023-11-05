using DataProtection_APIVersion_Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Http;
using System;

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
    public partial class EncryptionDecryptionController : ControllerBase
    {
        private IDataProtectionService _dataProtectionService { get; }
        public IOptions<FormatSettings> _options { get; }

        public EncryptionDecryptionController(IApiVersionReader apiVersion, IHttpContextAccessor httpContextAccessor, IDataProtectionService dataProtectionService, IOptions<FormatSettings> _option)
        {
            _dataProtectionService = dataProtectionService;
            _options = _option;
            int num = _options.Value.Number.Precision;
            string var1 = httpContextAccessor.HttpContext.Request.Query["api-version"].ToString();
            string var2 = httpContextAccessor.HttpContext.Request.Headers["api-version"].ToString();
        }

        [HttpGet("Encrypt/{input}")]
        public ActionResult<string> Encrypt([FromRoute] string input)
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
