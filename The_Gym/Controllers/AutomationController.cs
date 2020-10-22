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
using System.Threading;

namespace The_Gym.Controllers
{
    public class AutomationController : Controller
    {
        private The_GymEntities db = new The_GymEntities();

        [HttpGet]
        public ActionResult Student_Fee_Unpaid()
        {
            try
            {
                var gyms = db.GYMs.Where(i => i.IS_Active == true).ToList();
                foreach (var gym in gyms)
                {
                    var students = db.Students.Where(i => i.Status == true).ToList();
                    foreach (var student in students)
                    {
                        var Fees = db.Fees.Where(i => i.Student_ID == student.ID).ToList();
                        foreach (var Fee in Fees)
                        {
                            DateTime end_date = Fee.End_Date.Value;
                            DateTime start_date = DateTime.Now;
                            TimeSpan nod = (end_date - start_date);
                            var newdate = nod.TotalMilliseconds;
                            if (newdate > 0 && newdate < 604800000)
                            {
                                // Fee Mail
                            }
                            if (newdate < 0)
                            {
                                Fee.Status = false;
                                db.SaveChanges();
                                var Active = db.Fees.Where(i => i.Student_ID == student.ID && i.ID != Fee.ID).FirstOrDefault();
                                if (Active == null)
                                {
                                    student.Status = false;
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
                return View();
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult GYM_Fee_Unpaid()
        {
            try
            {
                var GYMs = db.GYMs.Where(i => i.IS_Active == true).ToList();
                foreach (var GYM in GYMs)
                {
                    var Fees = db.GYM_Fee.Where(i => i.ID == GYM.ID).ToList();
                    foreach (var Fee in Fees)
                    {
                        DateTime end_date = Fee.End_Date.Value;
                        DateTime start_date = DateTime.Now;
                        TimeSpan nod = (end_date - start_date);
                        var newdate = nod.TotalMilliseconds;
                        if (newdate > 0 && newdate < 604800000)
                        {
                            // Fee Mail
                        }
                        if (newdate < 0)
                        {
                            GYM.IS_Active = false;
                        }
                    }
                }
                return View();
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        public ActionResult Student_Birthday_Notifications()
        {
            try
            {
                var gyms = db.GYMs.Where(i => i.IS_Active == true).ToList();
                foreach (var gym in gyms)
                {
                    var students = db.Students.Where(i => i.Status == true && DbFunctions.TruncateTime(i.DOB) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
                    foreach (var student in students)
                    {
                        // Birthday Student Mail
                    }
                }
                return View();
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }
    }
}