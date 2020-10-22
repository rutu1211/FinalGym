using The_Gym.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace The_Gym.Controllers
{
    [Authorize]
    public class TrainerController : Controller
    {
        private The_GymEntities db = new The_GymEntities();

        [HttpGet]
        public ActionResult GYM_Wise_Trainer_Index(Int64 ID)
        {
            try
            {
                Session["IDS"] = ID;
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var demo = db.Trainers.Join(db.Branches, u => u.Branvch_ID, j => j.ID, (u, j) => new { Trainer = u, Branch = j }).Where(i => i.Branch.ID == i.Trainer.Branvch_ID && i.Trainer.Role_ID == ID && i.Trainer.GYM_ID == GYM_ID).OrderByDescending(i => i.Trainer.ID).ToList();
                List<TrainerBranchModel> TrainerBranchModelList = new List<TrainerBranchModel>();
                foreach (var d in demo)
                {
                    TrainerBranchModel trainerBranchModel = new TrainerBranchModel();
                    BranchModel branchModel = new BranchModel();
                    TrainerModel trainerModel = new TrainerModel();
                    branchModel.Name = d.Branch.Name;
                    trainerModel.Branvch_ID = d.Trainer.Branvch_ID;
                    trainerModel.Email_ID = d.Trainer.Email_ID;
                    trainerModel.First_Name = d.Trainer.First_Name;
                    trainerModel.Last_Name = d.Trainer.Last_Name;
                    trainerModel.ID = d.Trainer.ID;
                    trainerModel.Role_ID = d.Trainer.Role_ID;
                    trainerBranchModel.Trainer = trainerModel;
                    trainerBranchModel.Branch = branchModel;
                    TrainerBranchModelList.Add(trainerBranchModel);
                }
                return View(TrainerBranchModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Branch_Wise_Trainer_Index(Int64 ID, Int64 IDS)
        {
            try
            {
                Session["ID"] = ID;
                Session["IDS"] = IDS;
                var demo = db.Trainers.Join(db.Branches, u => u.Branvch_ID, j => j.ID, (u, j) => new { Trainer = u, Branch = j }).Where(i => i.Branch.ID == i.Trainer.Branvch_ID && i.Trainer.Role_ID == IDS && i.Branch.ID == ID).OrderByDescending(i => i.Trainer.ID).ToList();
                List<TrainerBranchModel> TrainerBranchModelList = new List<TrainerBranchModel>();
                foreach (var d in demo)
                {
                    TrainerBranchModel trainerBranchModel = new TrainerBranchModel();
                    BranchModel branchModel = new BranchModel();
                    TrainerModel trainerModel = new TrainerModel();
                    branchModel.Name = d.Branch.Name;
                    trainerModel.Branvch_ID = d.Trainer.Branvch_ID;
                    trainerModel.Email_ID = d.Trainer.Email_ID;
                    trainerModel.First_Name = d.Trainer.First_Name;
                    trainerModel.Last_Name = d.Trainer.Last_Name;
                    trainerModel.ID = d.Trainer.ID;
                    trainerModel.Role_ID = d.Trainer.Role_ID;
                    trainerBranchModel.Trainer = trainerModel;
                    trainerBranchModel.Branch = branchModel;
                    TrainerBranchModelList.Add(trainerBranchModel);
                }
                return View(TrainerBranchModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult GYM_Wise_Trainer_Create()
        {
            try
            {
                TrainerModel trainerModel = new TrainerModel();
                if (Convert.ToInt32(Session["IDS"]) == 2)
                {
                    ViewBag.trainer = "Create Manager";
                    trainerModel.Role_ID = 2;
                }
                if (Convert.ToInt32(Session["IDS"]) == 3)
                {
                    ViewBag.trainer = "Create Trainer";
                    trainerModel.Role_ID = 3;
                }
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID && i.IS_Active == true).ToList();
                ViewBag.Branch_NAME = Branches;
                return View(trainerModel);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Branch_Wise_Trainer_Create()
        {
            try
            {
                TrainerModel trainerModel = new TrainerModel();
                if (Convert.ToInt32(Session["IDS"]) == 2)
                {
                    ViewBag.trainer = "Create Manager";
                    trainerModel.Role_ID = 2;
                }
                if (Convert.ToInt32(Session["IDS"]) == 3)
                {
                    ViewBag.trainer = "Create Trainer";
                    trainerModel.Role_ID = 3;
                }
                var Branches = db.Branches.ToList();
                ViewBag.Branch_NAME = Branches;
                trainerModel.Branvch_ID = Convert.ToInt32(Session["ID"]);
                return View(trainerModel);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        public ActionResult GYM_Wise_Trainer_Create(TrainerModel model)
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
                        var Trainer = Mapper.Map<Trainer>(model);
                        Trainer.Role_ID = Convert.ToInt32(Session["IDS"]);
                        Trainer.GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                        if (Convert.ToInt32(Session["IDS"]) == 2)
                        {
                            Trainer.Password = GeneratePasswordModel.GeneratePassword(3, 3, 3);
                            //Trainer.Password = "admin";
                            var gym = db.GYMs.Where(i => i.ID == Trainer.GYM_ID).FirstOrDefault();
                            SendMailModel.OTP(Trainer.Password, model.First_Name + " " + model.Last_Name, model.Email_ID, gym.Name);
                            TempData["Success"] = "Manager Has Created Successfully.!";
                        }
                        if (Convert.ToInt32(Session["IDS"]) == 3)
                        {
                            TempData["Success"] = "Trainer Has Created Successfully.!";
                        }
                        db.Trainers.Add(Trainer);
                        db.SaveChanges();
                        
                    }
                    else
                    {
                        TrainerModel trainerModel = new TrainerModel();
                        if (Convert.ToInt32(Session["IDS"]) == 2)
                        {
                            ViewBag.trainer = "Create Manager";
                            trainerModel.Role_ID = 2;
                        }
                        if (Convert.ToInt32(Session["IDS"]) == 3)
                        {
                            ViewBag.trainer = "Create Trainer";
                            trainerModel.Role_ID = 3;
                        }
                        int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                        var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID && i.IS_Active == true).ToList();
                        ViewBag.Branch_NAME = Branches;
                        TempData["Error"] = "MailId is alredy registered";
                        return View(trainerModel);
                    }
                }
                else
                {
                    TrainerModel trainerModel = new TrainerModel();
                    if (Convert.ToInt32(Session["IDS"]) == 2)
                    {
                        ViewBag.trainer = "Create Manager";
                        trainerModel.Role_ID = 2;
                    }
                    if (Convert.ToInt32(Session["IDS"]) == 3)
                    {
                        ViewBag.trainer = "Create Trainer";
                        trainerModel.Role_ID = 3;
                    }
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID && i.IS_Active == true).ToList();
                    ViewBag.Branch_NAME = Branches;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View(trainerModel);
                }
                return RedirectToAction("GYM_Wise_Trainer_Index", "Trainer", new { ID = Session["IDS"] });
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        public ActionResult Branch_Wise_Trainer_Create(TrainerModel model)
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
                        var Trainer = Mapper.Map<Trainer>(model);
                        Trainer.Role_ID = Convert.ToInt32(Session["IDS"]);
                        Trainer.GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                        Trainer.Branvch_ID = Convert.ToInt32(Session["ID"]);
                        if (Convert.ToInt32(Session["IDS"]) == 2)
                        {
                            Trainer.Password = GeneratePasswordModel.GeneratePassword(3, 3, 3);
                            //Trainer.Password = "admin";
                            var gym = db.GYMs.Where(i => i.ID == Trainer.GYM_ID).FirstOrDefault();
                            SendMailModel.OTP(Trainer.Password, model.First_Name + " " + model.Last_Name, model.Email_ID, gym.Name);
                            TempData["Success"] = "Manager Has Created Successfully.!";
                        }
                        if (Convert.ToInt32(Session["IDS"]) == 3)
                        {
                            TempData["Success"] = "Trainer Has Created Successfully.!";
                        }
                        db.Trainers.Add(Trainer);
                        db.SaveChanges();
                    }
                    else
                    {
                        TrainerModel trainerModel = new TrainerModel();
                        if (Convert.ToInt32(Session["IDS"]) == 2)
                        {
                            ViewBag.trainer = "Create Manager";
                            trainerModel.Role_ID = 2;
                        }
                        if (Convert.ToInt32(Session["IDS"]) == 3)
                        {
                            ViewBag.trainer = "Create Trainer";
                            trainerModel.Role_ID = 3;
                        }
                        var Branches = db.Branches.ToList();
                        ViewBag.Branch_NAME = Branches;
                        trainerModel.Branvch_ID = Convert.ToInt32(Session["ID"]);
                        TempData["Error"] = "MailId is alredy registered";
                        return View(trainerModel);
                    }
                }
                else
                {
                    TrainerModel trainerModel = new TrainerModel();
                    if (Convert.ToInt32(Session["IDS"]) == 2)
                    {
                        ViewBag.trainer = "Create Manager";
                        trainerModel.Role_ID = 2;
                    }
                    if (Convert.ToInt32(Session["IDS"]) == 3)
                    {
                        ViewBag.trainer = "Create Trainer";
                        trainerModel.Role_ID = 3;
                    }
                    var Branches = db.Branches.ToList();
                    ViewBag.Branch_NAME = Branches;
                    trainerModel.Branvch_ID = Convert.ToInt32(Session["ID"]);
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View(trainerModel);
                }
                return RedirectToAction("Branch_Wise_Trainer_Index", "Trainer", new { ID = Session["ID"], IDS = Session["IDS"] });
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult GYM_Wise_Trainer_Edit(Int64 ID)
        {
            try
            {
                if (Convert.ToInt32(Session["IDS"]) == 2)
                {
                    ViewBag.trainer = "Edit Manager";
                }
                if (Convert.ToInt32(Session["IDS"]) == 3)
                {
                    ViewBag.trainer = "Edit Trainer";
                }
                var Details = db.Trainers.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID && i.IS_Active == true).ToList();
                    ViewBag.Branch_NAME = Branches;
                    var Trainer = Mapper.Map<TrainerModel>(Details);
                    return View(Trainer); ;
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
        public ActionResult Branch_Wise_Trainer_Edit(Int64 ID)
        {
            try
            {
                if (Convert.ToInt32(Session["IDS"]) == 2)
                {
                    ViewBag.trainer = "Edit Manager";
                }
                if (Convert.ToInt32(Session["IDS"]) == 3)
                {
                    ViewBag.trainer = "Edit Trainer";
                }
                var Details = db.Trainers.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    TrainerModel Trainer = new TrainerModel();
                    var Branches = db.Branches.ToList();
                    ViewBag.Branch_NAME = Branches;
                    Trainer.Branvch_ID = Convert.ToInt32(Session["ID"]);
                    var Trainers = Mapper.Map<TrainerModel>(Details);
                    return View(Trainers); ;
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
        public ActionResult GYM_Wise_Trainer_Edit(TrainerModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID && i.ID != model.ID).FirstOrDefault();
                    var students = db.Students.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    if (trainers == null && students == null && demo == null)
                    {
                        var dataExists = db.Trainers.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (dataExists != null)
                        {
                            dataExists.Branvch_ID = model.Branvch_ID;
                            dataExists.Email_ID = model.Email_ID;
                            dataExists.First_Name = model.First_Name;
                            dataExists.Last_Name = model.Last_Name;
                            db.SaveChanges();
                            if(dataExists.Role_ID == 2)
                            {
                                TempData["Success"] = "Manager Has Updated Successfully.!";
                            }
                            if (dataExists.Role_ID == 3)
                            {
                                TempData["Success"] = "Trainer Has Updated Successfully.!";
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["IDS"]) == 2)
                        {
                            ViewBag.trainer = "Edit Manager";
                        }
                        if (Convert.ToInt32(Session["IDS"]) == 3)
                        {
                            ViewBag.trainer = "Edit Trainer";
                        }
                        var Details = db.Trainers.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (Details != null)
                        {
                            int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                            var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID && i.IS_Active == true).ToList();
                            ViewBag.Branch_NAME = Branches;
                            var Trainer = Mapper.Map<TrainerModel>(Details);
                            TempData["Error"] = "MailId is alredy registered";
                            return View(Trainer); ;
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
                    if (Convert.ToInt32(Session["IDS"]) == 2)
                    {
                        ViewBag.trainer = "Edit Manager";
                    }
                    if (Convert.ToInt32(Session["IDS"]) == 3)
                    {
                        ViewBag.trainer = "Edit Trainer";
                    }
                    var Details = db.Trainers.Where(b => b.ID == model.ID).FirstOrDefault();
                    if (Details != null)
                    {
                        int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                        var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID && i.IS_Active == true).ToList();
                        ViewBag.Branch_NAME = Branches;
                        var Trainer = Mapper.Map<TrainerModel>(Details);
                        TempData["Error"] = "Please Fill All Required Details.!";
                        return View(Trainer);
                    }
                    else
                    {
                        TempData["Error"] = "Please Fill All Required Details.!";
                        return View();
                    }
                }
                return RedirectToAction("GYM_Wise_Trainer_Index", "Trainer", new { ID = Session["IDS"] });
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        public ActionResult Branch_Wise_Trainer_Edit(TrainerModel model, string Save, string Cancel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var trainers = db.Trainers.Where(i => i.Email_ID == model.Email_ID && i.ID != model.ID).FirstOrDefault();
                    var students = db.Students.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    var demo = db.Demoes.Where(i => i.Email_ID == model.Email_ID).FirstOrDefault();
                    if (trainers == null && students == null && demo == null)
                    {
                        var dataExists = db.Trainers.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (dataExists != null)
                        {
                            dataExists.Email_ID = model.Email_ID;
                            dataExists.First_Name = model.First_Name;
                            dataExists.Last_Name = model.Last_Name;
                            db.SaveChanges();
                            if (dataExists.Role_ID == 2)
                            {
                                TempData["Success"] = "Manager Has Updated Successfully.!";
                            }
                            if (dataExists.Role_ID == 3)
                            {
                                TempData["Success"] = "Trainer Has Updated Successfully.!";
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(Session["IDS"]) == 2)
                        {
                            ViewBag.trainer = "Edit Manager";
                        }
                        if (Convert.ToInt32(Session["IDS"]) == 3)
                        {
                            ViewBag.trainer = "Edit Trainer";
                        }
                        var Details = db.Trainers.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (Details != null)
                        {
                            TrainerModel Trainer = new TrainerModel();
                            var Branches = db.Branches.ToList();
                            ViewBag.Branch_NAME = Branches;
                            Trainer.Branvch_ID = Convert.ToInt32(Session["ID"]);
                            var Trainers = Mapper.Map<TrainerModel>(Details);
                            TempData["Error"] = "MailId is alredy registered";
                            return View(Trainers); ;
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
                    if (Convert.ToInt32(Session["IDS"]) == 2)
                    {
                        ViewBag.trainer = "Edit Manager";
                    }
                    if (Convert.ToInt32(Session["IDS"]) == 3)
                    {
                        ViewBag.trainer = "Edit Trainer";
                    }
                    var Details = db.Trainers.Where(b => b.ID == model.ID).FirstOrDefault();
                    if (Details != null)
                    {
                        TrainerModel Trainer = new TrainerModel();
                        var Branches = db.Branches.ToList();
                        ViewBag.Branch_NAME = Branches;
                        Trainer.Branvch_ID = Convert.ToInt32(Session["ID"]);
                        var Trainers = Mapper.Map<TrainerModel>(Details);
                        TempData["Error"] = "Please Fill All Required Details.!";
                        return View(Trainers); ;
                    }
                    else
                    {
                        TempData["Error"] = "Please Fill All Required Details.!";
                        return View();
                    }
                }
                return RedirectToAction("Branch_Wise_Trainer_Index", "Trainer", new { ID = Session["ID"], IDS = Session["IDS"] });
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Trainer_Delete(Int64 ID)
        {
            try
            {
                var Trainer = db.Trainers.Where(i => i.ID == ID).FirstOrDefault();
                db.Trainers.Remove(Trainer);
                db.SaveChanges();
                TempData["Success"] = "Diet Has Created Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult ManagerView(Int64 ID)
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                ViewBag.Branch_NAME = Branches;
                var Details = db.Trainers.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    var Admin = Mapper.Map<TrainerModel>(Details);
                    return View(Admin);
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
        public ActionResult Owner_Manager_View(Int64 ID)
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                ViewBag.Branch_NAME = Branches;
                var Details = db.Trainers.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    var Trainer = Mapper.Map<TrainerModel>(Details);
                    return View(Trainer);
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
    }
}