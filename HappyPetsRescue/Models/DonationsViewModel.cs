using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyPetsRescue.Models
{
    public class DonationsViewModel
    {
        public int UserID { get; set; }

        public List<User> users { get; set; }

        public double donationAmount { get; set; }

        public DateTime donationDate { get; set; }

        public decimal goalAmount { get; set; } = 75000; //Current goal

        public decimal totalDonations { get; set; }

        public bool IsGoalReached => totalDonations >= goalAmount;

        public string progressBarPercentage { get; set; }

    }
}