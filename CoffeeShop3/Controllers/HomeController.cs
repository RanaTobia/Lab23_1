using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeShopLab23.Models;

namespace CoffeeShopLab23.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult login(string users)
        //{
        //    ShopDBContext db = new ShopDBContext();

        //    foreach (Users u in db.Users)
        //    {
        //        if (users == u.UserName)
        //        {
        //            return View();
        //        }
        //       else  return View("Register");
        //    }
        //    return View("Shop");
        //}

        public IActionResult login()
        {
            //set a TempData with true when they login

            TempData["username"] = true;
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Shop()
        {
            ShopDBContext db = new ShopDBContext();

            //if this is null, they skipped login to view the shop without logging in
            if (TempData["username"] == null)
            {
                TempData["username"] = false;
            }

            return View(db);
        }
        public IActionResult buyItem()
        {
           
            return View();
        }

      

        public IActionResult MakeNewUser(Users u)
        {
            ShopDBContext db = new ShopDBContext();

            db.Users.Add(new Models.Users()
            {
                UserName = u.UserName,
                Email = u.Email,
                Password = u.Password,
                Gender = u.Gender,
                PhoneNumber = u.PhoneNumber,
                DateOfBirth = u.DateOfBirth,
                Counrty = u.Counrty,
            });

            // we should save changes that we just made to our database 
            db.SaveChanges();
            return View("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Get(string Items)
        {
            var itemObj = new Items();
            ShopDBContext db = new ShopDBContext();
            foreach (Items items in db.Items)
            {
                itemObj = new Items()
                {
                    Id = items.Id,
                    Name = items.Name,
                    Description = items.Description,
                    Price = items.Price,
                    Quantity = items.Quantity

                };
            }


            return Ok(itemObj);
        }
    }
}
