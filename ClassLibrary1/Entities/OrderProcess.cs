using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class OrderProcess
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public DateTime TimeComfirm { get; set; }

        public bool Confirmed { get; set; }

        public DateTime TimeShipping { get; set; }

        public bool Shipping { get; set; }

        public DateTime TimeSuccess { get; set; }

        public bool Success { get; set; }

        public DateTime TimeCancel { get; set; }

        public bool Cancel { get; set; }

        public Order Order { get; set; }

    }
}
