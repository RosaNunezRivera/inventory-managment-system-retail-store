using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    internal class Numbers
    {
        public string ID { get; set; }
        public int SalesId { get; set; }
        public int OrdersId { get; set; }

        public Numbers(string numberId, int salesId, int ordersId) 
        {
            ID = numberId;
            SalesId = salesId;
            OrdersId = ordersId;
        }

        public void SetSales()
        {
            this.SalesId++;
        }

        public void SetOrders()
        {
            this.OrdersId++;
        }
    }
}
