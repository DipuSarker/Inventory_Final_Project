using OST_Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OST_Inventory.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]                        
        public ActionResult Login(string btnSubmit, BaseAccount baseAccount)
        {
            string loginMsg = "";

            if (btnSubmit == "Login")
            {
                bool verifyStatus = baseAccount.verifyLogin();

                if (verifyStatus)
                {
                    Session["User"] = baseAccount.Username;
                    loginMsg = "Login success";
                    //return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    loginMsg = "Invalid username or password !!";
                }
                //if (baseAccount.Username == "Munna" && baseAccount.Password == "123456")
                //{
                //    Session["User"] = "Munna";
                //    loginMsg = "Login success";
                //    return RedirectToAction("Dashboard", "Home");
                //}
                //else
                //{
                //    loginMsg = "Login Fail";
                //}
            }
            else if (btnSubmit == "Forget Password")
            {
                return RedirectToAction("forget", "Account");
            }

            ViewBag.loginMsg = loginMsg;
            return View();
        }


        //public ActionResult Forgetpassword(string btnSubmit)
        //{
        //    if (btnSubmit == "Forget Password")
        //    {
        //        return RedirectToAction("forget", "Account");
        //    }
        //    return RedirectToAction("Login", "Account");
        //}

        public ActionResult forget()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}