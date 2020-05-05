namespace WebAppSome.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using WebAppSome.BusinessLogic.Services.Email;
    using WebAppSome.DataAccess.Entities;
    using WebAppSome.DataAccess.Repositories;
    using WebAppSome.Infrastructure;
    using WebAppSome.Models;

    public class AccountController : Controller
    {
        private readonly IRepository repo;
        private readonly UserManager<User> UserManager;
        private readonly SignInManager<User> SignInManager;
        private readonly EmailService EmailService;

        public AccountController(
            IRepository repository, 
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            IOptions<EmailConfig> emailConfig)
        {
            this.repo = repository;
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.EmailService = new EmailService(emailConfig.Value);
        }

        //[HttpPost("/token")]
        //public async Task<IActionResult> TokenAsync(string username, string password)
        //{
        //    ClaimsIdentity identity = await this.GetIdentity(username, password);

        //    if (identity == null)
        //    {
        //        return BadRequest(new { errorText = "Invalid username or password." });
        //    }

        //    var now = DateTime.UtcNow;
        //    // JWT-токен
        //    JwtSecurityToken jwt = new JwtSecurityToken(
        //        issuer: AuthOptions.ISSUER,
        //        audience: AuthOptions.AUDIENCE,
        //        notBefore: now,
        //        claims: identity.Claims,
        //        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),

        //        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        //    var responce = new
        //    {
        //        access_token = encodedJwt,
        //        username = identity.Name
        //    };

        //    return Json(responce);
        //}

        #region Old Authorization
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            //return Redirect("~/api/Values/Index");
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    User signedUser = await this.UserManager.FindByEmailAsync(model.Email);
                    if (signedUser != null)
                    {
                        // проверяем, подтвержден ли email
                        if (!await this.UserManager.IsEmailConfirmedAsync(signedUser))
                        {
                            ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                            return View(model);
                        }
                    }


                    var result = await this.SignInManager.PasswordSignInAsync(signedUser.UserName, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    }
                }
                catch
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
            try
            {
                if (this.ModelState.IsValid)
                {
                    User user = await this.UserManager.FindByEmailAsync(model.Email);

                    if (user == null)
                    {
                        user = new User { Email = model.Email, UserName = this.GetUsernameFromEmail(model.Email), Year = model.Year, /*optional*/EmailConfirmed = true };
                        var result = await this.UserManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            //await this.SignInManager.SignInAsync(user, false);

                            var code = await this.UserManager.GenerateEmailConfirmationTokenAsync(user);
                            var callbackUrl = Url.Action(
                                "ConfirmEmail",
                                "Account",
                                new { userId = user.Id, code = code },
                                protocol: HttpContext.Request.Scheme);

                            Message message = new Message(user.Email, "WebAppSome: Email confirmation",
                                $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>подтвердить</a>!");

                            await this.EmailService.SendEmailAsync(message);

                            return View("RegisterInfo");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователь с таким Email уже существует!");
                    }
                }
            }
            catch
            {
                User user = await this.UserManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await this.UserManager.DeleteAsync(user);
                }
                ModelState.AddModelError("", "Something get wrong!");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            ErrorViewModel errorViewModel = new ErrorViewModel
            {
                RequestId = "Confirmation email"
            };

            try
            {
                if (userId == null || code == null)
                {
                    return View("Error", errorViewModel);
                }

                var user = await this.UserManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return View("Error", errorViewModel);
                }

                var result = await this.UserManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    await this.SignInManager.SignInAsync(user, false);
                    return View(user);
                }
                else
                {
                    return View("Error", errorViewModel);
                }
            }
            catch
            {
                return View("Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await this.SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        #endregion

        #region private methods
        private string GetUsernameFromEmail(string email)
        {
            return email?.Split('@')[0];
        }

        /// <summary>
        /// Create ClaimsIdentity from User entity
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        //{
        //    //Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
        //    User user = await this.repo.GetUserAsync(username, password);

        //    if (user != null)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
        //            //new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
        //        };
        //        ClaimsIdentity claimsIdentity =
        //        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
        //            ClaimsIdentity.DefaultRoleClaimType);
        //        return claimsIdentity;
        //    }

        //    // если пользователя не найдено
        //    return null;
        //}

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
