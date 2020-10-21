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
using Omu.ValueInjecter.Utils;

namespace The_Gym.Controllers
{
    [Authorize]
    public class FeeController : Controller
    {
        private The_GymEntities db = new The_GymEntities();

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                int Branch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                var Fees = db.Fees.Where(i => i.Branch_ID == Branch_ID).OrderByDescending(i => i.ID).ToList();
                List<FeeStudentPlaneOfferModel> FeeStudentPlaneOfferModelList = new List<FeeStudentPlaneOfferModel>();
                foreach (var Fee in Fees)
                {
                    FeeStudentPlaneOfferModel FeeStudentPlaneOfferModel = new FeeStudentPlaneOfferModel();
                    FeeModel FeeModel = new FeeModel();
                    StudentModel StudentModel = new StudentModel();
                    PlaneModel PlaneModel = new PlaneModel();
                    OfferModel OfferModel = new OfferModel();

                    var Plane = db.Planes.Where(i => i.ID == Fee.ID).FirstOrDefault();
                    var Offer = db.Offers.Where(i => i.ID == Fee.ID).FirstOrDefault();
                    var Student = db.Students.Where(i => i.ID == Fee.ID).FirstOrDefault();

                    FeeModel.ID = Fee.ID;
                    FeeModel.Payment_Date = Fee.Payment_Date;
                    FeeModel.Duration = Fee.Start_Date + " TO " + Fee.End_Date;
                    PlaneModel.GST = Fee.GST;
                    FeeModel.Payment_Amount = Fee.Payment_Amount;
                    FeeModel.Discount_On_Bill = Fee.Discount_On_Bill;
                    OfferModel.Name = Offer.Name;
                    PlaneModel.Name = Plane.Name;
                    PlaneModel.Worth = Plane.Worth;
                    StudentModel.Full_Name = Student.First_Name + " " + Student.Last_Name;

                    FeeStudentPlaneOfferModel.FeeModel = FeeModel;
                    FeeStudentPlaneOfferModel.OfferModel = OfferModel;
                    FeeStudentPlaneOfferModel.PlaneModel = PlaneModel;
                    FeeStudentPlaneOfferModel.StudentModel = StudentModel;
                    FeeStudentPlaneOfferModelList.Add(FeeStudentPlaneOfferModel);
                }
                return View(FeeStudentPlaneOfferModelList);
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
                int Branch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                var Student = db.Students.Where(i => i.Branch_ID == Branch_ID).ToList();
                List<StudentModel> StudentModelList = new List<StudentModel>();
                foreach (var Full_Name in Student)
                {
                    StudentModel StudentModel = new StudentModel();
                    StudentModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                    StudentModel.ID = Full_Name.ID;
                    StudentModelList.Add(StudentModel);
                }
                ViewBag.Student_NAME = StudentModelList;

                var Plane = db.Planes.Join(db.Branch_Wise_Plane, j => j.ID, u => u.Plane_ID, (j, u) => new { Branch_Plane = u, Plane = j }).Where(i => i.Branch_Plane.Branch_ID == Branch_ID && i.Branch_Plane.Status == true).ToList();
                List<PlaneModel> PlaneModelList = new List<PlaneModel>();
                foreach (var d in Plane)
                {
                    PlaneModel planeModel = new PlaneModel();
                    planeModel.Name = d.Plane.Name;
                    planeModel.ID = d.Plane.ID;
                    PlaneModelList.Add(planeModel);
                }
                ViewBag.Planes_NAME = PlaneModelList;

                var Offer = db.Offers.Join(db.Branch_Wise_Offer, j => j.ID, u => u.Offer_ID, (j, u) => new { Branch_Wise_Offer = u, Offer = j }).Where(i => i.Branch_Wise_Offer.Branch_ID == Branch_ID && i.Branch_Wise_Offer.Status == true /*&& DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(i.Offer.Start_Date) && DbFunctions.TruncateTime(i.Offer.End_Date) > DbFunctions.TruncateTime(DateTime.Now)*/).ToList();
                List<OfferModel> OfferModelList = new List<OfferModel>();
                foreach (var d in Offer)
                {
                    OfferModel OfferModel = new OfferModel();
                    OfferModel.Name = d.Offer.Name;
                    OfferModel.ID = d.Offer.ID;
                    OfferModelList.Add(OfferModel);
                }
                ViewBag.Offer_NAME = OfferModelList;
                return View();
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Fee = Mapper.Map<Fee>(model);
                    Fee.Branch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                    Fee.GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    Fee.Payment_Date = DateTime.Now;
                    var plane = db.Planes.Where(i => i.ID == model.Plane_ID).FirstOrDefault();
                    Fee.GST = model.GST;
                    Fee.Discount_On_Bill = model.Discount_On_Bill;
                    db.Fees.Add(Fee);
                    db.SaveChanges();

