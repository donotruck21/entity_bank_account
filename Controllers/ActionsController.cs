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
            if(amount > 0){
                System.Console.WriteLine("THIS IS A DEPOSIT");
            } else {
                System.Console.WriteLine("THIS IS A WITHDRAWAL");
            }
            return RedirectToAction("Dashboard", "Users");
        }


        


    }

    
}
