using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RaythosAerospace.CustomServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RaythosAerospace.Controllers
{
    public class JWTController : Controller
    {

        private readonly ICustomService _customservice;

        public JWTController(ICustomService customservice)
        {
            _customservice = customservice;
        }
        public IActionResult Index()
        {
            return View();
        }

        public enum TokenResult
        {
            Success,
            TokenNotFound,
            TokenExpired
        }


        public void AttachToken(string token, IResponseCookies Cookies)
        {
            Cookies.Append("JWT", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1) // Cookie expiration time
            });
        }


        //getting the user from the request coookie token
        public string GetLoggedUser(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);


            var userIdClaim = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                string userId = userIdClaim.Value as string;
                // Use the userName or other claims as needed
                return userId;
            }

            return string.Empty;
        }

        //assigning the token to the cookie
        public string AssignToken(string email)
        {

            if (string.IsNullOrEmpty(email))
            {
                return string.Empty;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_customservice.GetJWTSettings().Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var payload = new JwtPayload(email, audience: null, claims: claims, notBefore: null, expires: DateTime.Now.AddHours(4));
            var header = new JwtHeader(creds);

            var token = new JwtSecurityToken(header:header,payload:payload);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        //gets the validation result and redirects the request to relevant controller
        public IActionResult HasErrorsInValidationResult(TokenResult result)
        {
            if (result == TokenResult.Success)
            {
                return null;
            }
            else if (result == TokenResult.TokenExpired)
            {
                return RedirectToAction("TokenExpiredError", "Error");
            }
            else
            {
                return RedirectToAction("TokenExpiredError", "Error");
            }
        }

        //validate the jwt token
        public TokenResult ValidateJWTToken(string accessToken)
        {


            if (!string.IsNullOrEmpty(accessToken))
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(accessToken);

                var expirationTime = token.ValidTo;

                if (expirationTime < DateTime.UtcNow)
                {
                   
                    return TokenResult.TokenExpired; 
                }

                return TokenResult.Success;


            }
            else
            {
                
                return TokenResult.TokenNotFound;
            }

            
        }
    }
}
