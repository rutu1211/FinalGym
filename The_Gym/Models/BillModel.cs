using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class BillModel
    {
        public string Fee { get; set; }
        public string Student { get; set; }
        public string GST { get; set; }
        public string Planes_Name { get; set; }
        public string Durations { get; set; }
        public string Price { get; set; }
        public string Offers { get; set; }
        public string DiscountOnBill { get; set; }
        public string Total { get; set; }
        public string Invoice_Date { get; set; }
        public string Student_Name { get; set; }
        public string Building_Name { get; set; }
        public string Area { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pin_Code { get; set; }
        public string Mobile { get; set; }
        public string GYM_Name { get; set; }
        public string Branch_Name { get; set; }
        public string Grand_Total { get; set; }
    }
}