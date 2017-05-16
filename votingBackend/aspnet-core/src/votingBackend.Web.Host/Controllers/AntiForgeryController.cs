using votingBackend.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace votingBackend.Web.Host.Controllers
{
    public class AntiForgeryController : votingBackendControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}