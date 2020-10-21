using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class Branch_Wise_EquipmentModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }
        public Nullable<int> Branch_ID { get; set; }
        public Nullable<int> Equipment_ID { get; set; }
        public Nullable<int> Number { get; set; }
    }

    public class Branch_Wise_EquipmentBranchModel
    {
        public BranchModel BranchModel { get; set; }
        public Branch_Wise_EquipmentModel Branch_Wise_EquipmentModel { get; set; }
    }
}