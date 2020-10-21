using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class BranchModel
    {
        public int ID { get; set; }
        public Nullable<int> GYM_ID { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Building Name")]
        [Required]
        public string Building_Name { get; set; }

        [DisplayName("Area")]
        [Required]
        public string Area { get; set; }

        [DisplayName("Landmark")]
        [Required]
        public string Landmark { get; set; }

        [DisplayName("City")]
        [Required]
        public Nullable<int> City_ID { get; set; }

        [DisplayName("State")]
        [Required]
        public Nullable<int> State_ID { get; set; }

        [DisplayName("Country")]
        [Required]
        public Nullable<int> Country_ID { get; set; }

        [DisplayName("Postal Code")]
        [Required]
        [DataType(DataType.PostalCode)]
        public Nullable<int> Pin_Code { get; set; }
        public Nullable<bool> IS_Active { get; set; }
    }

    public class BranchTotalModel
    {
        public BranchModel Branch { get; set; }
        public TotalModel Total { get; set; }
        public CountryModel Country { get; set; }
        public StateModel State { get; set; }
        public CityModel City { get; set; }
    }
}