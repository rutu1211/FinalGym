using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class StudentModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }

        [DisplayName("Branch Name")]
        [Required]
        public Nullable<int> Branch_ID { get; set; }

        [DisplayName("Trainer Name")]
        [Required]
        public Nullable<int> Trainer_ID { get; set; }

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

        [DisplayName("Birth Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DOB { get; set; }

        [DisplayName("Building Name")]
        [Required]
        public string Building_Name { get; set; }

        [DisplayName("Area")]
        [Required]
        public string Area { get; set; }

        [DisplayName("Landmark")]
        [Required]
        public string Landmark { get; set; }
        public Nullable<int> City_ID { get; set; }
        public Nullable<int> State_ID { get; set; }
        public Nullable<int> Country_ID { get; set; }

        [DisplayName("Postal Code")]
        [Required]
        [DataType(DataType.PostalCode)]
        public Nullable<int> Pin_Code { get; set; }

        [DisplayName("Mobile No")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [DisplayName("Emergency Concact No")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Emergency { get; set; }

        [DisplayName("Occupation")]
        [Required]
        public string Occupation { get; set; }

        public string Reference { get; set; }
        public string Disease { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<int> Current_Package_ID { get; set; }
        public string Special_Instrucation { get; set; }

        public Nullable<bool> Status { get; set; }
        public string Full_Name { get; set; }
    }
    
    public class StudentTrainerPlaneModel
    {
        public StudentModel Student { get; set; }
        public TrainerModel Trainer { get; set; }
        public PlaneModel Plane { get; set; }
    }
}