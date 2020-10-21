using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class PlaneModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Worth In RS")]
        [Required]
        public Nullable<decimal> Worth { get; set; }

        [DisplayName("Duration")]
        [Required]
        public string Duration { get; set; }

        [DisplayName("Duration No")]
        [Required]
        public Nullable<int> Duration_No { get; set; }
        public Nullable<bool> Is_Active { get; set; }

        [DisplayName("GST")]
        [Required]
        public string GST { get; set; }
    }

    public class PlaneTotalModel
    {
        public PlaneModel PlaneModel { get; set; }
        public TotalModel TotalModel { get; set; }
    }
}