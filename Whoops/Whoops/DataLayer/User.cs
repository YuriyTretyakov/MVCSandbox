﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Whoops.DataLayer
{
    public class User:IdentityUser
    {
        public DateTime RegisteredOn { get; set; }
        public DateTime LastLoggedInOn { get; set; }
        public int HistoryId { get; set; }
        public bool IsNotificationsAllowed { get; set; }
        public int LoyaltyId { get; set; }
        public DateTime DOB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}