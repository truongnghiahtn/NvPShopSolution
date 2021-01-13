using System;
using System.Collections.Generic;
using System.Text;

namespace NvPShop.Data.Entities
{
    public class Order
    {
        public int Id { set; get; }
        public Guid IdUser { set; get; }
        public DateTime OrderDate { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }

        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public List<OrderProcess> OrderProcesses { get; set; }
        public AppUser AppUser { get; set; }
    }
}
