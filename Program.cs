using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    internal class Program
    {
        /// <summary>
        /// Inventory Management System for a Retail Store application in c#
        /// with a focus on the four pillars of Object-Oriented Programming (OOP) 
        /// - Encapsulation, Inheritance, Polymorphism, and Abstraction.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //New object - Creating an instance of the class 
            InventoryManagementSystem ims = new InventoryManagementSystem();

            // Changing the backgroundColor color 
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;

            //Making the User Authentication
            bool authenticationUser = ims.UserAuthentication();
            
            if (authenticationUser) 
            {
                //Show main option menu of the system
                ims.MenuOfOptions();
            }
            else
            {
                Console.WriteLine("\n  Thanks for use Inventory Management System - Retail Store");
            }
              
            Console.ReadKey();
        }
    }
}
