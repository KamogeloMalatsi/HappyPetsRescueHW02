using HappyPetsRescue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace HappyPetsRescue.Controllers
{
    public class HomeController : Controller
    {
        private HappyPetsRescueEntities dbContext = new HappyPetsRescueEntities();

        public ActionResult Index()
        {
            List<AdoptedPet> adoptedPets = dbContext.AdoptedPet.ToList();
            List<Pet> pets = dbContext.Pet.ToList();
            List<User> users = dbContext.User.ToList();

            var AdoptedPets = dbContext.AdoptedPet.Include(ap => ap.Pet).Include(ap => ap.User).ToList();

            return View(AdoptedPets);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}