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
    public class StudentController : Controller
    {
        private The_GymEntities db = new The_GymEntities();

        [HttpGet]
        public ActionResult Gym_Wise_Index()
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Students = db.Trainers.Join(db.Students, u => u.ID, j => j.Trainer_ID, (u, j) => new { Trainer = u, Student = j }).Where(i => i.Student.GYM_ID == GYM_ID).OrderByDescending(i => i.Student.Status).OrderByDescending(i => i.Student.ID).ToList();
                List<StudentTrainerPlaneModel> StudentTrainerPlaneModelList = new List<StudentTrainerPlaneModel>();
                foreach (var d in Students)
                {
                    StudentTrainerPlaneModel StudentTrainerPlaneModel = new StudentTrainerPlaneModel();
                    TrainerModel trainerModel = new TrainerModel();
                    StudentModel StudentModel = new StudentModel();
                    PlaneModel PlaneModel = new PlaneModel();
                    StudentModel.Area = d.Student.Area;
                    StudentModel.Branch_ID = d.Student.Branch_ID;
                    StudentModel.City_ID = d.Student.City_ID;
                    StudentModel.Building_Name = d.Student.Building_Name;
                    StudentModel.Country_ID = d.Student.Country_ID;
                    StudentModel.Current_Package_ID = d.Student.Current_Package_ID;
                    StudentModel.Disease = d.Student.Disease;
                    StudentModel.DOB = d.Student.DOB;
                    StudentModel.Email_ID = d.Student.Email_ID;
                    StudentModel.Emergency = d.Student.Emergency;
                    StudentModel.Full_Name = d.Student.First_Name + " " + d.Student.Last_Name;
                    StudentModel.ID = d.Student.ID;
                    StudentModel.Landmark = d.Student.Landmark;
                    StudentModel.Last_Name = d.Student.Last_Name;
                    StudentModel.Mobile = d.Student.Mobile;
                    StudentModel.Occupation = d.Student.Occupation;
                    StudentModel.Pin_Code = d.Student.Pin_Code;
                    StudentModel.Reference = d.Student.Reference;
                    StudentModel.Special_Instrucation = d.Student.Special_Instrucation;
                    StudentModel.Start_Date = d.Student.Start_Date;
                    StudentModel.State_ID = d.Student.State_ID;
                    StudentModel.Status = d.Student.Status;
                    trainerModel.Full_Name = d.Trainer.First_Name + " " + d.Trainer.Last_Name;
                    if (d.Student.Current_Package_ID != null)
                    {
                        var plane = db.Planes.Where(i => i.ID == d.Student.Current_Package_ID).FirstOrDefault();
                        PlaneModel.Name = plane.Name;
                    }
                    StudentTrainerPlaneModel.Trainer = trainerModel;
                    StudentTrainerPlaneModel.Student = StudentModel;
                    StudentTrainerPlaneModel.Plane = PlaneModel;
                    StudentTrainerPlaneModelList.Add(StudentTrainerPlaneModel);
                }
                return View(StudentTrainerPlaneModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Branch_Wise_Index(Int64 ID)
        {
            try
            {
                Session["ID"] = ID;
                var Students = db.Trainers.Join(db.Students, u => u.ID, j => j.Trainer_ID, (u, j) => new { Trainer = u, Student = j }).Where(i => i.Student.Branch_ID == ID).OrderByDescending(i => i.Student.Status).OrderByDescending(i => i.Student.ID).ToList();
                List<StudentTrainerPlaneModel> StudentTrainerPlaneModelList = new List<StudentTrainerPlaneModel>();
                foreach (var d in Students)
                {
                    StudentTrainerPlaneModel StudentTrainerPlaneModel = new StudentTrainerPlaneModel();
                    TrainerModel trainerModel = new TrainerModel();
                    StudentModel StudentModel = new StudentModel();
                    PlaneModel PlaneModel = new PlaneModel();
                    StudentModel.Area = d.Student.Area;
                    StudentModel.Branch_ID = d.Student.Branch_ID;
                    StudentModel.City_ID = d.Student.City_ID;
                    StudentModel.Building_Name = d.Student.Building_Name;
                    StudentModel.Country_ID = d.Student.Country_ID;
                    StudentModel.Current_Package_ID = d.Student.Current_Package_ID;
                    StudentModel.Disease = d.Student.Disease;
                    StudentModel.DOB = d.Student.DOB;
                    StudentModel.Email_ID = d.Student.Email_ID;
                    StudentModel.Emergency = d.Student.Emergency;
                    StudentModel.Full_Name = d.Student.First_Name + " " + d.Student.Last_Name;
                    StudentModel.ID = d.Student.ID;
                    StudentModel.Landmark = d.Student.Landmark;
                    StudentModel.Last_Name = d.Student.Last_Name;
                    StudentModel.Mobile = d.Student.Mobile;
                    StudentModel.Occupation = d.Student.Occupation;
                    StudentModel.Pin_Code = d.Student.Pin_Code;
                    StudentModel.Reference = d.Student.Reference;
                    StudentModel.Special_Instrucation = d.Student.Special_Instrucation;
                    StudentModel.Start_Date = d.Student.Start_Date;
                    StudentModel.State_ID = d.Student.State_ID;
                    StudentModel.Status = d.Student.Status;
                    trainerModel.Full_Name = d.Trainer.First_Name + " " + d.Trainer.Last_Name;
                    if (d.Student.Current_Package_ID != null)
                    {
                        var plane = db.Planes.Where(i => i.ID == d.Student.Current_Package_ID).FirstOrDefault();
                        PlaneModel.Name = plane.Name;
                    }
                    StudentTrainerPlaneModel.Trainer = trainerModel;
                    StudentTrainerPlaneModel.Student = StudentModel;
                    StudentTrainerPlaneModel.Plane = PlaneModel;
                    StudentTrainerPlaneModelList.Add(StudentTrainerPlaneModel);
                }
                return View(StudentTrainerPlaneModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Trainer_Wise_Index(Int64 ID)
        {
            try
            {
                Session["TID"] = ID;
                var Students = db.Trainers.Join(db.Students, u => u.ID, j => j.Trainer_ID, (u, j) => new { Trainer = u, Student = j }).Where(i => i.Student.Trainer_ID == ID).OrderByDescending(i => i.Student.Status).OrderByDescending(i => i.Student.ID).ToList();
                List<StudentTrainerPlaneModel> StudentTrainerPlaneModelList = new List<StudentTrainerPlaneModel>();
                foreach (var d in Students)
                {
                    StudentTrainerPlaneModel StudentTrainerPlaneModel = new StudentTrainerPlaneModel();
                    TrainerModel trainerModel = new TrainerModel();
                    StudentModel StudentModel = new StudentModel();
                    PlaneModel PlaneModel = new PlaneModel();
                    StudentModel.Area = d.Student.Area;
                    StudentModel.Branch_ID = d.Student.Branch_ID;
                    StudentModel.City_ID = d.Student.City_ID;
                    StudentModel.Building_Name = d.Student.Building_Name;
                    StudentModel.Country_ID = d.Student.Country_ID;
                    StudentModel.Current_Package_ID = d.Student.Current_Package_ID;
                    StudentModel.Disease = d.Student.Disease;
                    StudentModel.DOB = d.Student.DOB;
                    StudentModel.Email_ID = d.Student.Email_ID;
                    StudentModel.Emergency = d.Student.Emergency;
                    StudentModel.Full_Name = d.Student.First_Name + " " + d.Student.Last_Name;
                    StudentModel.ID = d.Student.ID;
                    StudentModel.Landmark = d.Student.Landmark;
                    StudentModel.Last_Name = d.Student.Last_Name;
                    StudentModel.Mobile = d.Student.Mobile;
                    StudentModel.Occupation = d.Student.Occupation;
                    StudentModel.Pin_Code = d.Student.Pin_Code;
                    StudentModel.Reference = d.Student.Reference;
                    StudentModel.Special_Instrucation = d.Student.Special_Instrucation;
                    StudentModel.Start_Date = d.Student.Start_Date;
                    StudentModel.State_ID = d.Student.State_ID;
                    StudentModel.Status = d.Student.Status;
                    trainerModel.Full_Name = d.Trainer.First_Name + " " + d.Trainer.Last_Name;
                    if (d.Student.Current_Package_ID != null)
                    {
                        var plane = db.Planes.Where(i => i.ID == d.Student.Current_Package_ID).FirstOrDefault();
                        PlaneModel.Name = plane.Name;
                    }
                    StudentTrainerPlaneModel.Trainer = trainerModel;
                    StudentTrainerPlaneModel.Student = StudentModel;
                    StudentTrainerPlaneModel.Plane = PlaneModel;
                    StudentTrainerPlaneModelList.Add(StudentTrainerPlaneModel);
                }
                return View(StudentTrainerPlaneModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Gym_Wise_Create()
        {
            try
            {
                StudentModel studentModel = new StudentModel();
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                studentModel.GYM_ID = GYM_ID;
                ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                var Trainer_NAME = db.Trainers.Where(i => i.ID == 0).ToList();
                List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                foreach (var Full_Name in Trainer_NAME)
                {
                    TrainerModel TrainerModel = new TrainerModel();
                    TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                    TrainerModel.ID = Full_Name.ID;
                    TrainerModelList.Add(TrainerModel);
                }
                ViewBag.Trainer_NAME = TrainerModelList;
                return View(studentModel);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Branch_Wise_Create()
        {
            try
            {
                StudentModel studentModel = new StudentModel();
                int Branch_ID = Convert.ToInt32(Session["ID"]);
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                studentModel.Branch_ID = Branch_ID;
                studentModel.GYM_ID = GYM_ID;
                ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Branch_ID).ToList();
                var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == Branch_ID && i.Role_ID == 3).ToList();
                List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                foreach (var Full_Name in Trainer_NAME)
                {
                    TrainerModel TrainerModel = new TrainerModel();
                    TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                    TrainerModel.ID = Full_Name.ID;
                    TrainerModelList.Add(TrainerModel);
                }
                ViewBag.Trainer_NAME = TrainerModelList;
                return View(studentModel);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Trainer_Wise_Create()
        {
            try
            {
                int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                int TID = Convert.ToInt32(Session["TID"]);
                ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Branvch_ID).ToList();
                StudentModel studentModel = new StudentModel();
                studentModel.Branch_ID = Branvch_ID;
                studentModel.Trainer_ID = TID;
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == GYM_ID && i.Role_ID == 3).ToList();
                List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                foreach (var Full_Name in Trainer_NAME)
                {
                    TrainerModel TrainerModel = new TrainerModel();
                    TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                    TrainerModel.ID = Full_Name.ID;
                    TrainerModelList.Add(TrainerModel);
                }
                ViewBag.Trainer_NAME = TrainerModelList;
                return View(studentModel);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Get_Trainer_Name(Int64 ID)
        {
            try
            {
                List<Trainer> TrainerDetail = new List<Trainer>();
                TrainerDetail = db.Trainers.Where(i => i.Branvch_ID == ID && i.Role_ID == 3).ToList();
                return Json(TrainerDetail.Select(x => new
                {
                    StateID = x.ID,
                    StateName = x.First_Name + " " + x.Last_Name
                }).ToList(), JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Create_From_Demo(Int64 ID)
        {
            try
            {
                Session["DID"] = ID;
                var demo = db.Demoes.Where(i => i.ID == ID).FirstOrDefault();
                if (demo != null)
                {
                    StudentModel studentModel = new StudentModel();
                    studentModel.Branch_ID = demo.Branch_ID;
                    ViewBag.Branch_Name = db.Branches.Where(i => i.ID == demo.Branch_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == demo.Branch_ID && i.Role_ID == 3).ToList();
                    List<StudentModel> StudentModelList = new List<StudentModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        StudentModel StudentModel = new StudentModel();
                        StudentModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        StudentModel.Trainer_ID = Full_Name.ID;
                        studentModel.First_Name = demo.First_Name;
                        studentModel.Last_Name = demo.Last_Name;
                        studentModel.Email_ID = demo.Email_ID;
                        studentModel.Current_Package_ID = demo.Interested_Plane_ID;
                        studentModel.Mobile = demo.Mobile;
                        StudentModelList.Add(StudentModel);
                    }
                    ViewBag.Trainer_NAME = StudentModelList;
                    return View(studentModel);
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
        public ActionResult Gym_Wise_Create(StudentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var studentcont = db.Students.Count();
                    var gym = db.GYMs.Where(i => i.ID == GYM_ID).FirstOrDefault();
                    var gymplan = db.GYM_Plan.Where(i => i.ID == gym.Plan_ID).FirstOrDefault();
                    if (studentcont <= 50)
                    {
                        var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        var students = db.Students.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        if (trainers == null && students == null && demo == null)
                        {
                            var Student = Mapper.Map<Student>(model);
                            Student.Start_Date = DateTime.Now;
                            Student.GYM_ID = GYM_ID;
                            db.Students.Add(Student);
                            db.SaveChanges();
                            TempData["Success"] = "Student Has Created Successfully.!";
                        }
                        else
                        {
                            StudentModel studentModel = new StudentModel();
                            studentModel.GYM_ID = model.GYM_ID;
                            ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                            var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                            List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                            foreach (var Full_Name in Trainer_NAME)
                            {
                                TrainerModel TrainerModel = new TrainerModel();
                                TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                                TrainerModel.ID = Full_Name.ID;
                                TrainerModelList.Add(TrainerModel);
                            }
                            ViewBag.Trainer_NAME = TrainerModelList;
                            TempData["Error"] = "MailId is alredy registered";
                            return View(studentModel);
                        }
                    }
                    else
                    {
                        StudentModel studentModel = new StudentModel();
                        studentModel.GYM_ID = model.GYM_ID;
                        ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                        var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                        List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                        foreach (var Full_Name in Trainer_NAME)
                        {
                            TrainerModel TrainerModel = new TrainerModel();
                            TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                            TrainerModel.ID = Full_Name.ID;
                            TrainerModelList.Add(TrainerModel);
                        }
                        ViewBag.Trainer_NAME = TrainerModelList;
                        TempData["Error"] = "MailId is alredy registered";
                        return View(studentModel);
                    }

                }
                else
                {
                    StudentModel studentModel = new StudentModel();
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    studentModel.GYM_ID = model.GYM_ID;
                    ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == GYM_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View(studentModel);
                }
                return RedirectToAction("Gym_Wise_Index", "Student", new { ID = model.GYM_ID });
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Branch_Wise_Create(StudentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var studentcont = db.Students.Count();
                    var gym = db.GYMs.Where(i => i.ID == GYM_ID).FirstOrDefault();
                    var gymplan = db.GYM_Plan.Where(i => i.ID == gym.Plan_ID).FirstOrDefault();
                    if (studentcont <= 50)
                    {
                        var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        var students = db.Students.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        if (trainers == null && students == null && demo == null)
                        {
                            var Student = Mapper.Map<Student>(model);
                            Student.GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                            Student.Branch_ID = model.Branch_ID;
                            Student.Start_Date = DateTime.Now;
                            db.Students.Add(Student);
                            db.SaveChanges();
                            TempData["Success"] = "Student Has Created Successfully.!";
                        }
                        else
                        {
                            StudentModel studentModel = new StudentModel();
                            studentModel.Branch_ID = model.Branch_ID;
                            ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                            var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                            List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                            foreach (var Full_Name in Trainer_NAME)
                            {
                                TrainerModel TrainerModel = new TrainerModel();
                                TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                                TrainerModel.ID = Full_Name.ID;
                                TrainerModelList.Add(TrainerModel);
                            }
                            ViewBag.Trainer_NAME = TrainerModelList;
                            TempData["Error"] = "MailId is alredy registered";
                            return View(studentModel);
                        }
                    }
                    else
                    {
                        StudentModel studentModel = new StudentModel();
                        studentModel.Branch_ID = model.Branch_ID;
                        ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                        var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                        List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                        foreach (var Full_Name in Trainer_NAME)
                        {
                            TrainerModel TrainerModel = new TrainerModel();
                            TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                            TrainerModel.ID = Full_Name.ID;
                            TrainerModelList.Add(TrainerModel);
                        }
                        ViewBag.Trainer_NAME = TrainerModelList;
                        TempData["Error"] = "Please Fill All Required Details.!";
                        return View(studentModel);
                    }
                }
                else
                {
                    StudentModel studentModel = new StudentModel();
                    studentModel.Branch_ID = model.Branch_ID;
                    ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View(studentModel);
                }
                return RedirectToAction("Branch_Wise_Index", "Student", new { ID = model.Branch_ID });
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Trainer_Wise_Create(StudentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var studentcont = db.Students.Count();
                    var gym = db.GYMs.Where(i => i.ID == GYM_ID).FirstOrDefault();
                    var gymplan = db.GYM_Plan.Where(i => i.ID == gym.Plan_ID).FirstOrDefault();
                    if (studentcont <= 50)
                    {
                        var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        var students = db.Students.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        if (trainers == null && students == null && demo == null)
                        {
                            var Student = Mapper.Map<Student>(model);
                            Student.GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                            Student.Start_Date = DateTime.Now;
                            db.Students.Add(Student);
                            db.SaveChanges();
                            TempData["Success"] = "Student Has Created Successfully.!";
                        }
                        else
                        {
                            int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                            int TID = Convert.ToInt32(Session["TID"]);
                            ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Branvch_ID).ToList();
                            StudentModel studentModel = new StudentModel();
                            studentModel.Branch_ID = Branvch_ID;
                            studentModel.Trainer_ID = TID;
                            ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                            var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == GYM_ID && i.Role_ID == 3).ToList();
                            List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                            foreach (var Full_Name in Trainer_NAME)
                            {
                                TrainerModel TrainerModel = new TrainerModel();
                                TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                                TrainerModel.ID = Full_Name.ID;
                                TrainerModelList.Add(TrainerModel);
                            }
                            ViewBag.Trainer_NAME = TrainerModelList;
                            TempData["Error"] = "MailId is alredy registered";
                            return View(studentModel);
                        }
                    }
                    else
                    {
                        int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                        int TID = Convert.ToInt32(Session["TID"]);
                        ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Branvch_ID).ToList();
                        StudentModel studentModel = new StudentModel();
                        studentModel.Branch_ID = Branvch_ID;
                        studentModel.Trainer_ID = TID;
                        ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                        var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == GYM_ID && i.Role_ID == 3).ToList();
                        List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                        foreach (var Full_Name in Trainer_NAME)
                        {
                            TrainerModel TrainerModel = new TrainerModel();
                            TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                            TrainerModel.ID = Full_Name.ID;
                            TrainerModelList.Add(TrainerModel);
                        }
                        ViewBag.Trainer_NAME = TrainerModelList;
                        TempData["Error"] = "Please Fill All Required Details.!";
                        return View(studentModel);
                    }
                }
                else
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                    int TID = Convert.ToInt32(Session["TID"]);
                    ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Branvch_ID).ToList();
                    StudentModel studentModel = new StudentModel();
                    studentModel.Branch_ID = Branvch_ID;
                    studentModel.Trainer_ID = TID;
                    ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == GYM_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View(studentModel);
                }
                return RedirectToAction("Trainer_Wise_Index", "Student", new { ID = model.Trainer_ID });
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_From_Demo(StudentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var studentcont = db.Students.Count();
                    var gym = db.GYMs.Where(i => i.ID == GYM_ID).FirstOrDefault();
                    var gymplan = db.GYM_Plan.Where(i => i.ID == gym.Plan_ID).FirstOrDefault();
                    if (studentcont <= 50)
                    {
                        int DID = Convert.ToInt32(Session["DID"]);
                        var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        var students = db.Students.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                        var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID && i.ID != DID).FirstOrDefault();
                        if (trainers == null && students == null && demo == null)
                        {
                            var Student = Mapper.Map<Student>(model);
                            Student.GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                            Student.Branch_ID = model.Branch_ID;
                            Student.Start_Date = DateTime.Now;
                            db.Students.Add(Student);
                            db.SaveChanges();

                            new Thread(new ThreadStart(() =>
                            {
                                var Demo = db.Demoes.Where(i => i.ID == DID).FirstOrDefault();
                                db.Demoes.Remove(Demo);
                                db.SaveChanges();
                            })).Start();

                            TempData["Success"] = "Student Has Created Successfully.!";
                        }
                        else
                        {
                            StudentModel studentModel = new StudentModel();
                            studentModel.Branch_ID = model.Branch_ID;
                            ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                            var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                            List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                            foreach (var Full_Name in Trainer_NAME)
                            {
                                TrainerModel TrainerModel = new TrainerModel();
                                TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                                TrainerModel.ID = Full_Name.ID;
                                TrainerModelList.Add(TrainerModel);
                            }
                            ViewBag.Trainer_NAME = TrainerModelList;
                            TempData["Error"] = "MailId is alredy registered";
                            return View(studentModel);
                        }
                    }
                    else
                    {
                        StudentModel studentModel = new StudentModel();
                        studentModel.Branch_ID = model.Branch_ID;
                        var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                        ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                        List<StudentModel> StudentModelList = new List<StudentModel>();
                        foreach (var Full_Name in Trainer_NAME)
                        {
                            StudentModel StudentModel = new StudentModel();
                            StudentModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                            StudentModel.Trainer_ID = Full_Name.ID;
                            StudentModelList.Add(StudentModel);
                        }
                        ViewBag.Trainer_NAME = StudentModelList;
                        TempData["Error"] = "Please Fill All Required Details.!";
                        return View(studentModel);
                    }
                }
                else
                {
                    StudentModel studentModel = new StudentModel();
                    studentModel.Branch_ID = model.Branch_ID;
                    var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                    ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                    List<StudentModel> StudentModelList = new List<StudentModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        StudentModel StudentModel = new StudentModel();
                        StudentModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        StudentModel.Trainer_ID = Full_Name.ID;
                        StudentModelList.Add(StudentModel);
                    }
                    ViewBag.Trainer_NAME = StudentModelList;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View(studentModel);
                }
                return RedirectToAction("Index", "Demo");
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Gym_Wise_Edit(Int64 ID)
        {
            var Details = db.Students.Where(b => b.ID == ID).FirstOrDefault();
            if (Details != null)
            {
                ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == Details.GYM_ID).ToList();
                var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == Details.Branch_ID && i.Role_ID == 3).ToList();
                List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                foreach (var Full_Name in Trainer_NAME)
                {
                    TrainerModel TrainerModel = new TrainerModel();
                    TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                    TrainerModel.ID = Full_Name.ID;
                    TrainerModelList.Add(TrainerModel);
                }
                ViewBag.Trainer_NAME = TrainerModelList;
                var Student = Mapper.Map<StudentModel>(Details);
                return View(Student);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Branch_Wise_Edit(Int64 ID)
        {
            try
            {
                var Details = db.Students.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    int Branch_ID = Convert.ToInt32(Session["ID"]);
                    ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Branch_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == Branch_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    var Student = Mapper.Map<StudentModel>(Details);
                    Student.Branch_ID = Details.Branch_ID;
                    return View(Student);
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

        [HttpGet]
        public ActionResult Trainer_Wise_Edit(Int64 ID)
        {
            try
            {
                var Details = db.Students.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Details.Branch_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == Details.GYM_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    var Student = Mapper.Map<StudentModel>(Details);
                    return View(Student);
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
        [ValidateAntiForgeryToken]
        public ActionResult Gym_Wise_Edit(StudentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    var students = db.Students.Where(i => i.Email_ID == model.Email_ID && i.ID != model.ID).FirstOrDefault();
                    var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    if (trainers == null && students == null && demo == null)
                    {
                        var dataExists = db.Students.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (dataExists != null)
                        {
                            dataExists.First_Name = model.First_Name;
                            dataExists.Last_Name = model.Last_Name;
                            dataExists.Area = model.Area;
                            dataExists.Building_Name = model.Building_Name;
                            dataExists.Email_ID = model.Email_ID;
                            dataExists.Emergency = model.Emergency;
                            dataExists.Landmark = model.Landmark;
                            dataExists.Mobile = model.Mobile;
                            dataExists.Pin_Code = model.Pin_Code;
                            dataExists.Disease = model.Disease;
                            dataExists.Trainer_ID = model.Trainer_ID;
                            dataExists.DOB = model.DOB;
                            dataExists.Occupation = model.Occupation;
                            dataExists.Pin_Code = model.Pin_Code;
                            dataExists.Reference = model.Reference;
                            dataExists.Special_Instrucation = model.Special_Instrucation;
                            db.SaveChanges();
                            TempData["Success"] = "Student Has Updated Successfully.!";
                        }
                    }
                    else
                    {
                        StudentModel studentModel = new StudentModel();
                        studentModel.Branch_ID = model.Branch_ID;
                        studentModel.Trainer_ID = model.Trainer_ID;
                        ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                        var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                        List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                        foreach (var Full_Name in Trainer_NAME)
                        {
                            TrainerModel TrainerModel = new TrainerModel();
                            TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                            TrainerModel.ID = Full_Name.ID;
                            TrainerModelList.Add(TrainerModel);
                        }
                        ViewBag.Trainer_NAME = TrainerModelList;
                        TempData["Error"] = "MailId is alredy registered";
                        return View(studentModel);
                    }
                }
                else
                {
                    StudentModel studentModel = new StudentModel();
                    studentModel.Branch_ID = model.Branch_ID;
                    studentModel.Trainer_ID = model.Trainer_ID;
                    ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View(studentModel);
                }
                return RedirectToAction("Gym_Wise_Index", "Student", new { ID = model.GYM_ID });
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Branch_Wise_Edit(StudentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    var students = db.Students.Where(i => i.Email_ID == model.Email_ID && i.ID != model.ID).FirstOrDefault();
                    var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    if (trainers == null && students == null && demo == null)
                    {
                        var dataExists = db.Students.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (dataExists != null)
                        {
                            dataExists.First_Name = model.First_Name;
                            dataExists.Last_Name = model.Last_Name;
                            dataExists.Area = model.Area;
                            dataExists.Building_Name = model.Building_Name;
                            dataExists.Email_ID = model.Email_ID;
                            dataExists.Emergency = model.Emergency;
                            dataExists.Landmark = model.Landmark;
                            dataExists.Mobile = model.Mobile;
                            dataExists.Pin_Code = model.Pin_Code;
                            dataExists.Disease = model.Disease;
                            dataExists.Trainer_ID = model.Trainer_ID;
                            dataExists.DOB = model.DOB;
                            dataExists.Occupation = model.Occupation;
                            dataExists.Pin_Code = model.Pin_Code;
                            dataExists.Reference = model.Reference;
                            dataExists.Special_Instrucation = model.Special_Instrucation;
                            db.SaveChanges();
                            TempData["Success"] = "Student Has Updated Successfully.!";
                        }
                    }
                    else
                    {
                        StudentModel studentModel = new StudentModel();
                        studentModel.Branch_ID = model.Branch_ID;
                        ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                        var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                        List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                        foreach (var Full_Name in Trainer_NAME)
                        {
                            TrainerModel TrainerModel = new TrainerModel();
                            TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                            TrainerModel.ID = Full_Name.ID;
                            TrainerModelList.Add(TrainerModel);
                        }
                        ViewBag.Trainer_NAME = TrainerModelList;
                        TempData["Error"] = "MailId is alredy registered";
                        return View(studentModel);
                    }
                }
                else
                {
                    StudentModel studentModel = new StudentModel();
                    studentModel.Branch_ID = model.Branch_ID;
                    ViewBag.Branch_Name = db.Branches.Where(i => i.ID == model.Branch_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == model.Branch_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View(studentModel);
                }
                return RedirectToAction("Branch_Wise_Index", "Student", new { ID = model.Branch_ID });
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Trainer_Wise_Edit(StudentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    var students = db.Students.Where(i => i.Email_ID == model.Email_ID && i.ID != model.ID).FirstOrDefault();
                    var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    if (trainers == null && students == null && demo == null)
                    {
                        var dataExists = db.Students.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (dataExists != null)
                        {
                            dataExists.First_Name = model.First_Name;
                            dataExists.Last_Name = model.Last_Name;
                            dataExists.Area = model.Area;
                            dataExists.Building_Name = model.Building_Name;
                            dataExists.Email_ID = model.Email_ID;
                            dataExists.Emergency = model.Emergency;
                            dataExists.Landmark = model.Landmark;
                            dataExists.Mobile = model.Mobile;
                            dataExists.Pin_Code = model.Pin_Code;
                            dataExists.Disease = model.Disease;
                            dataExists.Trainer_ID = model.Trainer_ID;
                            dataExists.DOB = model.DOB;
                            dataExists.Occupation = model.Occupation;
                            dataExists.Pin_Code = model.Pin_Code;
                            dataExists.Reference = model.Reference;
                            dataExists.Special_Instrucation = model.Special_Instrucation;
                            db.SaveChanges();
                            TempData["Success"] = "Student Has Updated Successfully.!";
                        }
                    }
                    else
                    {
                        var Details = db.Students.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (Details != null)
                        {
                            ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Details.Branch_ID).ToList();
                            var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == Details.GYM_ID && i.Role_ID == 3).ToList();
                            List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                            foreach (var Full_Name in Trainer_NAME)
                            {
                                TrainerModel TrainerModel = new TrainerModel();
                                TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                                TrainerModel.ID = Full_Name.ID;
                                TrainerModelList.Add(TrainerModel);
                            }
                            ViewBag.Trainer_NAME = TrainerModelList;
                            var Student = Mapper.Map<StudentModel>(Details);
                            TempData["Error"] = "MailId is alredy registered";
                            return View(Student);
                        }
                        else
                        {
                            TempData["Error"] = "MailId is alredy registered";
                            return View();
                        }
                    }
                }
                else
                {
                    var Details = db.Students.Where(b => b.ID == model.ID).FirstOrDefault();
                    if (Details != null)
                    {
                        ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Details.Branch_ID).ToList();
                        var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == Details.GYM_ID && i.Role_ID == 3).ToList();
                        List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                        foreach (var Full_Name in Trainer_NAME)
                        {
                            TrainerModel TrainerModel = new TrainerModel();
                            TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                            TrainerModel.ID = Full_Name.ID;
                            TrainerModelList.Add(TrainerModel);
                        }
                        ViewBag.Trainer_NAME = TrainerModelList;
                        var Student = Mapper.Map<StudentModel>(Details);
                        TempData["Error"] = "Please Fill All Required Details.!";
                        return View(Student);
                    }
                    else
                    {
                        TempData["Error"] = "Please Fill All Required Details.!";
                        return View();
                    }
                }
                return RedirectToAction("Trainer_Wise_Index", "Student", new { ID = model.Trainer_ID });
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
                var Student = db.Students.Where(i => i.ID == ID).FirstOrDefault();
                db.Students.Remove(Student);

                var Fees = db.Fees.Where(i => i.Student_ID == ID).ToList();
                foreach (var Fee in Fees)
                {
                    db.Fees.Remove(Fee);
                    db.SaveChanges();
                }

                db.SaveChanges();
                TempData["Success"] = "Student Has Deleted Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Gym_Wise_View(Int64 ID)
        {
            try
            {
                var Details = db.Students.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    ViewBag.Branch_Name = db.Branches.Where(i => i.ID == Details.Branch_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.Branvch_ID == Details.Branch_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    var Students = Mapper.Map<StudentModel>(Details);
                    return View(Students);
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

        [HttpGet]
        public ActionResult Branch_Wise_View(Int64 ID)
        {
            try
            {
                var Details = db.Students.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == Details.GYM_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == Details.GYM_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    var Students = Mapper.Map<StudentModel>(Details);
                    return View(Students);
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

        [HttpGet]
        public ActionResult Trainer_Wise_View(Int64 ID)
        {
            try
            {
                var Details = db.Students.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    ViewBag.Branch_Name = db.Branches.Where(i => i.GYM_ID == Details.GYM_ID).ToList();
                    var Trainer_NAME = db.Trainers.Where(i => i.GYM_ID == Details.GYM_ID && i.Role_ID == 3).ToList();
                    List<TrainerModel> TrainerModelList = new List<TrainerModel>();
                    foreach (var Full_Name in Trainer_NAME)
                    {
                        TrainerModel TrainerModel = new TrainerModel();
                        TrainerModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        TrainerModel.ID = Full_Name.ID;
                        TrainerModelList.Add(TrainerModel);
                    }
                    ViewBag.Trainer_NAME = TrainerModelList;
                    var Students = Mapper.Map<StudentModel>(Details);
                    return View(Students);
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

        [HttpGet]
        public ActionResult New_Student(Int64 ID)
        {
            try
            {
                Session["ID"] = ID;
                var Students = db.Trainers.Join(db.Students, u => u.ID, j => j.Trainer_ID, (u, j) => new { Trainer = u, Student = j }).Where(i => i.Student.Branch_ID == ID && DbFunctions.TruncateTime(i.Student.Start_Date) == DbFunctions.TruncateTime(DateTime.Now)).OrderByDescending(i => i.Student.Status).ToList();
                List<StudentTrainerPlaneModel> StudentTrainerPlaneModelList = new List<StudentTrainerPlaneModel>();
                foreach (var d in Students)
                {
                    StudentTrainerPlaneModel StudentTrainerPlaneModel = new StudentTrainerPlaneModel();
                    TrainerModel trainerModel = new TrainerModel();
                    StudentModel StudentModel = new StudentModel();
                    PlaneModel PlaneModel = new PlaneModel();
                    StudentModel.Area = d.Student.Area;
                    StudentModel.Branch_ID = d.Student.Branch_ID;
                    StudentModel.City_ID = d.Student.City_ID;
                    StudentModel.Building_Name = d.Student.Building_Name;
                    StudentModel.Country_ID = d.Student.Country_ID;
                    StudentModel.Current_Package_ID = d.Student.Current_Package_ID;
                    StudentModel.Disease = d.Student.Disease;
                    StudentModel.DOB = d.Student.DOB;
                    StudentModel.Email_ID = d.Student.Email_ID;
                    StudentModel.Emergency = d.Student.Emergency;
                    StudentModel.Full_Name = d.Student.First_Name + " " + d.Student.Last_Name;
                    StudentModel.ID = d.Student.ID;
                    StudentModel.Landmark = d.Student.Landmark;
                    StudentModel.Last_Name = d.Student.Last_Name;
                    StudentModel.Mobile = d.Student.Mobile;
                    StudentModel.Occupation = d.Student.Occupation;
                    StudentModel.Pin_Code = d.Student.Pin_Code;
                    StudentModel.Reference = d.Student.Reference;
                    StudentModel.Special_Instrucation = d.Student.Special_Instrucation;
                    StudentModel.Start_Date = d.Student.Start_Date;
                    StudentModel.State_ID = d.Student.State_ID;
                    StudentModel.Status = d.Student.Status;
                    trainerModel.Full_Name = d.Student.First_Name + " " + d.Student.Last_Name;
                    if (d.Student.Current_Package_ID != null)
                    {
                        var plane = db.Planes.Where(i => i.ID == d.Student.Current_Package_ID).FirstOrDefault();
                        PlaneModel.Name = plane.Name;
                    }
                    StudentTrainerPlaneModel.Trainer = trainerModel;
                    StudentTrainerPlaneModel.Student = StudentModel;
                    StudentTrainerPlaneModel.Plane = PlaneModel;
                    StudentTrainerPlaneModelList.Add(StudentTrainerPlaneModel);
                }
                return View(StudentTrainerPlaneModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }
    }
}