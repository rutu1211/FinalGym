using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.IO;
using System.Configuration;

namespace The_Gym.Models
{
	public static class SendMailModel
	{

		public static void OTP(string bodys, string name, string to, string Gym_Name)
		{

			string body = Mail_BodyModel.OTP(bodys, name, Gym_Name);
			sendMail(body, to);
		}

		public static void Forgate_Password(string bodys, string name, string to, string Gym_Name)
		{

			string body = Mail_BodyModel.Forgate_Password(bodys, name, Gym_Name);
			sendMail(body, to);
		}

		public static void Feedback(string bodys, string name, string to, string Gym_Name)
		{

			string body = Mail_BodyModel.Feedback(bodys, name, Gym_Name);
			sendMail(body, to);
		}

		public static void sendMail(string body, string to)
		{
			try
			{
				MailMessage message = new MailMessage();
				message.From = new MailAddress("noreply@therutu.com");
				message.To.Add(new MailAddress(to));
				message.Subject = "OTP";
				message.SubjectEncoding = System.Text.Encoding.UTF8;
				message.Body = body;
				message.IsBodyHtml = true;
				SmtpClient smtp = new SmtpClient();
				smtp.Send(message);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

		}
	}
}