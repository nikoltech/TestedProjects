namespace WebAppSome.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [AllowAnonymous]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("getLogin")]
        public IActionResult GetLogin()
        {
            return this.Ok($"Добро пожаловать! Ваш логин {User.Identity.Name}");
        }

        [Authorize(Roles = "admin")]
        [Route("getRole")]
        public IActionResult GetRole()
        {
            return this.Ok($"Здравствуй админ, {User.Identity.Name}!");
        }
    }
}
