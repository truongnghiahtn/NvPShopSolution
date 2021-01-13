using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public  class ComboTranslation
    {
        public int Id { get; set; }
        public int IdCombo { get; set; }
        public string IdLanguage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public Language Language { get; set; }
        public Combo Combo { get; set; }
    }
}
