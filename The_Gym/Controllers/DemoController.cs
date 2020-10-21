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
    [Authorize]
    public class DemoController : Controller
    {
        private The_GymEntities db = new The_GymEntities();

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                var Demoe = db.Demoes.Where(i => i.Branch_ID == Branvch_ID).OrderByDescending(i => i.ID).ToList();
                List<DemoPlaneModel> DemoPlaneModelList = new List<DemoPlaneModel>();
                foreach (var d in Demoe)
                {
                    DemoPlaneModel DemoPlaneModel = new DemoPlaneModel();
                    PlaneModel planeModel = new PlaneModel();
                    DemoModel demoModel = new DemoModel();
                    demoModel.Branch_ID = d.Branch_ID;
                    demoModel.Date = d.Date;
                    demoModel.Email_ID = d.Email_ID;
                    demoModel.First_Name = d.First_Name;
                    demoModel.GYM_ID = d.GYM_ID;
                    demoModel.ID = d.ID;
                    demoModel.Interested_Plane_ID = d.Interested_Plane_ID;
                    demoModel.Last_Name = d.Last_Name;
                    demoModel.Mobile = d.Mobile;
                    demoModel.Start_Date = d.Start_Date;
                    var Name = db.Planes.Where(i => i.ID == d.Interested_Plane_ID).FirstOrDefault();
                    if (Name != null)
                    {
                        planeModel.Name = Name.Name;
                    }
                    DemoPlaneModel.PlaneModel = planeModel;
                    DemoPlaneModel.DemoModel = demoModel;
                    DemoPlaneModelList.Add(DemoPlaneModel);
                }
                return View(DemoPlaneModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                var status = new List<string>() { "True", "False" };
                ViewBag.Form_Fee = status;
                var Plane_NAMES = db.Branch_Wise_Plane.Where(i => i.Branch_ID == Branvch_ID && i.Status == true).ToList();
                List<PlaneModel> PlaneModelList = new List<PlaneModel>();
                foreach (var Plane_NAME in Plane_NAMES)
                {
                    PlaneModel PlaneModel = new PlaneModel();
                    var Plane = db.Planes.Where(i => i.ID == Plane_NAME.Plane_ID).FirstOrDefault();
                    PlaneModel.Name = Plane.Name;
                    PlaneModel.ID = Plane.ID;
                    PlaneModelList.Add(PlaneModel);
                }
                ViewBag.Plane_NAME = PlaneModelList;
                return View();
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DemoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    var students = db.Students.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    if (trainers == null && students == null && demo == null)
                    {
                        int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                        int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                        var Demo = Mapper.Map<Demo>(model);
                        Demo.GYM_ID = GYM_ID;
                        Demo.Branch_ID = Branvch_ID;
                        db.Demoes.Add(Demo);
                        db.SaveChanges();
                        TempData["Success"] = "Visitor Has Created Suscyfully.!";
                    }
                    else
                    {
                        int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                        var status = new List<string>() { "True", "False" };
                        ViewBag.Form_Fee = status;
                        var Plane_NAMES = db.Branch_Wise_Plane.Where(i => i.Branch_ID == Branvch_ID && i.Status == true).ToList();
                        List<PlaneModel> PlaneModelList = new List<PlaneModel>();
                        foreach (var Plane_NAME in Plane_NAMES)
                        {
                            PlaneModel PlaneModel = new PlaneModel();
                            var Plane = db.Planes.Where(i => i.ID == Plane_NAME.Plane_ID).FirstOrDefault();
                            PlaneModel.Name = Plane.Name;
                            PlaneModel.ID = Plane.ID;
                            PlaneModelList.Add(PlaneModel);
                        }
                        ViewBag.Plane_NAME = PlaneModelList;
                        TempData["Error"] = "MailId is alredy registered";
                        return View();
                    }
                }
                else
                {
                    int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                    var status = new List<string>() { "True", "False" };
                    ViewBag.Form_Fee = status;
                    var Plane_NAMES = db.Branch_Wise_Plane.Where(i => i.Branch_ID == Branvch_ID && i.Status == true).ToList();
                    List<PlaneModel> PlaneModelList = new List<PlaneModel>();
                    foreach (var Plane_NAME in Plane_NAMES)
                    {
                        PlaneModel PlaneModel = new PlaneModel();
                        var Plane = db.Planes.Where(i => i.ID == Plane_NAME.Plane_ID).FirstOrDefault();
                        PlaneModel.Name = Plane.Name;
                        PlaneModel.ID = Plane.ID;
                        PlaneModelList.Add(PlaneModel);
                    }
                    ViewBag.Plane_NAME = PlaneModelList;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View();
                }
                return RedirectToAction("Index", "Demo");
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Edit(Int64 id)
        {
            try
            {
                var Details = db.Demoes.Where(b => b.ID == id).FirstOrDefault();
                if (Details != null)
                {
                    int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                    var status = new List<string>() { "True", "False" };
                    ViewBag.Form_Fee = status;
                    var Plane_NAMES = db.Branch_Wise_Plane.Where(i => i.Branch_ID == Branvch_ID && i.Status == true).ToList();
                    List<PlaneModel> PlaneModelList = new List<PlaneModel>();
                    foreach (var Plane_NAME in Plane_NAMES)
                    {
                        PlaneModel PlaneModel = new PlaneModel();
                        var Plane = db.Planes.Where(i => i.ID == Plane_NAME.Plane_ID).FirstOrDefault();
                        PlaneModel.Name = Plane.Name;
                        PlaneModel.ID = Plane.ID;
                        PlaneModelList.Add(PlaneModel);
                    }
                    ViewBag.Plane_NAME = PlaneModelList;
                    var Demo = Mapper.Map<DemoModel>(Details);
                    return View(Demo);
                }
                else
                {
                    return View();
                }
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(DemoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    var students = db.Students.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID && i.ID != model.ID).FirstOrDefault();
                    if (trainers == null && students == null && demo == null)
                    {
                        var dataExists = db.Demoes.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (dataExists != null)
                        {
                            dataExists.Email_ID = model.Email_ID;
                            dataExists.First_Name = model.First_Name;
                            dataExists.Interested_Plane_ID = model.Interested_Plane_ID;
                            dataExists.Last_Name = model.Last_Name;
                            dataExists.Mobile = model.Mobile;
                            dataExists.Start_Date = model.Start_Date;
                            db.SaveChanges();
                            TempData["Success"] = "Visitor Has Updated Suscyfully.!";
                        }
                    }
                    else
                    {
                        int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                        var status = new List<string>() { "True", "False" };
                        ViewBag.Form_Fee = status;
                        var Plane_NAMES = db.Branch_Wise_Plane.Where(i => i.Branch_ID == Branvch_ID && i.Status == true).ToList();
                        List<PlaneModel> PlaneModelList = new List<PlaneModel>();
                        foreach (var Plane_NAME in Plane_NAMES)
                        {
                            PlaneModel PlaneModel = new PlaneModel();
                            var Plane = db.Planes.Where(i => i.ID == Plane_NAME.Plane_ID).FirstOrDefault();
                            PlaneModel.Name = Plane.Name;
                            PlaneModel.ID = Plane.ID;
                            PlaneModelList.Add(PlaneModel);
                        }
                        ViewBag.Plane_NAME = PlaneModelList;
                        TempData["Error"] = "MailId is alredy registered";
                        return View();
                    }
                }
                else
                {
                    int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                    var status = new List<string>() { "True", "False" };
                    ViewBag.Form_Fee = status;
                    var Plane_NAMES = db.Branch_Wise_Plane.Where(i => i.Branch_ID == Branvch_ID && i.Status == true).ToList();
                    List<PlaneModel> PlaneModelList = new List<PlaneModel>();
                    foreach (var Plane_NAME in Plane_NAMES)
                    {
                        PlaneModel PlaneModel = new PlaneModel();
                        var Plane = db.Planes.Where(i => i.ID == Plane_NAME.Plane_ID).FirstOrDefault();
                        PlaneModel.Name = Plane.Name;
                        PlaneModel.ID = Plane.ID;
                        PlaneModelList.Add(PlaneModel);
                    }
                    ViewBag.Plane_NAME = PlaneModelList;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View();
                }
                return RedirectToAction("Index", "Demo");
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Delete(Int64 ID)
        {
            try
            {
                new Thread(new ThreadStart(() =>
                {
                    var Demo = db.Demoes.Where(i => i.ID == ID).FirstOrDefault();
                    db.Demoes.Remove(Demo);
                    db.SaveChanges();
                })).Start();
                TempData["Success"] = "Demo Has Deleted Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Today_Joining(Int64 ID)
        {
            try
            {
                var Demoe = db.Demoes.Where(d => DbFunctions.TruncateTime(d.Start_Date) == DbFunctions.TruncateTime(DateTime.Now) && d.Branch_ID == ID).OrderByDescending(i => i.ID).ToList();
                List<DemoPlaneModel> DemoPlaneModelList = new List<DemoPlaneModel>();
                foreach (var d in Demoe)
                {
                    DemoPlaneModel DemoPlaneModel = new DemoPlaneModel();
                    PlaneModel planeModel = new PlaneModel();
                    DemoModel demoModel = new DemoModel();
                    demoModel.Branch_ID = d.Branch_ID;
                    demoModel.Date = d.Date;
                    demoModel.Email_ID = d.Email_ID;
                    demoModel.First_Name = d.First_Name;
                    demoModel.GYM_ID = d.GYM_ID;
                    demoModel.ID = d.ID;
                    demoModel.Interested_Plane_ID = d.Interested_Plane_ID;
                    demoModel.Last_Name = d.Last_Name;
                    demoModel.Mobile = d.Mobile;
                    demoModel.Start_Date = d.Start_Date;
                    var Name = db.Planes.Where(i => i.ID == d.Interested_Plane_ID).FirstOrDefault();
                    if (Name != null)
                    {
                        planeModel.Name = Name.Name;
                    }
                    DemoPlaneModel.PlaneModel = planeModel;
                    DemoPlaneModel.DemoModel = demoModel;
                    DemoPlaneModelList.Add(DemoPlaneModel);
                }
                return View(DemoPlaneModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Today_Visit(Int64 ID)
        {
            try
            {
                var Demoe = db.Demoes.Where(i => i.GYM_ID == ID && DbFunctions.TruncateTime(i.Date) == DbFunctions.TruncateTime(DateTime.Now)).OrderByDescending(i => i.ID).ToList();
                List<DemoPlaneModel> DemoPlaneModelList = new List<DemoPlaneModel>();
                foreach (var d in Demoe)
                {
                    DemoPlaneModel DemoPlaneModel = new DemoPlaneModel();
                    PlaneModel planeModel = new PlaneModel();
                    DemoModel demoModel = new DemoModel();
                    demoModel.Branch_ID = d.Branch_ID;
                    demoModel.Date = d.Date;
                    demoModel.Email_ID = d.Email_ID;
                    demoModel.First_Name = d.First_Name;
                    demoModel.GYM_ID = d.GYM_ID;
                    demoModel.ID = d.ID;
                    demoModel.Interested_Plane_ID = d.Interested_Plane_ID;
                    demoModel.Last_Name = d.Last_Name;
                    demoModel.Mobile = d.Mobile;
                    demoModel.Start_Date = d.Start_Date;
                    var Name = db.Planes.Where(i => i.ID == d.Interested_Plane_ID).FirstOrDefault();
                    if (Name != null)
                    {
                        planeModel.Name = Name.Name;
                    }
                    DemoPlaneModel.PlaneModel = planeModel;
                    DemoPlaneModel.DemoModel = demoModel;
                    DemoPlaneModelList.Add(DemoPlaneModel);
                }
                return View(DemoPlaneModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }
    }
}