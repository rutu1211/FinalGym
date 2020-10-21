using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Configuration;

namespace The_Gym.Models
{
    public static class Mail_BodyModel
    {
        public static string OTP(string bodys, string name, string Gym_Name)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Mail Template/OTP.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("^Gym Name^", Gym_Name + "<br>");
            body = body.Replace("^Resident’s Name^", name + "<br>");
            body = body.Replace("^Resident’s Body^", bodys + "<br>");
            return body;
        }

        public static string Forgate_Password(string bodys, string name, string Gym_Name)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Mail Template/Forgate_Password.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("^Gym Name^", Gym_Name + "<br>");
            body = body.Replace("^Resident’s Name^", name + "<br>");
            body = body.Replace("^Resident’s Body^", bodys + "<br>");
            return body;
        }

        public static string Feedback(string bodys, string name, string Gym_Name)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/Mail Template/Feedback.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("^Gym Name^", Gym_Name + "<br>");
            body = body.Replace("^Resident’s Name^", name + "<br>");
            body = body.Replace("^Resident’s Body^", bodys + "<br>");
            return body;
        }
    }
}