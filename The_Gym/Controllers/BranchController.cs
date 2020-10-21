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
    public class BranchController : Controller
    {
        private The_GymEntities db = new The_GymEntities();

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                var Branche = db.Branches.Where(i => i.GYM_ID == GYM_ID).OrderByDescending(i => i.ID).ToList();
                List<BranchTotalModel> BranchTotalModelList = new List<BranchTotalModel>();
                foreach (var d in Branche)
                {
                    BranchTotalModel BranchTotalModel = new BranchTotalModel();
                    BranchModel BranchModel = new BranchModel();
                    CountryModel CountryModel = new CountryModel();
                    StateModel stateModel = new StateModel();
                    CityModel cityModel = new CityModel();
                    var Country = db.Countries.Where(i => i.ID == d.Country_ID).FirstOrDefault();
                    CountryModel.CountryNames = Country.CountryNames;
                    var State = db.States.Where(i => i.ID == d.State_ID).FirstOrDefault();
                    stateModel.StateName = State.StateName;
                    var City = db.Cities.Where(i => i.ID == d.City_ID).FirstOrDefault();
                    cityModel.City_Name = City.City_Name;
                    BranchModel.Area = d.Area;
                    BranchModel.City_ID = d.City_ID;
                    BranchModel.Building_Name = d.Building_Name;
                    BranchModel.Country_ID = d.Country_ID;
                    BranchModel.ID = d.ID;
                    BranchModel.IS_Active = d.IS_Active;
                    BranchModel.Landmark = d.Landmark;
                    BranchModel.Name = d.Name;
                    BranchModel.Pin_Code = d.Pin_Code;
                    BranchModel.State_ID = d.State_ID;
                    BranchTotalModel.Branch = BranchModel;
                    BranchTotalModel.Country = CountryModel;
                    BranchTotalModel.City = cityModel;
                    BranchTotalModel.State = stateModel;
                    BranchTotalModelList.Add(BranchTotalModel);
                }
                return View(BranchTotalModelList);
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
                CityModel city = new CityModel();
                StateModel state = new StateModel();
                CountryModel country = new CountryModel();

                var Countrie = db.Countries.ToList();
                ViewBag.Countrie_Name = Countrie;
                var State = db.States.Where(i => i.Country_ID == 0).ToList();
                ViewBag.State_Name = State;
                var Citi = db.Cities.Where(i => i.State_ID == 0).ToList();
                ViewBag.City_Name = Citi;
                return View();
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Get_State_Name(Int64 ID)
        {
            try
            {
                List<State> stateDetail = new List<State>();
                stateDetail = db.States.Where(i => i.Country_ID == ID).ToList();
                return Json(stateDetail.Select(x => new
                {
                    StateID = x.ID,
                    StateName = x.StateName
                }).ToList(), JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult Get_City_Name(Int64 ID)
        {
            try
            {
                List<City> CityDetail = new List<City>();
                CityDetail = db.Cities.Where(i => i.State_ID == ID).ToList();
                return Json(CityDetail.Select(x => new
                {
                    StateID = x.ID,
                    StateName = x.City_Name
                }).ToList(), JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BranchModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int GYM_ID = Convert.ToInt32(Session["GYM_ID"]);
                    var Branches = db.Branches.Where(i => i.Name == model.Name && i.GYM_ID == GYM_ID).FirstOrDefault();
                    if (Branches == null)
                    {
                        var Branch = Mapper.Map<Branch>(model);
                        Branch.IS_Active = true;
                        Branch.GYM_ID = GYM_ID;
                        db.Branches.Add(Branch);
                        db.SaveChanges();

                        new Thread(new ThreadStart(() =>
                        {
                            var Equipments = db.Equipments.Where(i => i.GYM_ID == GYM_ID).ToList();
                            foreach (var Equipment in Equipments)
                            {
                                Branch_Wise_Equipment Branch_Wise_Equipment = new Branch_Wise_Equipment();
                                Branch_Wise_Equipment.Branch_ID = Branch.ID;
                                Branch_Wise_Equipment.Equipment_ID = Equipment.ID;
                                Branch_Wise_Equipment.GYM_ID = GYM_ID;
                                Branch_Wise_Equipment.Number = 0;
                                db.Branch_Wise_Equipment.Add(Branch_Wise_Equipment);
                                db.SaveChanges();
                            }

                            var Offers = db.Offers.Where(i => i.GYM_ID == GYM_ID).ToList();
                            foreach (var Offer in Offers)
                            {
                                Branch_Wise_Offer Branch_Wise_Offer = new Branch_Wise_Offer();
                                Branch_Wise_Offer.Branch_ID = Branch.ID;
                                Branch_Wise_Offer.Offer_ID = Offer.ID;
                                Branch_Wise_Offer.GYM_ID = GYM_ID;
                                Branch_Wise_Offer.Status = false;
                                db.Branch_Wise_Offer.Add(Branch_Wise_Offer);
                                db.SaveChanges();
                            }

                            var Planes = db.Planes.Where(i => i.GYM_ID == GYM_ID).ToList();
                            foreach (var Plane in Planes)
                            {
                                Branch_Wise_Plane Branch_Wise_Plane = new Branch_Wise_Plane();
                                Branch_Wise_Plane.Branch_ID = Branch.ID;
                                Branch_Wise_Plane.Plane_ID = Plane.ID;
                                Branch_Wise_Plane.GYM_ID = GYM_ID;
                                Branch_Wise_Plane.Status = false;
                                db.Branch_Wise_Plane.Add(Branch_Wise_Plane);
                                db.SaveChanges();
                            }
                        })).Start();
                        TempData["Success"] = "Branch Has Created Suscyfully.!";
                    }
                    else
                    {
                        CityModel city = new CityModel();
                        StateModel state = new StateModel();
                        CountryModel country = new CountryModel();

                        var Countrie = db.Countries.ToList();
                        ViewBag.Countrie_Name = Countrie;
                        var State = db.States.Where(i => i.Country_ID == model.Country_ID).ToList();
                        ViewBag.State_Name = State;
                        if (state != null)
                        {
                            var Citi = db.Cities.Where(i => i.State_ID == model.State_ID).ToList();
                            ViewBag.City_Name = Citi;
                        }
                        else
                        {
                            var Citi = db.Cities.Where(i => i.State_ID == 0).ToList();
                            ViewBag.City_Name = Citi;
                        }
                        TempData["Error"] = "Branch name is already registered.!";
                        return View();
                    }
                }
                else
                {
                    CityModel city = new CityModel();
                    StateModel state = new StateModel();
                    CountryModel country = new CountryModel();

                    var Countrie = db.Countries.ToList();
                    ViewBag.Countrie_Name = Countrie;
                    var State = db.States.Where(i => i.Country_ID == model.Country_ID).ToList();
                    ViewBag.State_Name = State;
                    var Citi = db.Cities.Where(i => i.State_ID == model.State_ID).ToList();
                    ViewBag.City_Name = Citi;
                    TempData["Error"] = "Please Fill All Details.!";
                    return View();
                }
                return RedirectToAction("Index", "Branch");
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
                var Details = db.Branches.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    CityModel city = new CityModel();
                    StateModel state = new StateModel();
                    CountryModel country = new CountryModel();

                    var Countrie = db.Countries.ToList();
                    ViewBag.Countrie_Name = Countrie;
                    var State = db.States.Where(i => i.Country_ID == Details.Country_ID).ToList();
                    ViewBag.State_Name = State;
                    var Citi = db.Cities.Where(i => i.State_ID == Details.State_ID).ToList();
                    ViewBag.City_Name = Citi;
                    var Branch = Mapper.Map<BranchModel>(Details);
                    return View(Branch);
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
        public ActionResult Edit(BranchModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Branches = db.Branches.Where(i => i.Name == model.Name && i.ID != model.ID).FirstOrDefault();
                    if (Branches == null)
                    {
                        var dataExists = db.Branches.Where(b => b.ID == model.ID).FirstOrDefault();
                        if (dataExists != null)
                        {
                            dataExists.Name = model.Name;
                            dataExists.Area = model.Area;
                            dataExists.City_ID = model.City_ID;
                            dataExists.Building_Name = model.Building_Name;
                            dataExists.Country_ID = model.Country_ID;
                            dataExists.Landmark = model.Landmark;
                            dataExists.Pin_Code = model.Pin_Code;
                            dataExists.State_ID = model.State_ID;
                            db.SaveChanges();
                            TempData["Error"] = "Branch Has Updated Suscyfully.!";
                        }
                    }
                    else
                    {

                        CityModel city = new CityModel();
                        StateModel state = new StateModel();
                        CountryModel country = new CountryModel();

                        var Countrie = db.Countries.ToList();
                        ViewBag.Countrie_Name = Countrie;
                        var State = db.States.Where(i => i.Country_ID == model.Country_ID).ToList();
                        ViewBag.State_Name = State;
                        if (state != null)
                        {
                            var Citi = db.Cities.Where(i => i.State_ID == model.State_ID).ToList();
                            ViewBag.City_Name = Citi;
                        }
                        else
                        {
                            var Citi = db.Cities.Where(i => i.State_ID == 0).ToList();
                            ViewBag.City_Name = Citi;
                        }
                        TempData["Success"] = "Please Fill All Required Details.!";
                        return View();
                    }
                }
                else
                {

                    CityModel city = new CityModel();
                    StateModel state = new StateModel();
                    CountryModel country = new CountryModel();

                    var Countrie = db.Countries.ToList();
                    ViewBag.Countrie_Name = Countrie;
                    var State = db.States.Where(i => i.Country_ID == model.Country_ID).ToList();
                    ViewBag.State_Name = State;
                    var Citi = db.Cities.Where(i => i.State_ID == model.State_ID).ToList();
                    ViewBag.City_Name = Citi;
                    TempData["Success"] = "Please Fill All Required Details.!";
                    return View();
                }
                return RedirectToAction("Index", "Branch");
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
                    var Branch = db.Branches.Where(i => i.ID == ID).FirstOrDefault();
                    db.Branches.Remove(Branch);
                    db.SaveChanges();

                    var Demos = db.Demoes.Where(i => i.Branch_ID == Branch.ID).ToList();
                    foreach (var Demo in Demos)
                    {
                        db.Demoes.Remove(Demo);
                        db.SaveChanges();
                    }

                    var Equipments = db.Branch_Wise_Equipment.Where(i => i.Branch_ID == Branch.ID).ToList();
                    foreach (var Equipment in Equipments)
                    {
                        db.Branch_Wise_Equipment.Remove(Equipment);
                        db.SaveChanges();
                    }

                    var Fees = db.Fees.Where(i => i.Branch_ID == Branch.ID).ToList();
                    foreach (var Fee in Fees)
                    {
                        db.Fees.Remove(Fee);
                        db.SaveChanges();
                    }

                    var Offers = db.Branch_Wise_Offer.Where(i => i.Branch_ID == Branch.ID).ToList();
                    foreach (var Offer in Offers)
                    {
                        db.Branch_Wise_Offer.Remove(Offer);
                        db.SaveChanges();
                    }

                    var Planes = db.Branch_Wise_Plane.Where(i => i.Branch_ID == Branch.ID).ToList();
                    foreach (var Plane in Planes)
                    {
                        db.Branch_Wise_Plane.Remove(Plane);
                        db.SaveChanges();
                    }

                    var Students = db.Students.Where(i => i.Branch_ID == Branch.ID).ToList();
                    foreach (var Student in Students)
                    {
                        db.Students.Remove(Student);
                        db.SaveChanges();
                    }

                    var Trainers = db.Trainers.Where(i => i.Branvch_ID == Branch.ID).ToList();
                    foreach (var Trainer in Trainers)
                    {
                        db.Trainers.Remove(Trainer);
                        db.SaveChanges();
                    }

                })).Start();
                TempData["Success"] = "Branch Has Deleted Successfully.!";
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return RedirectToAction("Contact", "Home");
            }
        }

        [HttpGet]
        public ActionResult View(Int64 ID)
        {
            try
            {
                CityModel city = new CityModel();
                StateModel state = new StateModel();
                CountryModel country = new CountryModel();

                var Countrie = db.Countries.ToList();
                ViewBag.Countrie_Name = Countrie;
                var State = db.States.Where(i => i.Country_ID == 0).ToList();
                ViewBag.State_Name = State;
                var Citi = db.Cities.Where(i => i.State_ID == 0).ToList();
                ViewBag.City_Name = Citi;
                var Details = db.Branches.Where(b => b.ID == ID).FirstOrDefault();
                if (Details != null)
                {
                    var Branch = Mapper.Map<BranchModel>(Details);
                    return View(Branch);
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
        public ActionResult ChangeBranchStatus(Int64 ID)
        {
            try
            {
                new Thread(new ThreadStart(() =>
                {
                    var dataExists = db.Branches.Where(b => b.ID == ID).FirstOrDefault();
                    if (dataExists != null)
                    {
                        dataExists.IS_Active = !dataExists.IS_Active;
                        db.SaveChanges();
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
