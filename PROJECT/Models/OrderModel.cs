using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJECT.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string DealerName { get; set; }
        public string GSTNo { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string SoldToParty { get; set; }
        public string ShipToParty { get; set; }
        public string Currency { get; set; }
        public string Incoterms { get; set; }
        public string Material { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public string MinQTY { get; set; }
        public string QTY { get; set; }
        public decimal MRP { get; set; }
        public string Rate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string PaymentMode { get; set; }
        public DateTime CreatedOrderTime { get; set; }
    }
}
