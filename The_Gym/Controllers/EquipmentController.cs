using The_Gym.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace The_Gym.Controllers
{
    [Authorize]
    public class EquipmentController : Controller
    {
        private The_GymEntities db = new The_GymEntities();

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Equipment = db.Equipments.Where(i => i.GYM_ID == GYM_ID).OrderByDescending(i => i.ID).ToList();
                List<EquipmentTotalModel> EquipmentTotalModelList = new List<EquipmentTotalModel>();
                foreach (var d in Equipment)
                {
                    EquipmentTotalModel EquipmentTotalModel = new EquipmentTotalModel();
                    EquipmentModel EquipmentModel = new EquipmentModel();
                    TotalModel TotalModel = new TotalModel();
                    EquipmentModel.Name = d.Name;
                    EquipmentModel.ID = d.ID;
                    var Equipment_Number = db.Branch_Wise_Equipment.Where(i => i.Equipment_ID == d.ID && i.GYM_ID == GYM_ID).ToList();
                    foreach (var Equipments in Equipment_Number)
                    {
                        TotalModel.Equipment = TotalModel.Equipment + Convert.ToInt32(Equipments.Number);
                    }
                    EquipmentTotalModel.TotalModel = TotalModel;
                    EquipmentTotalModel.EquipmentModel = EquipmentModel;
                    EquipmentTotalModelList.Add(EquipmentTotalModel);
                }
                return View(EquipmentTotalModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Add_Equipment(Int64 ID)
        {
            try
            {
                new Thread(new ThreadStart(() =>
                {
                    var dataExists = db.Branch_Wise_Equipment.Where(b => b.ID == ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.Number = dataExists.Number + 1;
                        db.SaveChanges();
                    }
                })).Start();

                TempData["Success"] = "Equipment Has Added Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Remove_Equipment(Int64 ID)
        {
            try
            {
                new Thread(new ThreadStart(() =>
                {
                    var dataExists = db.Branch_Wise_Equipment.Where(b => b.ID == ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.Number = dataExists.Number - 1;
                        db.SaveChanges();
                    }
                })).Start();

                TempData["Success"] = "Equipment Has Removed Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Branches(Int64 ID)
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Branch = db.Branches.Join(db.Branch_Wise_Equipment, j => j.ID, u => u.Branch_ID, (j, u) => new { Branch_Equipment = u, Branch = j }).Where(i => i.Branch_Equipment.GYM_ID == GYM_ID && i.Branch_Equipment.Equipment_ID == ID && i.Branch.IS_Active == true).ToList();
                List<Branch_Wise_EquipmentBranchModel> Branch_EquipmentBranchModelList = new List<Branch_Wise_EquipmentBranchModel>();
                foreach (var d in Branch)
                {
                    Branch_Wise_EquipmentBranchModel Branch_Wise_EquipmentBranchModel = new Branch_Wise_EquipmentBranchModel();
                    BranchModel BranchModel = new BranchModel();
                    Branch_Wise_EquipmentModel Branch_Wise_EquipmentModel = new Branch_Wise_EquipmentModel();
                    BranchModel.Name = d.Branch.Name;
                    Branch_Wise_EquipmentModel.Number = d.Branch_Equipment.Number;
                    Branch_Wise_EquipmentModel.ID = d.Branch_Equipment.ID;
                    Branch_Wise_EquipmentBranchModel.BranchModel = BranchModel;
                    Branch_Wise_EquipmentBranchModel.Branch_Wise_EquipmentModel = Branch_Wise_EquipmentModel;
                    Branch_EquipmentBranchModelList.Add(Branch_Wise_EquipmentBranchModel);
                }
                return View(Branch_EquipmentBranchModelList);
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var Equipments = Mapper.Map<Equipment>(model);
                    Equipments.GYM_ID = GYM_ID;
                    db.Equipments.Add(Equipments);
                    db.SaveChanges();

                    new Thread(new ThreadStart(() =>
                    {
                        var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                        foreach (var Branche in Branches)
                        {
                            Branch_Wise_Equipment Branch_Wise_Equipment = new Branch_Wise_Equipment();
                            Branch_Wise_Equipment.Branch_ID = Branche.ID;
                            Branch_Wise_Equipment.Equipment_ID = Equipments.ID;
                            Branch_Wise_Equipment.GYM_ID = GYM_ID;
                            Branch_Wise_Equipment.Number = 0;
                            db.Branch_Wise_Equipment.Add(Branch_Wise_Equipment);
                            db.SaveChanges();
                        }
                    })).Start();


                    TempData["Success"] = "Equipment Has Created Suscyfully.!";
                }
                else
                {
                    return View();
                }
                return RedirectToAction("Index", "Equipment");
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
                var Details = db.Equipments.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    var Equipment = Mapper.Map<EquipmentModel>(Details);
                    return View(Equipment);
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
        public ActionResult Edit(EquipmentModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataExists = db.Equipments.Where(b => b.ID == model.ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.Name = model.Name;
                        db.SaveChanges();
                        TempData["Error"] = "Equipment Has Updated Suscyfully.!";
                    }
                }
                else
                {
                    return View();
                }
                return RedirectToAction("Index", "Equipment");
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
                    var Equipment = db.Equipments.Where(i => i.ID == ID).FirstOrDefault();
                    db.Equipments.Remove(Equipment);
                    db.SaveChanges();

                    var Branch_Equipments = db.Branch_Wise_Equipment.Where(i => i.Equipment_ID == Equipment.ID).ToList();
                    foreach (var Branch_Equipment in Branch_Equipments)
                    {
                        db.Branch_Wise_Equipment.Remove(Branch_Equipment);
                        db.SaveChanges();
                    }
                })).Start();

                TempData["Success"] = "Equipment Has Deleted Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }
    }
}