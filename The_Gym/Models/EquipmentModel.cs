using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class EquipmentModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }

        [DisplayName("Equipment Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("How To Use")]
        public string Video { get; set; }
    }

    public class EquipmentTotalModel
    {
        public EquipmentModel EquipmentModel { get; set; }
        public TotalModel TotalModel { get; set; }
    }
}