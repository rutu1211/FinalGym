using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class TrainerModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }
        public Nullable<int> Role_ID { get; set; }

        [DisplayName("Select Branch")]
        [Required]
        public Nullable<int> Branvch_ID { get; set; }

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
        public string Building_Name { get; set; }
        public string Area { get; set; }
        public string Landmark { get; set; }
        public Nullable<int> City_ID { get; set; }
        public Nullable<int> State_ID { get; set; }
        public Nullable<int> Country_ID { get; set; }
        public Nullable<int> Pin_Code { get; set; }
        public Nullable<bool> IS_Active { get; set; }
        public Nullable<System.DateTime> Joining_Date { get; set; }
        public string Full_Name { get; set; }
        public string Password { get; set; }
    }
    
    public class TrainerBranchModel
    {
        public BranchModel Branch { get; set; }
        public TrainerModel Trainer { get; set; }
    }

    public class TrainerTotalModel
    {
        public TrainerModel Trainer { get; set; }
        public TotalModel Total { get; set; }
    }
}