using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }
    }
}
