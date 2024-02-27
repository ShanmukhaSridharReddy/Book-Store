using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text;

namespace ModelLayer.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email is Required")]
        public String Email {  get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public String Password { get; set; }
    }
}