                    new Thread(new ThreadStart(() =>
                    {
                        var delete = db.Fees.Where(i => /*i.Student_ID == model.Student_ID &&*/ i.ID != Fee.ID && i.Payment_Date.Value.Month != DateTime.Now.Month && i.End_Date < DateTime.Now).ToList();
                        if (delete != null)
                        {
                            foreach (var data in delete)
                            {
                                db.Fees.Remove(data);
                                db.SaveChanges();
                            }
                        }

                        var dataExists = db.Students.Where(b => b.ID == model.Student_ID).FirstOrDefault();
                        if (dataExists != null)
                        {
                            dataExists.Status = true;
                            dataExists.Current_Package_ID = Fee.Plane_ID;
                            db.SaveChanges();
                        }
                    })).Start();

                    TempData["Success"] = "Fees submitted sucessfully!!";
                    return RedirectToAction("Bill", "Fee", new { ID = Fee.ID });
                }
                else
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    int Branch_ID = Convert.ToInt32(Session["Branvch_ID"]);
                    var Student = db.Students.Where(i => i.Branch_ID == Branch_ID).ToList();
                    List<StudentModel> StudentModelList = new List<StudentModel>();
                    foreach (var Full_Name in Student)
                    {
                        StudentModel StudentModel = new StudentModel();
                        StudentModel.Full_Name = Full_Name.First_Name + " " + Full_Name.Last_Name;
                        StudentModel.ID = Full_Name.ID;
                        StudentModelList.Add(StudentModel);
                    }
                    ViewBag.Student_NAME = StudentModelList;

                    var Plane = db.Planes.Join(db.Branch_Wise_Plane, j => j.ID, u => u.Plane_ID, (j, u) => new { Branch_Plane = u, Plane = j }).Where(i => i.Branch_Plane.Branch_ID == Branch_ID && i.Branch_Plane.Status == true).ToList();
                    List<PlaneModel> PlaneModelList = new List<PlaneModel>();
                    foreach (var d in Plane)
                    {
                        PlaneModel planeModel = new PlaneModel();
                        planeModel.Name = d.Plane.Name;
                        planeModel.ID = d.Plane.ID;
                        PlaneModelList.Add(planeModel);
                    }
                    ViewBag.Planes_NAME = PlaneModelList;

                    var Offer = db.Offers.Join(db.Branch_Wise_Offer, j => j.ID, u => u.Offer_ID, (j, u) => new { Branch_Wise_Offer = u, Offer = j }).Where(i => i.Branch_Wise_Offer.Branch_ID == Branch_ID && i.Branch_Wise_Offer.Status == true).ToList();
                    List<OfferModel> OfferModelList = new List<OfferModel>();
                    foreach (var d in Offer)
                    {
                        OfferModel OfferModel = new OfferModel();
                        OfferModel.Name = d.Offer.Name;
                        OfferModel.ID = d.Offer.ID;
                        OfferModelList.Add(OfferModel);
                    }
                    ViewBag.Offer_NAME = OfferModelList;

