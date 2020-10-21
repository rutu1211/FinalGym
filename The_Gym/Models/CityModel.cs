using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class CityModel
    {
        public int ID { get; set; }
        public string City_Name { get; set; }
        public Nullable<int> State_ID { get; set; }
    }
}