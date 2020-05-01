namespace WebAppSome.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using WebAppSome.DataAccess.Entities;
    using WebAppSome.DataAccess.Repositories;
    using WebAppSome.Infrastructure;
    using WebAppSome.Models;

    public class AccountController : Controller
    {
        private readonly IRepository repo;

        public AccountController(IRepository repository)
        {
            this.repo = repository;
        }

        [HttpPost("/token")]
        public async Task<IActionResult> TokenAsync(string username, string password)
        {
            ClaimsIdentity identity = await this.GetIdentity(username, password);

            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // JWT-токен
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),

                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var responce = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(responce);
        }

        #region Old Authorization
        [HttpGet]
        public IActionResult Login()
        {
            return Redirect("~/api/Values/Index");
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    User user = await this.repo.GetUserAsync(model.Email, model.Password);
                    if (user != null)
                    {
                        await this.Authenticate(user.Email);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Something get wrong!");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    User user = await this.repo.GetUserByEmailAsync(model.Email);
                    if (user == null)
                    {
                        user = await this.repo.CreateUserAsync(model.Email, model.Password);
                        await this.Authenticate(user.Email);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь с таким Email уже существует!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Something get wrong!");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        #endregion

        #region private methods
        /// <summary>
        /// Create ClaimsIdentity from User entity
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            //Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
            User user = await this.repo.GetUserAsync(username, password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        private async Task Authenticate(string userName)
        {
            // create 1 claim
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            // install auth cookies
            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
        #endregion
    }
}
