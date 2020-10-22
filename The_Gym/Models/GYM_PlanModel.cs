using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class GYM_PlanModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public Nullable<decimal> Worth { get; set; }

        public string Duracation { get; set; }

        public Nullable<int> Duracation_NO { get; set; }

        public Nullable<int> Number_Of_Student { get; set; }
    }
}