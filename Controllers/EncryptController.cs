using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncryptController : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Encrypt([FromBody] EncryptionRequest request)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(request.PublicKey), out _);

            var data = Encoding.UTF8.GetBytes(request.PlainText);
            var encryptedData = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);

            return Convert.ToBase64String(encryptedData);
        }
    }

    public class EncryptionRequest
    {
        public string PublicKey { get; set; }
        public string PlainText { get; set; }
    }
}
