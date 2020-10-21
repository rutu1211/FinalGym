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
namespace The_Gym.Controllers
{
    [Authorize]
    public class DashbordController : Controller
    {
        private The_GymEntities db = new The_GymEntities();

        public ActionResult Owner()
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Branche = db.Branches.Where(i => i.GYM_ID == GYM_ID && i.IS_Active == true).ToList();
                List<BranchTotalModel> BranchTotalModelList = new List<BranchTotalModel>();
                ViewBag.Total_Student = db.Students.Where(i => i.GYM_ID == GYM_ID).Count();
                ViewBag.Total_Trainer = db.Trainers.Where(i => i.Role_ID == 3 && i.GYM_ID == GYM_ID).Count();
                ViewBag.Total_Manager = db.Trainers.Where(i => i.Role_ID == 2 && i.GYM_ID == GYM_ID).Count();
                ViewBag.Total_Branch = db.Branches.Where(i => i.GYM_ID == GYM_ID && i.IS_Active == true).Count();
                ViewBag.Total_Offer = db.Offers.Where(i => i.GYM_ID == GYM_ID && i.Status == true).Count();
                ViewBag.Total_Plane = db.Planes.Where(i => i.GYM_ID == GYM_ID && i.Is_Active == true).Count();
                ViewBag.Visit = db.Demoes.Where(i => i.GYM_ID == GYM_ID && DbFunctions.TruncateTime(i.Date) == DbFunctions.TruncateTime(DateTime.Now)).Count();
                ViewBag.Today_Joining = db.Demoes.Where(d => DbFunctions.TruncateTime(d.Date) == DbFunctions.TruncateTime(DateTime.Now) && d.GYM_ID == GYM_ID).Count();
                ViewBag.New_Student = db.Students.Where(d => DbFunctions.TruncateTime(d.Start_Date) == DbFunctions.TruncateTime(DateTime.Now) && d.GYM_ID == GYM_ID).Count();
                var Today_Earning = db.Fees.Where(i => i.GYM_ID == GYM_ID && DbFunctions.TruncateTime(i.Payment_Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
                var Earning = 0;
                foreach (var Earnings in Today_Earning)
                {
                    Earning = Earning + Convert.ToInt32(Earnings.Payment_Amount);
                }
                ViewBag.Today_Earning = Earning;

                var Month_Earnings = db.Fees.Where(i => i.GYM_ID == GYM_ID && i.Payment_Date.Value.Month == DateTime.Now.Month).ToList();
                var Earningss = 0;
                foreach (var Month_Earning in Month_Earnings)
                {
                    Earningss = Earningss + Convert.ToInt32(Month_Earning.Payment_Amount);
                }
                ViewBag.Month_Earning = Earningss;

                foreach (var d in Branche)
                {
                    BranchTotalModel BranchTotalModel = new BranchTotalModel();
                    BranchModel BranchModel = new BranchModel();
                    TotalModel TotalModel = new TotalModel();
                    BranchModel.ID = d.ID;
                    BranchModel.Name = d.Name;
                    TotalModel.Student = db.Students.Where(i => i.Branch_ID == d.ID).Count();
                    TotalModel.Trainer = db.Trainers.Where(i => i.Branvch_ID == d.ID && i.Role_ID == 3).Count();
                    TotalModel.Manager = db.Trainers.Where(i => i.Branvch_ID == d.ID && i.Role_ID == 2).Count();
                    TotalModel.Plane = db.Branch_Wise_Plane.Where(i => i.Branch_ID == d.ID && i.Status == true).Count();
                    TotalModel.Offer = db.Branch_Wise_Offer.Where(i => i.Branch_ID == d.ID && i.Status == true).Count();
                    TotalModel.Demo = db.Demoes.Where(i => i.Branch_ID == d.ID && DbFunctions.TruncateTime(i.Date) == DbFunctions.TruncateTime(DateTime.Now)).Count();
                    ViewBag.Today_Joining = db.Demoes.Where(i => DbFunctions.TruncateTime(i.Start_Date) == DbFunctions.TruncateTime(DateTime.Now) && i.Branch_ID == d.ID).Count();
                    ViewBag.New_Student = db.Students.Where(i => DbFunctions.TruncateTime(i.Start_Date) == DbFunctions.TruncateTime(DateTime.Now) && i.Branch_ID == d.ID).Count();
                    TotalModel.Today_Earning = Earning;
                    TotalModel.Month_Earning = Earningss;
                    BranchTotalModel.Total = TotalModel;
                    BranchTotalModel.Branch = BranchModel;
                    BranchTotalModelList.Add(BranchTotalModel);
                }
                return View(BranchTotalModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        public ActionResult Manager()
        {
            try
            {
                int Branvch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                var Trainer = db.Trainers.Join(db.Branches, u => u.Branvch_ID, j => j.ID, (u, j) => new { Trainer = u, Branch = j }).Where(i => i.Branch.ID == i.Trainer.Branvch_ID && i.Trainer.Role_ID == 3 && i.Branch.ID == Branvch_ID).ToList();
                List<TrainerTotalModel> TrainerTotalModelList = new List<TrainerTotalModel>();
                ViewBag.Total_Student = db.Students.Where(i => i.Branch_ID == Branvch_ID).Count();
                ViewBag.Total_Trainer = db.Trainers.Where(i => i.Role_ID == 3 && i.Branvch_ID == Branvch_ID).Count();
                ViewBag.Today_Joining = db.Demoes.Where(d => DbFunctions.TruncateTime(d.Start_Date) == DbFunctions.TruncateTime(DateTime.Now) && d.Branch_ID == Branvch_ID).Count();
                ViewBag.Student_Fee = db.Students.Where(i => i.Branch_ID == Branvch_ID && i.Status != true).Count();
                ViewBag.Total_Offer = db.Branch_Wise_Offer.Where(i => i.Branch_ID == Branvch_ID && i.Status == true).Count();
                ViewBag.Total_Plane = db.Branch_Wise_Plane.Where(i => i.Branch_ID == Branvch_ID && i.Status == true).Count();
                foreach (var d in Trainer)
                {
                    TrainerTotalModel TrainerTotalModel = new TrainerTotalModel();
                    TrainerModel trainerModel = new TrainerModel();
                    TotalModel TotalModel = new TotalModel();
                    trainerModel.Branvch_ID = d.Trainer.Branvch_ID;
                    trainerModel.Email_ID = d.Trainer.Email_ID;
                    trainerModel.First_Name = d.Trainer.First_Name;
                    trainerModel.Last_Name = d.Trainer.Last_Name;
                    trainerModel.ID = d.Trainer.ID;
                    trainerModel.Role_ID = d.Trainer.Role_ID;
                    TotalModel.Student = db.Students.Where(i => i.Trainer_ID == d.Trainer.ID).Count();
                    TrainerTotalModel.Trainer = trainerModel;
                    TrainerTotalModel.Total = TotalModel;
                    TrainerTotalModelList.Add(TrainerTotalModel);
                }
                return View(TrainerTotalModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }
    }
}