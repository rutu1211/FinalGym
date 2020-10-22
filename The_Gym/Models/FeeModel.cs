using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class FeeModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }

        [DisplayName("Select Student")]
        [Required]
        public Nullable<int> Student_ID { get; set; }

        public Nullable<int> Branch_ID { get; set; }

        [DisplayName("Select Plane")]
        [Required]
        public Nullable<int> Plane_ID { get; set; }

        [DisplayName("Select Offer")]
        public Nullable<int> Offer_ID { get; set; }

        [DisplayName("Start Date")]
        [Required]
        public Nullable<System.DateTime> Start_Date { get; set; }

        [DisplayName("End Date")]
        [Required]
        public Nullable<System.DateTime> End_Date { get; set; }
        public Nullable<System.DateTime> Payment_Date { get; set; }
        public Nullable<int> Discount_On_Bill_Type { get; set; }

        [DisplayName("Discount On Bill")]
        public Nullable<decimal> Discount_On_Bill { get; set; }

        [DisplayName("Payment Amount")]
        [Required]
        public Nullable<decimal> Payment_Amount { get; set; }
        public string GST { get; set; }

        [DisplayName("GST")]
        public Nullable<decimal> GST_Amount { get; set; }

        public string Duration { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class PlaneBranch_Wise_PlaneModel
    {
        public PlaneModel PlaneModel { get; set; }
        public Branch_Wise_PlaneModel Branch_Wise_PlaneModel { get; set; }
    }

    public class StudentFeePlaneModel
    {
        public StudentModel StudentModel { get; set; }
        public FeeModel FeeModel { get; set; }
        public PlaneModel PlaneModel { get; set; }
    }

    public class FeeStudentPlaneOfferModel
    {
        public StudentModel StudentModel { get; set; }
        public FeeModel FeeModel { get; set; }
        public PlaneModel PlaneModel { get; set; }
        public OfferModel OfferModel { get; set; }
    }
}