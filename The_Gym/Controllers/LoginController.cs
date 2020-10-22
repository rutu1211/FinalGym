using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using The_Gym.Models;
using Omu.ValueInjecter;
using System.Web.Security;
using System.Web.Script.Serialization;

namespace The_Gym.Controllers
{
    public class LoginController : Controller
    {
		private The_GymEntities db = new The_GymEntities();

		[HttpGet]
		public ActionResult Login()
		{
			try
			{
				if (Request.Cookies["UserName"] != null)
				{
					LoginModel loginModel = new LoginModel();
					loginModel.Email = Request.Cookies["UserName"].Value;
					loginModel.Remember_Me = true;
					return View(loginModel);
				}
				TempData["Success"] = null;
				TempData["Error"] = null;
				return View();
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpPost]
		public ActionResult Login(LoginModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var trainer = db.Trainers.Where(i => i.Email_ID == model.Email).FirstOrDefault();
					if (trainer != null && (trainer.Role_ID == 1 || trainer.Role_ID == 2))
					{
						if (trainer.Password == model.Password)
						{
							var gym = db.GYMs.Where(i => i.ID == trainer.GYM_ID).FirstOrDefault();
		                    if (gym.IS_Active == true)
                            {
								Session["Trainer_ID"] = trainer.ID;
								Session["Trainer_Role"] = trainer.Role_ID;
								Session["Name"] = trainer.First_Name + " " + trainer.Last_Name;
								Session["Branvch_ID"] = trainer.Branvch_ID;
								Session["GYM_ID"] = trainer.GYM_ID;
								var Name = db.GYMs.Where(i => i.ID == trainer.GYM_ID).FirstOrDefault();
								Session["GYM_Name"] = Name.Name;

								if (model.Remember_Me == true)
								{
									Response.Cookies["UserName"].Value = model.Email;
									Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
								}
								else
								{
									Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
								}

								if (Convert.ToInt32(Session["Trainer_Role"]) == 1)
								{
									Session["Type"] = "Owner";
									FormsAuthentication.SetAuthCookie(model.Email, false);
									return RedirectToAction("Owner", "Dashbord");
								}

								if (Convert.ToInt32(Session["Trainer_Role"]) == 2)
								{
									Session["Type"] = "Manager";
									FormsAuthentication.SetAuthCookie(model.Email, false);
									return RedirectToAction("Manager", "Dashbord");
								}
                            }
                            else
                            {
                                if (Request.Cookies["UserName"] != null)
                                {
                                    LoginModel loginModel = new LoginModel();
                                    loginModel.Email = Request.Cookies["UserName"].Value;
                                    loginModel.Remember_Me = true;
                                    TempData["Error"] = "Invalid Email Id.!";
                                    return View(loginModel);
                                }
                            }
                        }
						else
						{
							if (Request.Cookies["UserName"] != null)
							{
								LoginModel loginModel = new LoginModel();
								loginModel.Email = Request.Cookies["UserName"].Value;
								loginModel.Remember_Me = true;
								TempData["Error"] = "Invalid Password.!";
								return View(loginModel);
							}
						}
					}
					else
					{
						if (Request.Cookies["UserName"] != null)
						{
							LoginModel loginModel = new LoginModel();
							loginModel.Email = Request.Cookies["UserName"].Value;
							loginModel.Remember_Me = true;
							TempData["Error"] = "Invalid Email Id.!";
							return View(loginModel);
						}
					}
					return View();
				}
				else
				{
					TempData["Error"] = "Please Fill All Required Details.!";
					return View();
				}
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}


