using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    /// <summary>
    /// Child class Electronics extended from Product
    /// </summary>
    internal class Electronics : Product
    {
        //Specifics properties for electronics' product
        protected string Model { get; set; }

        //Parametarized Constructor 
        public Electronics(int productId, string description, string brand, double cost, double marginOfGain, double unitPrice, int inventory, int reorderPoint, string category, string user, string model) :
            base(productId, description, brand, cost, marginOfGain, unitPrice, inventory, reorderPoint, category, user)
        {
           
            Model = model;
        }

        internal override void UpdateProductInformation(string newDescription, string newBrand, string newModel)
        {
            // Update the description and brand
            this.Description = newDescription;
            this.Brand = newBrand;
            this.Model = newModel;
            Console.WriteLine($"Product Electronics information updated: Description {newDescription}, brand {newBrand} and model {newModel}");
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
