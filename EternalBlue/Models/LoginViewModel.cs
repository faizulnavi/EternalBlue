using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EternalBlue.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}