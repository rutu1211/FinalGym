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
    public class OfferController : Controller
    {
        private The_GymEntities db = new The_GymEntities();

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Offers = db.Offers.Where(i => i.GYM_ID == GYM_ID).OrderByDescending(i => i.ID).ToList();
                List<OfferTotalModel> OfferTotalModelList = new List<OfferTotalModel>();
                foreach (var d in Offers)
                {
                    OfferTotalModel OfferTotalModel = new OfferTotalModel();
                    OfferModel offerModel = new OfferModel();
                    TotalModel totalModel = new TotalModel();
                    offerModel.Discount = d.Discount;
                    offerModel.End_Date = d.End_Date;
                    offerModel.ID = d.ID;
                    offerModel.Name = d.Name;
                    offerModel.Start_Date = d.Start_Date;
                    offerModel.Status = d.Status;
                    totalModel.Branch = db.Branch_Wise_Offer.Where(i => i.Offer_ID == d.ID && i.Status == true).Count();
                    OfferTotalModel.total = totalModel;
                    OfferTotalModel.Offer = offerModel;
                    OfferTotalModelList.Add(OfferTotalModel);
                }
                return View(OfferTotalModelList);
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
                return View();
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        public ActionResult Create(OfferModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var Offer = Mapper.Map<Offer>(model);
                    Offer.GYM_ID = GYM_ID;
                    Offer.Status = true;
                    db.Offers.Add(Offer);
                    db.SaveChanges();

                    new Thread(new ThreadStart(() =>
                    {
                        var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                        foreach (var Branche in Branches)
                        {
                            Branch_Wise_Offer Branch_Wise_Offer = new Branch_Wise_Offer();
                            Branch_Wise_Offer.Branch_ID = Branche.ID;
                            Branch_Wise_Offer.Status = true;
                            Branch_Wise_Offer.GYM_ID = GYM_ID;
                            Branch_Wise_Offer.Offer_ID = Offer.ID;
                            db.Branch_Wise_Offer.Add(Branch_Wise_Offer);
                            db.SaveChanges();
                        }

                    })).Start();
                    TempData["Success"] = "Offer Has Created Successfully.!";
                }
                else
                {
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View();
                }
                return RedirectToAction("Index", "Offer");
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Edit(Int64 ID)
        {
            try
            {
                var Details = db.Offers.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    var Offer = Mapper.Map<OfferModel>(Details);
                    return View(Offer); ;
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
        public ActionResult Edit(OfferModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataExists = db.Offers.Where(b => b.ID == model.ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.Name = model.Name;
                        dataExists.Discount = model.Discount;
                        dataExists.End_Date = model.End_Date;
                        dataExists.Start_Date = model.Start_Date;
                        db.SaveChanges();
                        TempData["Success"] = "Offer Has Updated Successfully.!";
                    }
                }
                else
                {
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View();
                }
                return RedirectToAction("Index", "Offer");
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
                    var Offer = db.Offers.Where(i => i.ID == ID).FirstOrDefault();
                    db.Offers.Remove(Offer);
                    db.SaveChanges();

                    var Offers = db.Branch_Wise_Offer.Where(i => i.Offer_ID == Offer.ID).ToList();
                    foreach (var Offe in Offers)
                    {
                        db.Branch_Wise_Offer.Remove(Offe);
                        db.SaveChanges();
                    }
                })).Start();

                TempData["Success"] = "Offer Has Deleted Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Gym_Wise_Offer(Int64 ID)
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Branch_Selected = db.Branches.Join(db.Branch_Wise_Offer, j => j.ID, u => u.Branch_ID, (j, u) => new { Branch_Wise_Offer = u, Branch = j }).Where(i => i.Branch.GYM_ID == GYM_ID && i.Branch_Wise_Offer.Offer_ID == ID).ToList();
                List<Branch_Wise_OfferBranchModel> Branch_Wise_OfferBranchModelList = new List<Branch_Wise_OfferBranchModel>();
                foreach (var d in Branch_Selected)
                {
                    Branch_Wise_OfferBranchModel Branch_Wise_OfferBranchModel = new Branch_Wise_OfferBranchModel();
                    BranchModel BranchModel = new BranchModel();
                    Branch_Wise_OfferModel Branch_Wise_OfferModel = new Branch_Wise_OfferModel();
                    BranchModel.Area = d.Branch.Area;
                    BranchModel.Building_Name = d.Branch.Building_Name;
                    BranchModel.Landmark = d.Branch.Landmark;
                    BranchModel.Name = d.Branch.Name;
                    Branch_Wise_OfferModel.Status = d.Branch_Wise_Offer.Status;
                    Branch_Wise_OfferModel.ID = d.Branch_Wise_Offer.ID;
                    Branch_Wise_OfferBranchModel.Branch = BranchModel;
                    Branch_Wise_OfferBranchModel.Branch_Wise_OfferModel = Branch_Wise_OfferModel;
                    Branch_Wise_OfferBranchModelList.Add(Branch_Wise_OfferBranchModel);
                }
                return View(Branch_Wise_OfferBranchModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Branch_Wise_Offer(Int64 ID)
        {
            try
            {
                var Branch_Selected = db.Offers.Join(db.Branch_Wise_Offer, j => j.ID, u => u.Offer_ID, (j, u) => new { Branch_Wise_Offer = u, Offer = j }).Where(i => i.Branch_Wise_Offer.Branch_ID == ID).ToList();
                List<Branch_Wise_OfferOfferModel> Branch_Wise_OfferOfferModelList = new List<Branch_Wise_OfferOfferModel>();
                foreach (var d in Branch_Selected)
                {
                    Branch_Wise_OfferOfferModel Branch_Wise_OfferOfferModel = new Branch_Wise_OfferOfferModel();
                    OfferModel OfferModel = new OfferModel();
                    Branch_Wise_OfferModel Branch_Wise_OfferModel = new Branch_Wise_OfferModel();
                    OfferModel.Name = d.Offer.Name;
                    OfferModel.Discount = d.Offer.Discount;
                    OfferModel.Start_Date = d.Offer.Start_Date;
                    OfferModel.End_Date = d.Offer.End_Date;
                    Branch_Wise_OfferModel.ID = d.Branch_Wise_Offer.ID;
                    Branch_Wise_OfferModel.Status = d.Branch_Wise_Offer.Status;
                    Branch_Wise_OfferOfferModel.OfferModel = OfferModel;
                    Branch_Wise_OfferOfferModel.Branch_Wise_OfferModel = Branch_Wise_OfferModel;
                    Branch_Wise_OfferOfferModelList.Add(Branch_Wise_OfferOfferModel);
                }
                return View(Branch_Wise_OfferOfferModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Branch_Wise_OfferStatus(Int64 ID)
        {
            try
            {
                new Thread(new ThreadStart(() =>
                {
                    var dataExists = db.Branch_Wise_Offer.Where(b => b.ID == ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.Status = !dataExists.Status;
                        db.SaveChanges();
                    }

                    var Exists = db.Branch_Wise_Offer.Where(i => i.Offer_ID == dataExists.Offer_ID && i.Status == true).FirstOrDefault();
                    if (Exists == null)
                    {
                        var data = db.Offers.Where(b => b.ID == dataExists.Offer_ID).FirstOrDefault();
                        if (data != null)
                        {
                            data.Status = false;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var data = db.Offers.Where(b => b.ID == dataExists.Offer_ID).FirstOrDefault();
                        if (data != null)
                        {
                            data.Status = true;
                            db.SaveChanges();
                        }
                    }
                })).Start();
                
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult OfferStatus(Int64 ID)
        {
            try
            {
                new Thread(new ThreadStart(() =>
                {
                    var dataExists = db.Offers.Where(b => b.ID == ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.Status = !dataExists.Status;
                        db.SaveChanges();
                    }

                    var Exists = db.Branch_Wise_Offer.Where(b => b.Offer_ID == ID).ToList();
                    if (Exists != null)
                    {
                        foreach (var data in Exists)
                        {
                            data.Status = dataExists.Status;
                            db.SaveChanges();
                        }
                    }
                })).Start();
                
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }
    }
}