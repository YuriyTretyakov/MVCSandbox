using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Whoops.DataLayer.UserInfo;

namespace Whoops.DataLayer
{
    public class User:IdentityUser
    {
        public DateTime RegisteredOn { get; set; }
        public DateTime LastLoggedInOn { get; set; }
        public bool IsNotificationsAllowed { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        ICollection<UserHistory> History { get; set; }
        public Loyalty Loyalty { get; set; }
    }
}