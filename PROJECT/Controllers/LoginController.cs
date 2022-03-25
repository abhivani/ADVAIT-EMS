using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PROJECT.DB;
using PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT.Controllers
{
    public class LoginController : Controller
    {

        private IConfiguration configuration;
        private readonly AccountDbContext _dbAccountContext;

        public LoginController(IConfiguration iConfig, AccountDbContext accountDbContext)
        {
            configuration = iConfig;
            _dbAccountContext = accountDbContext;
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                var adminUNFromDB = await _dbAccountContext.GetLoginDetails(model);
                if (adminUNFromDB != null)
                {
                    if (model.Username == adminUNFromDB.Username)
                    {
                        if (model.Password == adminUNFromDB.Password)
                        {
                            HttpContext.Session.SetString("UserUN", adminUNFromDB.Username);
                            HttpContext.Session.SetString("UserPW", adminUNFromDB.Password);
                            return RedirectToAction("Dashboard", "Dashboard");
                        }
                    }
                }
            }
            ViewBag.Error = ("Please Enter Valid Username and Password");
            ModelState.Clear();
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UserUN", "null");
            return RedirectToAction("Index", "Home");
        }

    }
}
