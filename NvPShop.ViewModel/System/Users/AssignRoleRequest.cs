using NvPShop.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.System.Users
{
    public  class AssignRoleRequest
    {
        public Guid Id { get; set; }
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
