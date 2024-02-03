using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    /// <summary>
    /// Class to store the orders to suppliers 
    /// </summary>
    internal class Orders
    {
        //Proptected properties
        protected DateTime OrderDate;
        protected string SupplierName;
        protected int OrderId;
        protected int ProductId;
        protected int Quantity;
        protected double UnitPrice;
        protected string User;

        internal Orders(DateTime orderDate, string supplierName, int orderId, int productId, int quantity, double unitPrice, string user)
        {
            OrderDate = orderDate;
            SupplierName = supplierName;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            User = user;
        }

        // Getter and Setter methods for properties
        public DateTime GetOrderDate()
        {
            return OrderDate;
        }

        public void SetOrderDate(DateTime orderDate)
        {
            OrderDate = orderDate;
        }

        public string GetSupplierName()
        {
            return SupplierName;
        }

        public void SetSupplierName(string supplierName)
        {
            SupplierName = supplierName;
        }

        public int GetOrderId()
        {
            return OrderId;
        }

        public void SetOrderId(int orderId)
        {
            OrderId = orderId;
        }

        public int GetProductId()
        {
            return ProductId;
        }

        public void SetProductId(int productId)
        {
            ProductId = productId;
        }

        public int GetQuantity()
        {
            return Quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public double GetUnitPrice()
        {
            return UnitPrice;
        }

        public void SetUnitPrice(double unitPrice)
        {
            UnitPrice = unitPrice;
        }

        public string GetUser()
        {
            return User;
        }

        public void SetUser(string user)
        {
            User = user;
        }
    }
}