		[HttpGet]
		public ActionResult Logout()
		{
			try
			{
				Session.Clear();
				Session.Abandon();
				FormsAuthentication.SignOut();
				return RedirectToAction("Login", "Login");
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpGet]
		public ActionResult Signup(Int64 ID)
		{
			try
			{
				if (ID != 0)
				{
					SignupModel signupModel = new SignupModel();
					var Gym = db.GYMs.Where(i => i.ID == ID).FirstOrDefault();
					signupModel.Gym_Name = Gym.Name;
					signupModel.ID = Gym.ID;
					return View(signupModel);
				}
				return View();
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Signup(SignupModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var trainers = db.Trainers.Where(i => i.Email_ID == model.Email).FirstOrDefault();
					var students = db.Students.Where(i => i.Email_ID == model.Email).FirstOrDefault();
					var demo = db.Demoes.Where(i => i.Email_ID == model.Email).FirstOrDefault();
					if (trainers == null && students == null && demo == null)
					{
						if (model.ID != 0)
						{
							Trainer trainer = new Trainer();
							trainer.First_Name = model.First_Name;
							trainer.Last_Name = model.Last_Name;
							trainer.Email_ID = model.Email;
							trainer.GYM_ID = model.ID;
							trainer.Role_ID = 1;
							trainer.Password = GeneratePasswordModel.GeneratePassword(3, 3, 3);
							//trainer.Password = "admin";
							SendMailModel.OTP(trainer.Password, trainer.First_Name + " " + trainer.Last_Name, trainer.Email_ID, model.Gym_Name);
							db.Trainers.Add(trainer);
							db.SaveChanges();


							GYM gYM = new GYM();
							var dataExists = db.GYMs.Where(b => b.ID == model.ID).FirstOrDefault();
							if (dataExists != null)
							{
								dataExists.Name = model.Gym_Name;
								db.SaveChanges();
							}
							TempData["Success"] = "You are registered sucessfully!! Password has been sent to your registered mail id";
							return RedirectToAction("Signup", "Login", new { ID = model.ID });
						}
						else
						{
							GYM gYM = new GYM();
							gYM.IS_Active = true;
							gYM.Name = model.Gym_Name;
							db.GYMs.Add(gYM);
							db.SaveChanges();

							Trainer trainer = new Trainer();
							trainer.First_Name = model.First_Name;
							trainer.Last_Name = model.Last_Name;
							trainer.Email_ID = model.Email;
							trainer.GYM_ID = gYM.ID;
							trainer.Role_ID = 1;
							trainer.Password = GeneratePasswordModel.GeneratePassword(3, 3, 3);
							//trainer.Password = "admin";
							SendMailModel.OTP(trainer.Password, trainer.First_Name + " " + trainer.Last_Name, trainer.Email_ID, model.Gym_Name);
							db.Trainers.Add(trainer);
							db.SaveChanges();
							TempData["Success"] = "You are registered sucessfully!! Password has been sent to your registered mail id";
							return RedirectToAction("Login", "Login", new { ID = model.ID });
						}
					}
					else
					{
						SignupModel signupModel = new SignupModel();
						signupModel.Gym_Name = model.Gym_Name;
						signupModel.ID = model.ID;
						TempData["Error"] = "MailId is alredy registered";
						return View(signupModel);
					}
				}
				else
				{
					SignupModel signupModel = new SignupModel();
					signupModel.Gym_Name = model.Gym_Name;
					signupModel.ID = model.ID;
					TempData["Error"] = "Please Fill All Required Details.!";
					return View(signupModel);
				}
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex);
				TempData["Error"] = ex;
				return RedirectToAction("Login", "Login");
			}
		}

