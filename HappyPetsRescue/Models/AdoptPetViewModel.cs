using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyPetsRescue.Models
{
    public class AdoptPetViewModel
    {
        public Pet selectedPet { get; set; }
        public List<User> Users { get; set; } 
        public string SelectedUserName { get; set; } 
        public string SelectedUserPhoneNumber { get; set; }
    }
}