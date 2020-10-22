using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class GYMModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IS_Active { get; set; }
        public Nullable<int> Plan_ID { get; set; }
    }
}