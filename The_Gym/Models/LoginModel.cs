using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace The_Gym.Models
{
    public class LoginModel
    {
        [DisplayName("Email")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Plase Enter Valid Email.")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
        public bool Remember_Me { get; set; }
    }

    public class SignupModel
    {
        public int ID { get; set; }

        [DisplayName("Gym Name")]
        [Required]
        public string Gym_Name { get; set; }

        [DisplayName("Owner First Name")]
        [Required]
        public string First_Name { get; set; }

        [DisplayName("Owner Last Name")]
        [Required]
        public string Last_Name { get; set; }

        [DisplayName("Owner Email")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Plase Enter Valid Email.")]
        public string Email { get; set; }

        [DisplayName("Terms Of Service")]
        [Required]
        public bool Terms_of_Service { get; set; }
    }

    public class Reset_EmailModel
    {
        [DisplayName("Email")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Plase Enter Valid Email.")]
        public string Email { get; set; }

        [DisplayName("Repeat Email")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Plase Enter Valid Email.")]
        [Compare("Email", ErrorMessage = "Email doesn't match.")]
        public string Repeat_Email { get; set; }
        public int Role_ID { get; set; }
    }

    public class Reset_PasswordModel
    {
        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }

        [DisplayName("Repeat Password")]
        [Required]
        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        public string Repeat_Password { get; set; }
        public int Role_ID { get; set; }
    }

    public class Forgot_PasswordModel
    {
        [DisplayName("Email")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Plase Enter Valid Email.")]
        public string Email { get; set; }
    }

    public class Send_FeedbackModel
    {
        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Email")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Plase Enter Valid Email.")]
        public string Email { get; set; }

        [DisplayName("Message")]
        [Required]
        public string Message { get; set; }
        public int Role_ID { get; set; }
    }
}