		[HttpGet]
		public ActionResult Forgot_Password()
		{
			try
			{
				return View();
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpPost]
		public ActionResult Forgot_Password(Forgot_PasswordModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var trainer = db.Trainers.Where(i => i.Email_ID == model.Email).FirstOrDefault();
					if (trainer != null)
					{
						trainer.Password = GeneratePasswordModel.GeneratePassword(3, 3, 3);
						var gym = db.GYMs.Where(i => i.ID == trainer.GYM_ID).FirstOrDefault();
						SendMailModel.Forgate_Password(trainer.Password, trainer.First_Name + " " + trainer.Last_Name, trainer.Email_ID, gym.Name);
						TempData["Success"] = "Please check your registerd mailId for a new password";
						db.SaveChanges();
					}
					else
					{
						TempData["Error"] = "MailId is not registered";
						return View();
					}
				}
				else
				{
					TempData["Error"] = "Please Fill All Required Details.!";
					return View();
				}

				return RedirectToAction("Login", "Login");
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpGet]
		public ActionResult Reset_Password()
		{
			try
			{
				int ID = Convert.ToInt32(Session["Trainer_ID"]);
				var trainer = db.Trainers.Where(i => i.ID == ID).FirstOrDefault();
				Reset_PasswordModel Reset_PasswordModel = new Reset_PasswordModel();
				Reset_PasswordModel.Role_ID = Convert.ToInt32(trainer.Role_ID);
				return View(Reset_PasswordModel);
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpPost]
		public ActionResult Reset_Password(Reset_PasswordModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					int ID = Convert.ToInt32(Session["Trainer_ID"]);
					var trainer = db.Trainers.Where(i => i.ID == ID).FirstOrDefault();
					trainer.Password = model.Password;
					db.SaveChanges();
					TempData["Success"] = "Password has been reset successfully!! ";
					if (trainer.Role_ID == 1)
					{
						return RedirectToAction("Owner", "Dashbord");
					}
					if (trainer.Role_ID == 2)
					{
						return RedirectToAction("Manager", "Dashbord");
					}
					return View();
				}
				else
				{
					TempData["Error"] = "Please Fill All Required Details.!";
					int ID = Convert.ToInt32(Session["Trainer_ID"]);
					var trainer = db.Trainers.Where(i => i.ID == ID).FirstOrDefault();
					Reset_PasswordModel Reset_PasswordModel = new Reset_PasswordModel();
					Reset_PasswordModel.Role_ID = Convert.ToInt32(trainer.Role_ID);
					return View(Reset_PasswordModel);
				}
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpGet]
		public ActionResult Reset_Email()
		{
			try
			{
				int ID = Convert.ToInt32(Session["Trainer_ID"]);
				var trainer = db.Trainers.Where(i => i.ID == ID).FirstOrDefault();
				Reset_EmailModel Reset_EmailModel = new Reset_EmailModel();
				Reset_EmailModel.Role_ID = Convert.ToInt32(trainer.Role_ID);
				return View(Reset_EmailModel);
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpPost]
		public ActionResult Reset_Email(Reset_EmailModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					int ID = Convert.ToInt32(Session["Trainer_ID"]);
					var trainers = db.Trainers.Where(i => i.Email_ID == model.Email && i.ID != ID).FirstOrDefault();
					var students = db.Students.Where(i => i.Email_ID == model.Email).FirstOrDefault();
					var demo = db.Demoes.Where(i => i.Email_ID == model.Email).FirstOrDefault();
					if (trainers == null && students == null && demo == null)
					{
						var trainer = db.Trainers.Where(i => i.ID == ID).FirstOrDefault();
						if (trainer != null)
						{
							trainer.Email_ID = model.Email;
							db.SaveChanges();
							TempData["Success"] = "Email has been reset successfully!! ";
							if (trainer.Role_ID == 1)
							{
								return RedirectToAction("Owner", "Dashbord");
							}
							if (trainer.Role_ID == 2)
							{
								return RedirectToAction("Manager", "Dashbord");
							}
						}
					}
                    else
                    {
						TempData["Error"] = "MailId is alredy registered";
						int Trainer_ID = Convert.ToInt32(Session["Trainer_ID"]);
						var trainer = db.Trainers.Where(i => i.ID == Trainer_ID).FirstOrDefault();
						Reset_EmailModel Reset_EmailModel = new Reset_EmailModel();
						Reset_EmailModel.Role_ID = Convert.ToInt32(trainer.Role_ID);
						return View(Reset_EmailModel);
					}
				}
				else
				{
					TempData["Error"] = "Please Fill All Required Details.!";
					int Trainer_ID = Convert.ToInt32(Session["Trainer_ID"]);
					var trainer = db.Trainers.Where(i => i.ID == Trainer_ID).FirstOrDefault();
					Reset_EmailModel Reset_EmailModel = new Reset_EmailModel();
					Reset_EmailModel.Role_ID = Convert.ToInt32(trainer.Role_ID);
					return View(Reset_EmailModel);
				}
				return View();
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpGet]
		public ActionResult Send_Feedback()
		{
			try
			{
				int ID = Convert.ToInt32(Session["Trainer_ID"]);
				var trainer = db.Trainers.Where(i => i.ID == ID).FirstOrDefault();
				Send_FeedbackModel send_FeedbackModel = new Send_FeedbackModel();
				send_FeedbackModel.Email = trainer.Email_ID;
				send_FeedbackModel.Name = trainer.First_Name + " " + trainer.Last_Name;
				send_FeedbackModel.Role_ID = Convert.ToInt32(trainer.Role_ID);
				return View(send_FeedbackModel);
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpPost]
		public ActionResult Send_Feedback(Send_FeedbackModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					int ID = Convert.ToInt32(Session["Trainer_ID"]);
					var trainer = db.Trainers.Where(i => i.ID == ID).FirstOrDefault();
					if (trainer != null)
					{
						var gym = db.GYMs.Where(i => i.ID == trainer.GYM_ID).FirstOrDefault();
						SendMailModel.Feedback(model.Message, trainer.First_Name + " " + trainer.Last_Name + " " + "( " + trainer.Role_ID + " )", "info@therutu.com", gym.Name);
						TempData["Success"] = "Thank you for sharing your feedback!! ";
						if (trainer.Role_ID == 1)
						{
							return RedirectToAction("Owner", "Dashbord");
						}
						if (trainer.Role_ID == 2)
						{
							return RedirectToAction("Manager", "Dashbord");
						}
					}
					return View();
				}
				else
				{
					TempData["Error"] = "Please Fill All Required Details.!";
					int ID = Convert.ToInt32(Session["Trainer_ID"]);
					var trainer = db.Trainers.Where(i => i.ID == ID).FirstOrDefault();
					Send_FeedbackModel send_FeedbackModel = new Send_FeedbackModel();
					send_FeedbackModel.Email = trainer.Email_ID;
					send_FeedbackModel.Name = trainer.First_Name + " " + trainer.Last_Name;
					send_FeedbackModel.Role_ID = Convert.ToInt32(trainer.Role_ID);
					return View(send_FeedbackModel);
				}
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}

		[HttpGet]
		public ActionResult Bill()
		{
			try
			{
				return View();
			}

			catch (Exception ex)
			{
				return RedirectToAction("Contact", "Home");
			}
		}
	}
}