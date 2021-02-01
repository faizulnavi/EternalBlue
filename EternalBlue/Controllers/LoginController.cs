using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EternalBlue.Models;

namespace EternalBlue.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
           Session["Login"] = "True"; //storing login session to prevent unauthorize access of application.
           return View();
        }
        //Below controller fucntion will convert ascii to text and will compare with entered password. if password is correct will redirect to After login page (AfterLogin)
        [HttpPost()]
        public ActionResult Index(LoginViewModel passw)
        {
            string pass =  Request.Form["pass"];
            string[] PasswordHint = pass.Split(' ');
            string compareText = asciiToSentence(PasswordHint);
            string EnterPassword = passw.Password;
            if (EnterPassword == compareText)
            {
                ModelState.Clear();
                return RedirectToAction("AfterLogin");
            }
            ModelState.AddModelError("Password", "Incorrect Password");
            return View(passw);
        }

        public ActionResult AfterLogin()
        {
            if (Session["Login"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        //Will convert Image hint to normal text and will compare with entered password, if the password is correct will redirect to task page (Page3)
        [HttpPost()]
        public ActionResult AfterLogin(LoginViewModel pssw2)
        {
            string Encodedpass2 = "Ymx1ZTFzRjByZXYzcg==";
            string EnterPassword = pssw2.Password;
            if (EnterPassword == Base64Decode(Encodedpass2))
            {
                return RedirectToAction("Task");
            }
            ModelState.AddModelError("Password", "Incorrect Password");
            return View(pssw2);
        }

        public ActionResult Task()
        {
            if (Session["Login"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
           
        }

        public ActionResult DownloadTask()
        {
            //Build the File Path.
            string filename = "Task.md";
            string path = Server.MapPath("~/File/") + filename;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", filename);
        }

        public static string asciiToSentence(String[] str)
        {
            int num = 0;
            var strbuilder = new StringBuilder();
            string passvalue;
            foreach(string charstr in str)
            {
                num = Convert.ToInt32(charstr);
               
                // If num is within the required range 
                if (num >= 32 && num <= 122)
                {

                    // Convert num to char 
                    char ch = (char)num;
                    strbuilder.Append(ch);

                    // Reset num to 0 
                    num = 0;
                }

            }
            passvalue = strbuilder.ToString();
            return passvalue;
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}