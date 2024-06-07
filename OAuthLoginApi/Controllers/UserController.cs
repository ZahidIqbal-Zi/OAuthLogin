using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace OAuthLoginApi.Controllers
{
    public class UserController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/forall")]
        public IHttpActionResult Get()
        {
            return Ok("Now server time is: " + DateTime.Now.ToString());
        }

        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name + ". You Can Access The Following Systems: " + identity.Claims.FirstOrDefault(c => c.Type == "scope")?.Value);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);

            return Ok("Hello " + identity.Name + ". Your Roles Are : " + string.Join(",", roles.ToList()) + ". You Can Access The Following Systems: " + identity.Claims.FirstOrDefault(c => c.Type == "scope")?.Value);
        }
    }
}
