﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.System.Users
{
    public class RegisterRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Guid IdRole { get; set; }
    }
}
