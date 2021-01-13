using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.System.Users
{
    public class ResetPasswordRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewpassword { get; set; }
    }
}
