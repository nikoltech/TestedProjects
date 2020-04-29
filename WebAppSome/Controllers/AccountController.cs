namespace WebAppSome.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using WebAppSome.Entities;
    using WebAppSome.Interfaces;
    using WebAppSome.Models;

    public class AccountController : Controller
    {
        private readonly IRepository repo;

        public AccountController(IRepository repository)
        {
            this.repo = repository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
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

        #region private methods
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
