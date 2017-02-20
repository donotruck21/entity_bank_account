using System;
using System.Collections.Generic;
using System.Linq;
using BankAccount.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers
{
    public class ActionsController : Controller{

        private BankAccountContext _context;

        public ActionsController(BankAccountContext context){
            _context = context;
        }
        // Post: /Action/
        [HttpPost]
        [Route("Action")]
        public IActionResult Register(int amount){
            System.Console.WriteLine(amount);
            User myUser = _context.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("CurrUserId"));
            if((-1 * amount) > myUser.Money){
                HttpContext.Session.SetString("WithdrawError", "Not Enough Money");
                return RedirectToAction("Dashboard", "Users");
            } else {
                BankAccount.Models.Action NewAction = new BankAccount.Models.Action{
                    Type = "Deposit",
                    Amount = amount,
                    UserId = (int)HttpContext.Session.GetInt32("CurrUserId"),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                if(amount < 0){
                    NewAction.Type = "Withdrawal";
                }
                _context.Add(NewAction);
                myUser.Money += amount;
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Users");
            }
            
        }

    }
    
}
