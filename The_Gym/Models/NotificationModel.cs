using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace The_Gym.Models
{
    public class NotificationModel
    {
        [DisplayName("Message")]
        [Required]
        public string MSG { get; set; }

        [DisplayName("Manager")]
        public bool Manager { get; set; }

        [DisplayName("Trainer")]
        public bool Trainer { get; set; }

        [DisplayName("Student")]
        public bool Student { get; set; }
    }
}