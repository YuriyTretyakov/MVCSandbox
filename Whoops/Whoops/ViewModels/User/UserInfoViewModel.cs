using System;

namespace Whoops.ViewModels.User
{
    public class UserInfoViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool AllowNotifications { get; set; }
    }
}
