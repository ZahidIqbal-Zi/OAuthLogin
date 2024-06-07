using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OAuthLoginApi
{
    public class APIAuthorization : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.CompletedTask;
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            if (context.UserName == "admin" && context.Password == "admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "player"));
                identity.AddClaim(new Claim("scope", "b_game vip_character_personalize"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Admin"));
                context.Validated(identity);
            }
            else if (context.UserName == "player" && context.Password == "player")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "player"));
                identity.AddClaim(new Claim("scope", "b_game"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Player"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Invalid_Credentials", "Please Provide Valid Username Or Password");
                 return Task.CompletedTask; 
            }
            return Task.CompletedTask;
        }
    }
}