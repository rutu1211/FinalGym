using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class GYM_FeeModel
    {
        public int ID { get; set; }

        public Nullable<int> Plan_ID { get; set; }

        public Nullable<System.DateTime> Start_Date { get; set; }

        public Nullable<System.DateTime> End_Date { get; set; }
    }
}