using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagmentSystemRetailStore
{
    /// <summary>
    /// Abstract class to use like blueprint to create derived classes - Electronics - Clothing - Toys&Games 
    /// </summary>
    internal abstract class Product
    {
        //Properties for the Abstract class Product 
        //Public properties ProductID, Description, Brand
        //Private properties Cost, MarginOfGain, Price, Inventory
        protected int ProductId;
        protected string Description;
        protected string Brand;
        protected double Cost;
        protected double MarginOfGain;
        protected double Price;
        protected int Inventory;
        protected int ReorderPoint;
        protected string Category;
        protected string User;

        //Parametarized Constructor 
        internal Product(int productId, string description, string brand, double cost, double marginOfGain, double unitPrice, int inventory, int reorderPoint, string category, string user)
        {
            ProductId = productId;
            Description = description;
            Brand = brand;
            Cost = cost;
            MarginOfGain = marginOfGain;
            Price = unitPrice;
            Inventory = inventory;
            ReorderPoint = reorderPoint;
            Category = category;
            User = user;
        }

        public int GetProductId() 
        {
            return this.ProductId;
        }

        public string GetDescription()
        {
            return this.Description;
        }

        public string GetBrand()
        {
            return this.Brand;
        }

        public double GetCost()
        {
            return this.Cost;
        }

        public double GetPrice()
        {
            return this.Price;
        }

        public double GetMarginOfGain()
        {
            return this.MarginOfGain;
        }

        public int GetInventory()
        {
            return this.Inventory;
        }

        public int GetReorderLevel()
        {
            return this.ReorderPoint;
        }

        public string GetCategory()
        {
            return this.Category;
        }

        public string GetUser()
        {
            return this.User;
        }

        //public setter method to set the cost of the product
        public void SetCost(double cost)
        {
            if (cost > 0 )
                this.Cost = cost;
            else
                throw new Exception("Please pass a cost greater than cero");
        }

        //public setter method to set the percentaje of gain
        public void SetMarginOfGain(double margin)
        {
            if (margin >= 0)
                this.MarginOfGain = margin;
            else
                throw new Exception("Please enter a porcentaje like decimal like 0.10 (10%)");
        }

        //public setter method to set the percentaje of gain
        public void SetPrice(double margin)
        {
            if (margin >= 0.00 || margin <= 0.10)
                this.Price = this.Cost * (1 + margin);
            else
                throw new Exception("Please enter a porcentaje like decimal like 0.10 (10%)");
        }

        //public setter method to set new inventory after a sell operation
        public void SetSellInventory(int quantity)
        {
            if (quantity > 0)
                this.Inventory-= quantity;
            else
                throw new Exception("Please enter a inventory quantity greater than cero");
        }

        //public setter method to set new inventory after a order's supplier operation
        public void SetOrderInventory(int quantity)
        {
            if (quantity > 0)
                this.Inventory += quantity;
            else
                throw new Exception("Please enter a inventory quantity greater than cero");
        }

        //public setter method to set the percentaje of gain
        public void SetReorderPoint(int reorderQuantity)
        {
            if (reorderQuantity > 0)
                this.ReorderPoint = reorderQuantity;
            else
                throw new Exception("Please enter a reorder point quantity greater than cero");
        }

        //Abstract method to manage inventory level  
        internal abstract int ManageInventoryLevel();

        //Abstract method to update product's information 
        internal abstract void UpdateProductInformation(string description, string brand, string model);

      
       

    }
}
