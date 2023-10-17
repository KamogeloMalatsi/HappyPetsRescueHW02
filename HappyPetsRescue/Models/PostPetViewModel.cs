using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyPetsRescue.Models
{
    public class PostPetViewModel
    {
        public List<PetType> PetTypes { get; set; }
        public List<User> Users { get; set; }
        public List<Location> Locations { get; set; }
        public List<Gender> Genders { get; set; }
    }
}