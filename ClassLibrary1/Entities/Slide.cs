
using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class Slide
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }

        public string Image { get; set; }
        public int SortOrder { get; set; }
        public bool Status { set; get; }

        public DateTime DateCreated { set; get; }
        public DateTime DateUpdate { get; set; }
    }
}
