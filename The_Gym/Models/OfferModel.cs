using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class OfferModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Start Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public Nullable<System.DateTime> Start_Date { get; set; }

        [DisplayName("End Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public Nullable<System.DateTime> End_Date { get; set; }

        [DisplayName("Discount Type")]
        public Nullable<int> Discount_Type { get; set; }

        [DisplayName("Discount In Percentage")]
        [Required]
        public Nullable<decimal> Discount { get; set; }
        public Nullable<bool> Status { get; set; }
    }

    public class OfferTotalModel
    {
        public OfferModel Offer { get; set; }
        public TotalModel total { get; set; }
    }
}