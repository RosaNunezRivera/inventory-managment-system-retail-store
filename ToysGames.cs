using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    internal class ToysGames : Product
    {
        //Specifics properties for Toys & Games product
      
        public string Type { get; set; }

        //Parametarized Constructor 
        public ToysGames(int productId, string description, string brand, double cost, double marginOfGain, double unitPrice, 
            int inventory, int reorderPoint, string category, string user, string type) :
            base(productId, description, brand, cost, marginOfGain, unitPrice, inventory, reorderPoint, category, user)
        {
            Type = type;
        }

        //Method to update product information 
        internal override void UpdateProductInformation(string newDescription, string newBrand, string newType)
        {
            // Update the description and brand
            this.Description = newDescription;
            this.Brand = newBrand;
            this.Type = newType;
            Console.WriteLine($"Product: Toys & Games information updated: Description {newDescription}, brand {newBrand} and size {newType}");
        }

        internal override int ManageInventoryLevel()
        {
            if (this.Inventory > this.ReorderPoint)
            {
                return 0;
            }
            else
            {
                return this.ReorderPoint - this.Inventory;
            }
        }
    }
}
