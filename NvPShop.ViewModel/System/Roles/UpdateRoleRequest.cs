﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.ViewModel.System.Roles
{
    public class UpdateRoleRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
