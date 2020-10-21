using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class Branch_Wise_OfferModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }
        public Nullable<int> Branch_ID { get; set; }
        public Nullable<int> Offer_ID { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class Branch_Wise_OfferBranchModel
    {
        public Branch_Wise_OfferModel Branch_Wise_OfferModel { get; set; }
        public BranchModel Branch { get; set; }
    }

    public class Branch_Wise_OfferOfferModel
    {
        public Branch_Wise_OfferModel Branch_Wise_OfferModel { get; set; }
        public OfferModel OfferModel { get; set; }
    }
}