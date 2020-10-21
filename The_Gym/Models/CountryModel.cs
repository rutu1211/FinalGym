using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class CountryModel
    {
        public int ID { get; set; }
        public string SortName { get; set; }
        public string CountryNames { get; set; }
        public Nullable<int> PhoneCode { get; set; }
    }
}