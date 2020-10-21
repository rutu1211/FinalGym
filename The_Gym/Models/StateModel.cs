using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class StateModel
    {
        public int ID { get; set; }
        public string StateName { get; set; }
        public Nullable<int> Country_ID { get; set; }
    }
}