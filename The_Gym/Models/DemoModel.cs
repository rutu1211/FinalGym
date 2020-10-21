using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class DemoModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }
        public Nullable<int> Branch_ID { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string First_Name { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string Last_Name { get; set; }

        [DisplayName("Email ID")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email_ID { get; set; }

        [DisplayName("Mobile No")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        public Nullable<System.DateTime> Date { get; set; }

        [DisplayName("Start Date")]
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Start_Date { get; set; }

        [DisplayName("Interested Plane")]
        [Required]
        public Nullable<int> Interested_Plane_ID { get; set; }
    }

    public class DemoPlaneModel
    {
        public DemoModel DemoModel { get; set; }
        public PlaneModel PlaneModel { get; set; }
    }
}