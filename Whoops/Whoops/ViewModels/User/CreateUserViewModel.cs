﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Whoops.ViewModels.User
{
    public class CreateUserViewModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public bool AllowNotifications { get; set; }
    }
}