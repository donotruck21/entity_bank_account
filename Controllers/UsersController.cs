using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers
{
    public class UsersController : Controller{

        private BankAccountContext _context;

        public UsersController(BankAccountContext context){
            _context = context;
        }
        // GET: /Register/
        [HttpGet]
        [Route("")]
        public IActionResult Register(){
            ViewBag.Errors = new List<User>();
            return View();
        }

        // GET: /Login/
        [HttpGet]
        [Route("Login")]
        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(RegisterViewModel model){
            if(ModelState.IsValid){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User NewUser = new User{
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Money = 0
                };
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                _context.Add(NewUser);


                User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == NewUser.Email);
                HttpContext.Session.SetInt32("CurrUserId", CurrentUser.UserId);
                return View("Success");
            } else {
                ViewBag.Errors = ModelState.Values;
                return View("Register");
            }
        }
    }

    
}
