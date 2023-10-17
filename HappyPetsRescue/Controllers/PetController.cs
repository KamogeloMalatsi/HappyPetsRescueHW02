using HappyPetsRescue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;

namespace HappyPetsRescue.Controllers
{
    public class PetController : Controller
    {
        private HappyPetsRescueEntities dbContext = new HappyPetsRescueEntities();

        public ActionResult PetPage(int? PetType, int? Breed, int? Location)
        {
            var Pets = dbContext.Pet.Include(p => p.Gender).Include(p => p.Location)
                                    .Include(p => p.PetType).Include(p => p.Breed)
                                    .Include(p => p.AdoptionStatus).Include(p => p.User)
                                    .Where(p => p.PetID != 0);


            if (PetType.HasValue)
            {
                Pets = Pets.Include(p => p.PetType)
                           .Where(p => p.PetType.PetTypeID == PetType.Value);
            }

            if (Breed.HasValue)
            {
                Pets = Pets.Include(p => p.Breed)
                           .Where(p => p.Breed.BreedID == Breed.Value);
            }

            if (Location.HasValue)
            {
                Pets = Pets.Include(p => p.Location)
                           .Where(p => p.Location.LocationID == Location.Value);
            }

            Pets = Pets.Include(p => p.Gender).Include(p => p.AdoptionStatus).Include(p => p.User);

            var JoinedPetData = Pets.ToList();

            return View(JoinedPetData);
        }

        public ActionResult GetBreedByPetType(int petTypeID)
        {
            var Breeds = dbContext.Breed
                .Where(b => b.PetTypeID == petTypeID)
                .Select(b => new { b.BreedID, b.BreedName });

            var breedNames = new List<string>();
            var breedIds = new List<int>();

            foreach (var pt in Breeds)
            {
                breedNames.Add(pt.BreedName);
                breedIds.Add(pt.BreedID);
            }

            return Content(
                string.Format("{0};{1}", string.Join(",", breedNames), string.Join(",", breedIds)),
                "text/plain"
            );
        }

        public ActionResult GetUsers()
        {
            var users = dbContext.User;

            var userNames = new List<string>();
            var UserIds = new List<int>();

            foreach (var user in users)
            {
                userNames.Add(user.UserFullName);
                UserIds.Add(user.UserID);
            }

            return Content(
                string.Format("{0};{1}", string.Join(",", userNames), string.Join(",", UserIds)),
                "text/plain"
            );
        }

        public ActionResult GetUserPhoneNumber(int userID)
        {
            var users = dbContext.User
                .Where(u => u.UserID == userID)
                .Select(u => new { u.UserID, u.UserContactNumber });


            var userPhoneNumbers = new List<string>();
            var UserIds = new List<int>();

            foreach (var user in users)
            {
                userPhoneNumbers.Add(user.UserContactNumber);
                UserIds.Add(user.UserID);
            }

            return Content(
                string.Format("{0};{1}", string.Join(",", userPhoneNumbers), string.Join(",", UserIds)),
                "text/plain"
            );
        }

        [HttpGet]
        public ActionResult DonationsPage() 
        {
            DonationsViewModel donations = new DonationsViewModel();

            donations.UserID = 0;
            donations.donationDate = DateTime.Now;
            donations.totalDonations = (decimal)dbContext.Donation.Sum(d => d.DonationAmount);

            decimal percentage = (donations.totalDonations / donations.goalAmount) * 100;

            donations.progressBarPercentage = Math.Round(percentage).ToString()+"%";
            donations.users = dbContext.User.ToList();

            return View(donations);
        }

        [HttpPost]
        public ActionResult DonationsPage(string userName, decimal Amount)
        {
            int selectedUserID = Convert.ToInt32(userName);

            var userRecord = dbContext.User.FirstOrDefault(u => u.UserID == selectedUserID);

            var newDonation = new Donation
            {
                UserID = userRecord.UserID,
                DonationAmount = Amount,
                DonationDate = DateTime.Now

            };

            dbContext.Donation.Add(newDonation);
            dbContext.SaveChanges();

            DonationsViewModel donations = new DonationsViewModel();

            donations.totalDonations = (decimal)dbContext.Donation.Sum(d => d.DonationAmount);

            decimal percentage = (donations.totalDonations / donations.goalAmount) * 100;

            donations.progressBarPercentage = Math.Round(percentage).ToString() + "%";
            donations.users = dbContext.User.ToList();


            return View(donations);
        }

        public ActionResult AdoptionsPage(int? petID)
        {
            if (petID == null)
            {
                return RedirectToAction("Error");
            }

            var selectedPet = dbContext.Pet.Include(p => p.Gender).Include(p => p.Location)
                                    .Include(p => p.PetType).Include(p => p.Breed)
                                    .Include(p => p.AdoptionStatus).Include(p => p.User)
                                    .Where(p => p.PetID == petID)
                                    .FirstOrDefault();

            var users = dbContext.User.ToList();

            AdoptPetViewModel petDetails = new AdoptPetViewModel();
            petDetails.selectedPet = selectedPet;
            petDetails.Users = users;


            return View(petDetails);
        }

        [HttpPost]
        public ActionResult AdoptionsPage(int petID, string userName)
        {
            //userName carries the user ID
            var pet = dbContext.Pet.FirstOrDefault(p => p.PetID == petID);
            int userID = Convert.ToInt32(userName);
            var user = dbContext.User.FirstOrDefault(u => u.UserID == userID);

            pet.AdoptionStatus = dbContext.AdoptionStatus.FirstOrDefault(s => s.StatusName == "Adopted");

            var adoption = new AdoptedPet
            {
                PetID = pet.PetID,
                UserID = user.UserID,
            };

            dbContext.AdoptedPet.Add(adoption);
            dbContext.SaveChanges();

            //return View("");
            return RedirectToAction("Index","Home");
            
        }

        public ActionResult PostAPet() 
        {
            var petsViewModel = new PostPetViewModel
            {
                PetTypes = dbContext.PetType.ToList(),
                Users = dbContext.User.ToList(),
                Locations = dbContext.Location.ToList(),
                Genders = dbContext.Gender.ToList()
            };


            return View(petsViewModel); 
        }

        [HttpPost]
        public ActionResult PostAPet(string petName, int petAge, decimal petWeight, string petType,
            string petBreed, string location, string gender, string userName, string petStory)
        {
            int petTypeID = Convert.ToInt32(petType);
            int breedID = Convert.ToInt32(petBreed);
            int locationID = Convert.ToInt32(location);
            int genderID = Convert.ToInt32(gender);
            int userID = Convert.ToInt32(userName);

            var newPet = new Pet
            {
                PetName = petName,
                PetAge = petAge,
                PetStory = petStory,
                PetWeight = petWeight,
                //PetImage = image.FileName,
                PetImage = "GermanShepherd.jpg",
                GenderID = genderID,
                LocationID = locationID,
                PetTypeID = petTypeID,
                BreedID = breedID,
                AdoptionStatusID = 1,
                UserID = userID
            };

            dbContext.Pet.Add(newPet);
            dbContext.SaveChanges();

            return RedirectToAction("PetPage");
        }

    }
}