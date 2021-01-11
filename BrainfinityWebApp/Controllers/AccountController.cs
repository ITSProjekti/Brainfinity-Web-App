using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BrainfinityWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BrainfinityWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _client;

        public object TextEncodings { get; private set; }

        public AccountController(IHttpClientFactory client)
        {
            _client = client;
        }

        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginForm(Korisnik korisnik)
        {
            var client = _client.CreateClient("takmicenje");
            var response = await client.PostAsJsonAsync<Korisnik>("account/login", korisnik);
            var r = await response.Content.ReadAsStringAsync();

            //if (!string.IsNullOrWhiteSpace(r))
            //{
            //var tokenValue = JsonConvert.DeserializeObject<Token>(r);
            ////HttpContext.Session.SetString("token", t.TokenName);

            ////var s = HttpContext.Session.GetString("token");
            //var handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadToken(tokenValue.TokenName) as JwtSecurityToken;
            //var role = token.Claims.First(m => m.Type == new IdentityOptions().ClaimsIdentity.RoleClaimType).Value;

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, korisnik.Username),
            //    new Claim(ClaimTypes.Role, role),
            //    new Claim("token", tokenValue.TokenName)
            //};

            //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //var authProperties = new AuthenticationProperties
            //{
            //    IsPersistent = true,
            //    RedirectUri = "http://localhost:44376/account/login"
            //};

            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            if (response.IsSuccessStatusCode)
            {
                await LoginKorisnik(r, korisnik);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login");
            //}

            //return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Login");
        }

        public IActionResult Register()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterForm(Korisnik korisnik)
        {
            var client = _client.CreateClient("takmicenje");

            if (HttpContext.Request.Form.Files != null)
            {
                var files = HttpContext.Request.Form.Files;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var fs = file.OpenReadStream())
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            if (file.Name == "slika")
                            {
                                korisnik.Slika = fileBytes;
                            }
                            else if (file.Name == "logo")
                            {
                                korisnik.Logo = fileBytes;
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(korisnik.KorisnickiTip))
            {
                korisnik.KorisnickiTip = "Tim";
            }

            var response = await client.PostAsJsonAsync<Korisnik>("account/register", korisnik);
            var r = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await LoginKorisnik(r, korisnik);
                return RedirectToAction("Login");
            }

            return RedirectToAction("Register");
        }

        [Authorize]
        public async Task<string> Korisnik()
        {
            var username = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Name).Value;
            var client = _client.CreateClient("takmicenje");
            var request = new HttpRequestMessage(HttpMethod.Get, "account/" + username);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var korisnik = await response.Content.ReadAsByteArrayAsync();
                var img = Base64UrlEncoder.Decode(Convert.ToBase64String(korisnik));
                img = img.Trim('"');
                return img;
            }

            return null;
        }

        public async Task LoginKorisnik(string response, Korisnik korisnik)
        {
            if (!string.IsNullOrWhiteSpace(response))
            {
                var tokenValue = JsonConvert.DeserializeObject<Token>(response);
                //HttpContext.Session.SetString("token", t.TokenName);

                //var s = HttpContext.Session.GetString("token");
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadToken(tokenValue.TokenName) as JwtSecurityToken;
                var role = token.Claims.First(m => m.Type == new IdentityOptions().ClaimsIdentity.RoleClaimType).Value;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, korisnik.Username),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("token", tokenValue.TokenName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                    //RedirectUri = "http://localhost:44376/account/login"
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            }
        }
    }
}