                    var Planes = db.Planes.Where(i => i.GYM_ID == GYM_ID).ToList();
                    ViewBag.Planes_NAME = Planes;
                    TempData["Error"] = "Please Fill All Required Details.!";
                    return View();
                }
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }

        }
        
        [HttpPost]
        public ActionResult GetPlaneName(Int64 ID)
        {
            try
            {
                FeeModel FeeDetail = new FeeModel();
                var plane = db.Planes.Where(i => i.ID == ID).FirstOrDefault();
                return Json(plane, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        public ActionResult GetOfferName(int OFFER_ID, int Plan_ID)
        {
            try
            {
                var Offer = db.Offers.Where(i => i.ID == OFFER_ID).FirstOrDefault();
                var Plan = db.Planes.Where(i => i.ID == Plan_ID).FirstOrDefault();
                return new JsonResult()
                {
                    Data = new
                    {
                        Offer = Offer,
                        Plan = Plan
                    }
                };
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }
        
        [HttpGet]
        public ActionResult Bill(Int64 ID)
        {
            try
            {
                BillModel billModel = new BillModel();
                var Fee = db.Fees.Where(i => i.ID == ID).FirstOrDefault();
                var Gym_Name = db.GYMs.Where(i => i.ID == Fee.GYM_ID).FirstOrDefault();
                var Plane = db.Planes.Where(i => i.ID == Fee.Plane_ID).FirstOrDefault();
                var Branch = db.Branches.Where(i => i.ID == Fee.Branch_ID).FirstOrDefault();
                var Offer = db.Offers.Where(i => i.ID == Fee.Offer_ID).FirstOrDefault();
                var Student = db.Students.Where(i => i.ID == Fee.Student_ID).FirstOrDefault();
                billModel.Student_Name = Student.First_Name + " " + Student.Last_Name;
                billModel.Invoice_Date = Fee.Payment_Date.ToString();
                billModel.Planes_Name = Plane.Name;
                billModel.GYM_Name = Gym_Name.Name;
                billModel.Branch_Name = Branch.Name;
                billModel.Durations = Fee.Start_Date + " TO " + Fee.End_Date;
                billModel.Price = Plane.Worth.ToString();
                billModel.DiscountOnBill = Convert.ToString(Fee.Discount_On_Bill);
                if (Fee.Discount_On_Bill == null)
                {
                    Fee.Discount_On_Bill = 0;
                }

                if (Offer != null)
                {
                    Decimal total = Convert.ToDecimal(((Plane.Worth - ((Plane.Worth * Offer.Discount) / 100)) - Fee.Discount_On_Bill));
                    billModel.Offers = Offer.Discount.ToString();
                    billModel.Total = total.ToString();
                    billModel.GST = Plane.GST.ToString();
                    //if (billModel.GST != null && billModel.GST != "")
                    //{
                    //    total = Convert.ToDecimal(((total - (total * Convert.ToDecimal(billModel.GST)) / 100)));
                    //}
                    billModel.Grand_Total = Fee.Payment_Amount.ToString();
                }
                else
                {
                    Decimal total = Convert.ToDecimal(Plane.Worth - Fee.Discount_On_Bill);
                    billModel.Offers = "0.00";
                    billModel.Total = total.ToString();
                    billModel.GST = Plane.GST.ToString();
                    billModel.Grand_Total = Fee.Payment_Amount.ToString();
                }
                return View(billModel);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Today_Earning(Int64 ID)
        {
            try
            {
                var Plane = db.Students.Join(db.Fees, j => j.ID, u => u.Student_ID, (j, u) => new { Fees = u, Student = j }).Where(i => i.Student.Branch_ID == ID && DbFunctions.TruncateTime(i.Fees.Payment_Date) == DbFunctions.TruncateTime(DateTime.Now)).ToList();
                List<StudentFeePlaneModel> StudentFeePlaneModelList = new List<StudentFeePlaneModel>();
                foreach (var d in Plane)
                {
                    StudentFeePlaneModel StudentFeePlaneModel = new StudentFeePlaneModel();
                    StudentModel StudentModel = new StudentModel();
                    FeeModel feeModel = new FeeModel();
                    PlaneModel planeModel = new PlaneModel();
                    StudentModel.First_Name = d.Student.First_Name;
                    StudentModel.Last_Name = d.Student.Last_Name;
                    StudentModel.Email_ID = d.Student.Email_ID;
                    StudentModel.Mobile = d.Student.Mobile;
                    feeModel.Payment_Amount = d.Fees.Payment_Amount;
                    var Plane_Name = db.Planes.Where(i => i.ID == d.Student.Current_Package_ID).FirstOrDefault();
                    planeModel.Name = Plane_Name.Name;
                    StudentFeePlaneModel.StudentModel = StudentModel;
                    StudentFeePlaneModel.FeeModel = feeModel;
                    StudentFeePlaneModel.PlaneModel = planeModel;
                    StudentFeePlaneModelList.Add(StudentFeePlaneModel);
                }
                return View(StudentFeePlaneModelList);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Month_Earning(Int64 ID)
        {
            try
            {
                var Plane = db.Students.Join(db.Fees, j => j.ID, u => u.Student_ID, (j, u) => new { Fees = u, Student = j }).Where(i => i.Student.Branch_ID == ID && i.Fees.Payment_Date.Value.Month == DateTime.Now.Month).ToList();
                List<StudentFeePlaneModel> StudentFeePlaneModelList = new List<StudentFeePlaneModel>();
                foreach (var d in Plane)
                {
                    StudentFeePlaneModel StudentFeePlaneModel = new StudentFeePlaneModel();
                    StudentModel StudentModel = new StudentModel();
                    FeeModel feeModel = new FeeModel();
                    PlaneModel planeModel = new PlaneModel();
                    StudentModel.First_Name = d.Student.First_Name;
                    StudentModel.Last_Name = d.Student.Last_Name;
                    StudentModel.Email_ID = d.Student.Email_ID;
                    StudentModel.Mobile = d.Student.Mobile;
                    feeModel.Payment_Amount = d.Fees.Payment_Amount;
                    var Plane_Name = db.Planes.Where(i => i.ID == d.Student.Current_Package_ID).FirstOrDefault();
                    planeModel.Name = Plane_Name.Name;
                    StudentFeePlaneModel.StudentModel = StudentModel;
                    StudentFeePlaneModel.FeeModel = feeModel;
                    StudentFeePlaneModel.PlaneModel = planeModel;
                    StudentFeePlaneModelList.Add(StudentFeePlaneModel);
                }
                return View(StudentFeePlaneModelList);
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
                    var Fees = db.Fees.Where(i => i.ID == ID).FirstOrDefault();
                    db.Fees.Remove(Fees);
                    db.SaveChanges();
                })).Start();
                TempData["Success"] = "Bill Has Deleted Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }
    }
}