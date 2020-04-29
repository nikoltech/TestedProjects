namespace WebAppSome.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        //public async Task<IActionResult> Login(LoginModel model)
        //{
        //    if(!this.ModelState.IsValid)
        //    {
        //        return View(model);
        //    }


        //}
    }
}
