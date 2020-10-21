using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class Branch_Wise_PlaneModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }
        public Nullable<int> Branch_ID { get; set; }
        public Nullable<int> Plane_ID { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class Branch_Wise_PlaneBranchModel
    {
        public Branch_Wise_PlaneModel Branch_Wise_PlaneModel { get; set; }
        public BranchModel Branch { get; set; }
    }

    public class Branch_Wise_PlanePlaneModel
    {
        public Branch_Wise_PlaneModel Branch_Wise_PlaneModel { get; set; }
        public PlaneModel PlaneModel { get; set; }
    }
}