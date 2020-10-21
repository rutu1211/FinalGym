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
    public class NotificationController : Controller
    {

        [HttpGet]
        public ActionResult Owner()
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
        public ActionResult Owner(NotificationModel model)
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

        [HttpGet]
        public ActionResult Manager()
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
        public ActionResult Manager(NotificationModel model)
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
    }
}