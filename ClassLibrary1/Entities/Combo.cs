using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class Combo
    {

        public int Id { get; set; }
        
        public string UrlImg { get; set; }
        public int Discount { get; set; }
        public bool status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public List<ComboTranslation> ComboTranslations { get; set; }

         public List<ComboProduct> ComboProducts { get; set; }
    }
}
