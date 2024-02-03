using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    /// <summary>
    /// Class to declare global variables & structures
    /// </summary>
    internal class GlobalStructures
    {
        //SD to store products instance
        public static List<Product> productList = new List<Product>();

        //SD to store numbersid for new orders or new sales 
        public static List<Numbers> numbersId = new List<Numbers>();

        //SD to store users 
        public static List<User> usersList = new List<User>();

        //SD to store loggin's users
        public static List<Login> usersLogin = new List<Login>();

        //SD to store sales transactions
        public static List<Sales> salesList = new List<Sales>();

        //SD to store orders transactions
        public static List<Orders> ordersList = new List<Orders>();
    }
}
 