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
    public class PlanController : Controller
    {
        private The_GymEntities db = new The_GymEntities();
        // GET: Booking
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Plane = db.Planes.Where(i => i.GYM_ID == GYM_ID).OrderByDescending(i => i.ID).ToList();
                List<PlaneTotalModel> PlaneTotalModelList = new List<PlaneTotalModel>();
                foreach (var d in Plane)
                {
                    PlaneTotalModel PlaneTotalModel = new PlaneTotalModel();
                    PlaneModel planeModel = new PlaneModel();
                    TotalModel totalModel = new TotalModel();
                    planeModel.Name = d.Name;
                    planeModel.Duration = d.Duration;
                    planeModel.Duration_No = d.Duration_No;
                    planeModel.GST = d.GST;
                    planeModel.GYM_ID = d.GYM_ID;
                    planeModel.ID = d.ID;
                    planeModel.Is_Active = d.Is_Active;
                    planeModel.Worth = d.Worth;
                    totalModel.Branch = db.Branch_Wise_Plane.Where(i => i.Plane_ID == d.ID && i.Status == true).Count();
                    PlaneTotalModel.TotalModel = totalModel;
                    PlaneTotalModel.PlaneModel = planeModel;
                    PlaneTotalModelList.Add(PlaneTotalModel);
                }
                return View(PlaneTotalModelList);
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
                ViewBag.Plane_Duration = new List<string>() { "Year", "Month", "Day" };
                ViewBag.Plane_GST = new List<string>() { "5%", "12%", "18%", "28%" };
                return View();
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlaneModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var Plane = Mapper.Map<Plane>(model);
                    Plane.GYM_ID = GYM_ID;
                    Plane.Is_Active = true;
                    db.Planes.Add(Plane);
                    db.SaveChanges();

                    new Thread(new ThreadStart(() =>
                    {
                        var Branches = db.Branches.Where(i => i.GYM_ID == GYM_ID).ToList();
                        foreach (var Branche in Branches)
                        {
                            Branch_Wise_Plane Branch_Wise_Plane = new Branch_Wise_Plane();
                            Branch_Wise_Plane.Branch_ID = Branche.ID;
                            Branch_Wise_Plane.Status = true;
                            Branch_Wise_Plane.GYM_ID = GYM_ID;
                            Branch_Wise_Plane.Plane_ID = Plane.ID;
                            db.Branch_Wise_Plane.Add(Branch_Wise_Plane);
                            db.SaveChanges();
                        }

                    })).Start();

                    TempData["Success"] = "Plan Has Created Successfully.!";
                }
                else
                {
                    ViewBag.Plane_Duration = new List<string>() { "Year", "Month", "Day" };
                    ViewBag.Plane_GST = new List<string>() { "5%", "12%", "18%", "28%" };
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View();
                }
                return RedirectToAction("Index", "Plan");
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
                var Details = db.Planes.Where(b => b.ID == id).FirstOrDefault();
                if (Details != null)
                {
                    ViewBag.Plane_Duration = new List<string>() { "Year", "Month", "Day" };
                    ViewBag.Plane_GST = new List<string>() { "5%", "12%", "18%", "28%" };
                    var Plane = Mapper.Map<PlaneModel>(Details);
                    return View(Plane);
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
        public ActionResult Edit(PlaneModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dataExists = db.Planes.Where(b => b.ID == model.ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.Duration_No = model.Duration_No;
                        dataExists.Duration = model.Duration;
                        dataExists.Name = model.Name;
                        dataExists.Worth = model.Worth;
                        dataExists.GST = model.GST;
                        db.SaveChanges();
                        TempData["Success"] = "Plan Has Updated Successfully.!";
                    }
                }
                else
                {
                    ViewBag.Plane_Duration = new List<string>() { "Year", "Month", "Day" };
                    ViewBag.Plane_GST = new List<string>() { "5%", "12%", "18%", "28%" };
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View();
                }
                return RedirectToAction("Index", "Plan");
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
                    var Plane = db.Planes.Where(i => i.ID == ID).FirstOrDefault();
                    db.Planes.Remove(Plane);
                    db.SaveChanges();

                    var Planes = db.Branch_Wise_Plane.Where(i => i.Plane_ID == Plane.ID).ToList();
                    foreach (var Plan in Planes)
                    {
                        db.Branch_Wise_Plane.Remove(Plan);
                        db.SaveChanges();
                    }

                })).Start();

                TempData["Success"] = "Plan Has Deleted Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Gym_Wise_Plan(Int64 ID)
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Branch_Selected = db.Branches.Join(db.Branch_Wise_Plane, j => j.ID, u => u.Branch_ID, (j, u) => new { Branch_Plane = u, Branch = j }).Where(i => i.Branch.GYM_ID == GYM_ID && i.Branch_Plane.Plane_ID == ID).ToList();
                List<Branch_Wise_PlaneBranchModel> Branch_Wise_PlaneBranchModelList = new List<Branch_Wise_PlaneBranchModel>();
                foreach (var d in Branch_Selected)
                {
                    Branch_Wise_PlaneBranchModel Branch_Wise_PlaneBranchModel = new Branch_Wise_PlaneBranchModel();
                    BranchModel BranchModel = new BranchModel();
                    Branch_Wise_PlaneModel Branch_Wise_PlaneModel = new Branch_Wise_PlaneModel();
                    BranchModel.Area = d.Branch.Area;
                    BranchModel.Building_Name = d.Branch.Building_Name;
                    BranchModel.Landmark = d.Branch.Landmark;
                    BranchModel.Name = d.Branch.Name;
                    Branch_Wise_PlaneModel.Status = d.Branch_Plane.Status;
                    Branch_Wise_PlaneModel.ID = d.Branch_Plane.ID;
                    Branch_Wise_PlaneBranchModel.Branch = BranchModel;
                    Branch_Wise_PlaneBranchModel.Branch_Wise_PlaneModel = Branch_Wise_PlaneModel;
                    Branch_Wise_PlaneBranchModelList.Add(Branch_Wise_PlaneBranchModel);
                }
                return View(Branch_Wise_PlaneBranchModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Branch_Wise_Plan(Int64 ID)
        {
            try
            {
                var Branch_Selected = db.Planes.Join(db.Branch_Wise_Plane, j => j.ID, u => u.Plane_ID, (j, u) => new { Branch_Plane = u, Plane = j }).Where(i => i.Branch_Plane.Branch_ID == ID).ToList();
                List<Branch_Wise_PlanePlaneModel> Branch_Wise_PlanePlaneModelList = new List<Branch_Wise_PlanePlaneModel>();
                foreach (var d in Branch_Selected)
                {
                    Branch_Wise_PlanePlaneModel Branch_Wise_PlaneBranchModel = new Branch_Wise_PlanePlaneModel();
                    PlaneModel PlaneModel = new PlaneModel();
                    Branch_Wise_PlaneModel Branch_Wise_PlaneModel = new Branch_Wise_PlaneModel();
                    PlaneModel.Name = d.Plane.Name;
                    PlaneModel.Duration = d.Plane.Duration;
                    PlaneModel.Duration_No = d.Plane.Duration_No;
                    PlaneModel.Worth = d.Plane.Worth;
                    Branch_Wise_PlaneModel.Status = d.Branch_Plane.Status;
                    Branch_Wise_PlaneModel.ID = d.Branch_Plane.ID;
                    Branch_Wise_PlaneBranchModel.PlaneModel = PlaneModel;
                    Branch_Wise_PlaneBranchModel.Branch_Wise_PlaneModel = Branch_Wise_PlaneModel;
                    Branch_Wise_PlanePlaneModelList.Add(Branch_Wise_PlaneBranchModel);
                }
                return View(Branch_Wise_PlanePlaneModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Branch_Wise_PlanStatus(Int64 ID)
        {
            try
            {
                new Thread(new ThreadStart(() =>
                {
                    var dataExists = db.Branch_Wise_Plane.Where(b => b.ID == ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.Status = !dataExists.Status;
                        db.SaveChanges();
                    }

                    var Exists = db.Branch_Wise_Plane.Where(i => i.Plane_ID == dataExists.Plane_ID && i.Status == true).FirstOrDefault();
                    if (Exists == null)
                    {
                        var data = db.Planes.Where(b => b.ID == dataExists.Plane_ID).FirstOrDefault();
                        if (data != null)
                        {
                            data.Is_Active = false;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var data = db.Planes.Where(b => b.ID == dataExists.Plane_ID).FirstOrDefault();
                        if (data != null)
                        {
                            data.Is_Active = true;
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
        public ActionResult PlanStatus(Int64 ID)
        {
            try
            {
                new Thread(new ThreadStart(() =>
                {
                    var dataExists = db.Planes.Where(b => b.ID == ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.Is_Active = !dataExists.Is_Active;
                        db.SaveChanges();
                    }

                    var Exists = db.Branch_Wise_Plane.Where(b => b.Plane_ID == ID).ToList();
                    if (Exists != null)
                    {
                        foreach (var data in Exists)
                        {
                            data.Status = dataExists.Is_Active;
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