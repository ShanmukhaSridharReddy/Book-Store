using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Model
{
    public class UserModel
    {
        [Required(ErrorMessage ="UserName Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Phone Number is Required")]
        public string phoneNo {  get; set; }        

    }
}
