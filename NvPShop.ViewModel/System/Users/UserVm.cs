﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.System.Users
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
