using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Data;
using BookStore.Areas.Login.Data;
using System.Security.Cryptography;

namespace BookStore.Areas.Login.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckLogin(mLogin mlogin)
        {
            if (mlogin.BS_USER == "admin" && mlogin.BS_PASS == "123") // Check if the username and password are correct
            {
                // Generate a secret key
                //var key = GenerateSecretKey(256); // Generates a key with a length of 256 bits (32 bytes)
                var key = "vylfAou9cDlGAdTF4IolUONGLNPYtfWTgkdU8OqfAH0=";
                // Generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, mlogin.BS_USER)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7), // Set token expiration time as desired
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Redirect to the desired controller action with the JWT token as a query parameter
                return RedirectToAction("BooksCheck", "Books", new { area = "Books", token = tokenString });
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "Login" });
            }
        }

        public string GenerateSecretKey(int keyLength)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[keyLength / 8]; // Convert bits to bytes
                rng.GetBytes(bytes);
                return Convert.ToBase64String(bytes);
            }
        }

    }
}