using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    /// <summary>
    /// Child class extender from Product class
    /// </summary>
    internal class Cloth : Product
    {
        //Specifics properties for electronics' product
      
        public string Size { get; set; }

        //Parametarized Constructor 
        public Cloth(int productId, string description, string brand, double cost, double marginOfGain, double unitPrice, int inventory, int reorderPoint, string category, string user, string size) :
            base(productId, description, brand, cost, marginOfGain, unitPrice, inventory,reorderPoint, category, user)
        {
            Size = size;
        }

        //Method to update product information 
        internal override void UpdateProductInformation(string newDescription, string newBrand, string newSize)
        {
            // Update the description and brand
            this.Description = newDescription;
            this.Brand = newBrand;
            this.Size = newSize;
            Console.WriteLine($"Product Cloth information updated: Description {newDescription}, brand {newBrand} and size {newSize}");
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
