using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    internal class Sales
    {
        protected DateTime SaleDate;
        protected string Customer;
        protected int SaleId;
        protected int ProductId;
        protected int Quantity;
        protected double UnitPrice;
        protected string User;

        internal Sales(DateTime saleDate, string customer, int saleId, int productId, int quantity, double unitPrice, string user) 
        {
            SaleDate = saleDate;
            Customer = customer;
            SaleId = saleId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            User = user;
        }

        // Getter and Setter methods for properties
        public DateTime GetSaleDate()
        {
            return SaleDate;
        }

        public void SetSaleDate(DateTime saleDate)
        {
            SaleDate = saleDate;
        }

        public string GetCustomer()
        {
            return Customer;
        }

        public void SetCustomer(string customer)
        {
            Customer = customer;
        }

        public int GetSaleId()
        {
            return SaleId;
        }

        public void SetSaleId(int saleId)
        {
            SaleId = saleId;
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
