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
            ViewBag.Errors = "";
            return View();
        }

        // GET: /Dashboard/
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard(){
            ViewBag.CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("CurrUserId"));
            if(ViewBag.AllActions == null){
                ViewBag.AllActions = _context.Actions.Where(action => action.UserId == HttpContext.Session.GetInt32("CurrUserId")).ToList();
            } else {
                ViewBag.AllActions = new List<BankAccount.Models.Action>();
            }

            if(ViewBag.WithdrawError == null){
                ViewBag.WithdrawError = "";
            } else {
                ViewBag.WithdrawError = HttpContext.Session.GetString("WithdrawError");
            }
            return View();
        }


        // Register //
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(RegisterViewModel model){
            if(ModelState.IsValid){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User NewUser = new User{
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    Email = model.Email,                    
                    Money = 0
                };
                NewUser.Password = Hasher.HashPassword(NewUser, NewUser.Password);
                _context.Add(NewUser);
                _context.SaveChanges();


                User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == NewUser.Email);
                HttpContext.Session.SetInt32("CurrUserId", CurrentUser.UserId);
                return RedirectToAction("Dashboard");
            } else {
                ViewBag.Errors = ModelState.Values;
                return View("Register");
            }
        }


        // Log In//
        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUser(string email, string PwToCheck){
            var user = _context.Users.SingleOrDefault(userr => userr.Email == email);
            if(user != null && PwToCheck != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, PwToCheck)){
                    HttpContext.Session.SetInt32("CurrUserId", user.UserId);
                    return RedirectToAction("Dashboard");
                } else {
                    ViewBag.Errors = "Invalid Name or Password";
                    return View("Login");
                }
            } else {
                ViewBag.Errors = "Invalid Name or Password";
                return View("Login");
            }

        }



        // GET: /Log Out/
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return View("Login");
        }
    }

    
}
