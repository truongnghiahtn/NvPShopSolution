﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class AppConfig
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }
    }
}
