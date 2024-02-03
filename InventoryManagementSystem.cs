using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    internal class InventoryManagementSystem
    {
        private IOrderedEnumerable<Product> orderedProductsList;


        /// <summary>
        /// Method to show the main menu of the systems 
        /// </summary>
        public void MenuOfOptions()
        {
            try
            {
                //Declare variables
                int option = 0;
                string optionString = "";
                bool isValidInt = false;

                //Do-while to be in a loop while option is not exit
                do
                {
                    do
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\n  Inventory Management System - Retail Store");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("\n  Option menu:");
                        Console.WriteLine("  -------------");
                        Console.WriteLine("  (1) Add Product");
                        Console.WriteLine("      Add new product into the system");

                        Console.WriteLine("  (2) Discounts and Promotions");
                        Console.WriteLine("      Apply special prices");

                        Console.WriteLine("  (3) Sales");
                        Console.WriteLine("      Register a product sale");

                        Console.WriteLine("  (4) Stock Assessment");
                        Console.WriteLine("      See the entire inventory's product");

                        Console.WriteLine("  (5) Replenish the stock ");
                        Console.WriteLine("      Restoring the inventory levels of products");

                        Console.WriteLine("  (6) Exit");

                        //Gettint the string enter by the user and parse if is a integer 
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("\n  Enter an option (1-6): ");
                        Console.ForegroundColor = ConsoleColor.Black;
                        optionString = Console.ReadLine();

                        isValidInt = int.TryParse(optionString, out option);

                        if (!isValidInt || string.IsNullOrEmpty(optionString) || option <= 0 || option > 6)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("  Only numbers are valid, please enter numbers 1-6");
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("\n  Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    } while (!isValidInt || option <= 0 || option > 6 || string.IsNullOrEmpty(optionString));

                    //Evaluating each value of the option enter by the user
                    switch (option)
                    {
                        case 1:
                            AddProducts();
                            break;
                        case 2:
                            DiscountsPromotions();
                            break;
                        case 3:
                            Sales();
                            break;
                        case 4:
                            StockAssessment();
                            break;
                        case 5:
                            Orders();
                            break;
                        case 6:
                            Console.WriteLine("\n  Thanks for use Inventory Management System - Retail Store");
                            break;
                    }
                } while (option != 6);
            }
            catch (Exception ex)
            {
                Console.WriteLine("  Error ocurred in option menu! " + ex.Message);
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Method to ask the user and password to performance a User Authentication
        /// </summary>
        /// <returns></returns>
        public bool UserAuthentication()
        {
            //Set initial values
            InitialProducts();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n  Inventory Management System - Retail Store");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n  User Authentication:");
            Console.WriteLine("  ----------------------");


            //Declaring variables
            string userInput;
            string passwordInput;
            int counter = 1;
            int maxAttempts = 3;
            bool isAuthenticated = false;

            //Asking user and password to validate 
            do
            {
                Console.Write("\n  Enter your user:");
                userInput = Console.ReadLine();

                Console.Write("  Enter your password:");
                passwordInput = GetPassword();

                Console.ForegroundColor = ConsoleColor.Black;

                //Validating if the user and password was founded to perform an user authentication
                if (GlobalStructures.usersList.Any(p => p.UserName == userInput && p.Password == passwordInput))
                {
                    Console.WriteLine($"  User and password has been authenticated");
                    Login userLogin = new Login(userInput);
                    GlobalStructures.usersLogin.Add(userLogin);
                    isAuthenticated = true;
                    break;
                }
                else
                {
                    Console.WriteLine($"  User and password could not be authenticated");
                    counter++;
                }
            } while (counter <= maxAttempts);
            return isAuthenticated;
        }

        /// <summary>
        /// Method to ask a password with a mask using '*' character
        /// </summary>
        /// <returns></returns>
        public static string GetPassword()
        {
            string password="";
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                // Ignore any key that isn't a printable character or Enter
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    // Printing the input with an asterisk
                    Console.Write("*"); 
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // Removing the last character from the password if backspace is pressed
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b"); // Erase the last character on the console
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after the user presses Enter
            return password;
        }



        /// <summary>
        /// AddProducts method. Functioality add products to the system
        /// </summary>
        public void AddProducts()
        {
            //Declaring variables
            int productId;
            string description = "";
            string brand = "";
            double cost = 0;
            string marginOfGainString = "";
            double marginOfGain = 0;
            string productIdString = "";
            string costString = "";
            string inventoryString = "";
            int inventory = 0;
            string reorderLevelString = "";
            int reorderLevel = 0;
            string categoryOptionString = "";
            int categoryOption = 0;
            bool isValidInt = false;
            string lastProperty = "";  //size, model or type

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n  Inventory Management System - Retail Store\n");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("  Add Products:");
            Console.WriteLine("  -------------");

            do
            {
                Console.WriteLine("  (1). Cloth  (2) Electronics (3) Toys&games");
                Console.Write("  Choose an option: ");
                categoryOptionString = Console.ReadLine().Trim();

                // Get a string and using a Char.TryParse() method to get a boolean value to validate a valid char input
                isValidInt = int.TryParse(categoryOptionString, out categoryOption);

                if (!isValidInt || categoryOption <= 0 || categoryOption > 3)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"  Please, enter numbers 1 to 3\n");
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    isValidInt = true;
                }
            } while (!isValidInt || categoryOption <= 0 || categoryOption > 3);

            do
            {
                Console.Write("  Producto Id:");
                productIdString = Console.ReadLine();

                if (!int.TryParse(productIdString, out productId))
                {
                    Console.Write("  Please, enter a valid number\n");
                }
                else
                {
                    if (GlobalStructures.productList.Any(p => p.GetProductId() == productId))
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"\n  The id {productId} is alredy registered, please try another id\n");
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                }

            } while (!int.TryParse(productIdString, out productId) || GlobalStructures.productList.Any(p => p.GetProductId() == productId));

            do
            {
                Console.Write("  Description:");
                description = Console.ReadLine();

                if (string.IsNullOrEmpty(description))
                {
                    Console.Write("  Please, enter a product's name or description\n");
                }

            } while (string.IsNullOrEmpty(description));

            do
            {
                Console.Write("  Brand:");
                brand = Console.ReadLine();

                if (string.IsNullOrEmpty(brand))
                {
                    Console.WriteLine("  Please, enter a product's brand\n");
                }

            } while (string.IsNullOrEmpty(brand));


            do
            {
                Console.Write("  Cost:");
                costString = Console.ReadLine();

                if (!double.TryParse(costString, out cost))
                {
                    Console.WriteLine("  Please, enter a cost of the producto\n");
                }

            } while (!double.TryParse(costString, out cost));

            //Pattern to evalute a valud 
            string patternMarginOfGain = @"^\d+(\.\d{1,2})?$";
            do
            {
                Console.Write("  % Margin of Gain (decimal value):");
                marginOfGainString = Console.ReadLine();

                if (!double.TryParse(marginOfGainString, out marginOfGain) && Regex.IsMatch(marginOfGainString, patternMarginOfGain))
                {
                    Console.WriteLine("  Please, enter a margin of gain for the product\n");
                }
            } while (!double.TryParse(marginOfGainString, out marginOfGain) && Regex.IsMatch(marginOfGainString, patternMarginOfGain));

            do
            {
                Console.Write("  Initial Inventory:");
                inventoryString = Console.ReadLine();

                if (!int.TryParse(inventoryString, out inventory))
                {
                    Console.WriteLine("  Please, enter the value of inventory\n");
                }
            } while (!int.TryParse(inventoryString, out inventory));

            do
            {
                Console.Write("  Reorder level (minimum existence to start a order)");
                reorderLevelString = Console.ReadLine();

                if (!int.TryParse(reorderLevelString, out reorderLevel))
                {
                    Console.WriteLine("  Please, enter the value of inventory\n");
                }
            } while (!int.TryParse(reorderLevelString, out reorderLevel));


            //Get the user loggin
            Login firstLogin = GlobalStructures.usersLogin[0];
            string userNameLoggin = firstLogin.UserName;
            
            //Evaluating option choosen by the user 
            switch (categoryOption)
            {
                //In case that product is a Cloth
                case 1:
                    //Asking for the last property Size
                    do
                    {
                        lastProperty = GetLastProperty("size");

                    } while (string.IsNullOrEmpty(lastProperty));
                    Cloth newProductCloth = new Cloth(productId, description, brand, cost, marginOfGain, cost*(1+marginOfGain), inventory, reorderLevel, "Cloth", userNameLoggin, lastProperty);

                    //Adding the new instance product Cloth
                    GlobalStructures.productList.Add(newProductCloth);
                    AddSuccessfully(description);

                    break;
                case 2:
                    //Asking for the last property model
                    do
                    {
                        lastProperty = GetLastProperty("model");

                    } while (string.IsNullOrEmpty(lastProperty));

                    Electronics newProductElectronic = new Electronics(productId, description, brand, cost, marginOfGain, cost * (1 + marginOfGain), inventory, reorderLevel, "Electronics", userNameLoggin, lastProperty);
                    GlobalStructures.productList.Add(newProductElectronic);
                    AddSuccessfully(description);
                    Console.ReadKey();
                    break;
                case 3:
                    //Asking for the last property type
                    do
                    {
                        lastProperty = GetLastProperty("type");

                    } while (string.IsNullOrEmpty(lastProperty));

                    ToysGames newProductToysGames = new ToysGames(productId, description, brand, cost, marginOfGain, cost * (1 + marginOfGain),inventory, reorderLevel, "Toys&Games", userNameLoggin, lastProperty);
                    GlobalStructures.productList.Add(newProductToysGames);
                    AddSuccessfully(description);
                    break;
            }

        }//End of method AddProduct

        /// <summary>
        /// Method to get the property for each category of products
        /// </summary>
        /// <param name="dataToGet"></param>
        /// <returns></returns>
        public string GetLastProperty(string dataToGet)
        {
            Console.Write($"  {dataToGet}:");
            string lastProperty = Console.ReadLine();

            if (string.IsNullOrEmpty(lastProperty))
            {
                Console.WriteLine($"  Please, enter a product's {dataToGet}");
            }
            return lastProperty;
        }

        public void AddSuccessfully(string des)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  The new product {des} had been created\n");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadKey();
        }

        /// <summary>
        /// Adding products in each category
        /// Set the numbers for sales and orders to use like id
        /// Set the user for use in option of User authentication
        /// </summary>
        public void InitialProducts()
        {
            try
            {
                //Adding products type Cloth
                GlobalStructures.productList.Add(new Cloth(1, "Cotton Shirt", "ClothBrand", 25.0,  0.2, 30.0, 15, 3, "Cloth", "admin", "M"));
                GlobalStructures.productList.Add(new Cloth(2, "Denim Pants", "ClothBrand", 30.0, 0.2, 36.0, 20, 3, "Cloth", "rnunez", "L"));
                GlobalStructures.productList.Add(new Cloth(3, "Hoodie", "ClothBrand", 35.0, 0.2, 42.0, 12, 3, "Cloth", "admin", "XL"));

                //Adding products type Electronics
                GlobalStructures.productList.Add(new Electronics(4, "Smartphone", "ElectronicsBrand", 300.0, 0.3, 390.00, 8, 5, "Electronics", "rnunez", "ModelXYZ"));
                GlobalStructures.productList.Add(new Electronics(5, "Tablet", "ElectronicsBrand", 200.0, 0.3, 260.0, 10, 5, "Electronics", "rnunez", "ModelABC"));
                GlobalStructures.productList.Add(new Electronics(6, "Wireless Headphones", "ElectronicsBrand", 100.0, 0.3, 130.0, 15, 5, "Electronics", "admin", "Model123"));

                //Adding products type Toy&Games
                GlobalStructures.productList.Add(new ToysGames(7, "Puzzle", "ToysBrand", 15.0, 0.1, 16.5, 5, 10, "Toys&Games", "admin", "1000 Piece Puzzle"));
                GlobalStructures.productList.Add(new ToysGames(8, "Plush Doll", "ToysBrand", 10.0, 0.1,11.0, 3, 10, "Toys&Games", "admin", "Bear Doll"));
                GlobalStructures.productList.Add(new ToysGames(9, "Board Game", "ToysBrand", 20.0, 0.1, 22.0, 20, 10, "Toys&Games", "rnunez", "Monopoly"));

                GlobalStructures.numbersId.Add(new Numbers("NumbersId", 1, 1));

                User adminUser = new User("admin", "admin555");
                GlobalStructures.usersList.Add(adminUser);

                User rosaUser = new User("rnunez", "rosa123");
                GlobalStructures.usersList.Add(rosaUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine("  Error ocurred! " + ex.Message);
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Method to print the Sotck Assesment 
        /// Printing all the product sorter by category and including general information (description, brand)
        /// Printing cost, inventory, reorder level , and amount reorder 
        /// Bring in this option the total amount invest to make a order 
        /// </summary>
        public void StockAssessment()
        {
            try
            {
                //Create a sorted the SD to be show order by Category
                var orderedProductsList = GlobalStructures.productList.OrderBy(p => p.GetCategory()).ToList();
               

                Print("  Stock Assessment", orderedProductsList);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("  Error ocurred! " + ex.Message);
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Method to print the product's inventory acoording a the filtered SD passed as argument
        /// </summary>
        /// <param name="title"></param>
        /// <param name="orderedProductsList"></param>
        public void Print(string title, List<Product> orderedProductsList) 
        {
            try
            {
                var reorderamount = GlobalStructures.productList.Sum(p => p.ManageInventoryLevel() * p.GetCost());
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n  Inventory Management System - Retail Store");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(title);
                Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("     Category   Id            Decription             Brand       Cost     Inventory Reor.Level  Reorder Amount  User");
                Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");

                if (orderedProductsList.Count > 0)
                {
                    foreach (var product in orderedProductsList)
                    {
                        Console.WriteLine("   " + product.GetCategory().PadRight(10).Substring(0, 10) + " | " + product.GetProductId().ToString().PadRight(4) + " | " + product.GetDescription().PadRight(25).Substring(0, 25) + " | " + product.GetBrand().PadRight(10).Substring(0, 10) + " | " + product.GetCost().ToString("C").PadRight(7) + " | " + product.GetInventory().ToString().PadRight(8) + " | " + product.GetReorderLevel().ToString().PadRight(8) + " | " + (product.ManageInventoryLevel() * product.GetCost()).ToString("C").PadRight(12) + " | " + product.GetUser().PadRight(8).Substring(0, 8));
                    }
                }
                else
                {
                    Console.WriteLine("  There is not any product in any category to show");
                }
                Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"  Total reorder amount --> {reorderamount.ToString("C")}");
                Console.ForegroundColor = ConsoleColor.Black;
            }
            catch (Exception ex)
            {
                Console.WriteLine("  Error ocurred! " + ex.Message);
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
            Console.WriteLine("\n  Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        /// <summary>
        /// Sales method, to register product's sales and store in a SD
        /// </summary>
        public void Sales()
        {
            try
            {
                //Declaring variables
                string customer;
                string productIdString = "";
                int productId = 0;
                bool isValidInt = false;
                int quantitySold = 0;
                string quantitySoldString;
                double price;
                DateTime now = DateTime.Now;
                bool addMoreProduct = true;
                string sellMoreProducts="y";
                double total=0;
                bool successfulQuantitySold = false;
                int actualInventory=0;

                //Get the user loggin
                Login firstLogin = GlobalStructures.usersLogin[0];
                string userNameLoggin = firstLogin.UserName;

                //Get the actual number of sale 
                Numbers firstNumber = GlobalStructures.numbersId.First();
                int actualSaleId = firstNumber.SalesId;

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n  Inventory Management System - Retail Store\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("  Sale's Products:");
                Console.WriteLine("  ----------------");

                Console.WriteLine($"  Sale Id: {actualSaleId}");
                Console.WriteLine($"  Date   : {now}");
                Console.WriteLine($"  User   : {userNameLoggin}");

                //Asking the customer name
                do
                {
                    Console.Write("  Customer name:");
                    customer = Console.ReadLine();

                    if (string.IsNullOrEmpty(customer))
                    {
                        Console.WriteLine("  Please, enter a customer name like Jonh Smith\n");
                    }

                } while (string.IsNullOrEmpty(customer));

                Console.WriteLine("  Products ");
                Console.WriteLine("  ----------------------------------------------------------------------------------------");
                Console.WriteLine("      Id           Decription              Category     Brand       Inventory    Price    ");
                Console.WriteLine("  ----------------------------------------------------------------------------------------");

                do
                {
                    Console.Write("  Id:");
                    productIdString = Console.ReadLine();

                    if (!int.TryParse(productIdString, out productId))
                    {
                        Console.Write("  Please, enter a valid product id\n");
                    }
                    else 
                    {
                        var filteredProduct = GlobalStructures.productList.Where(p => p.GetProductId() == productId).ToList();
                        if (filteredProduct.Count>0)
                        {
                            foreach (var product in filteredProduct)
                            {
                                Console.Write("               "+product.GetDescription().PadRight(25).Substring(0, 25) + " | " + product.GetCategory().PadRight(10).Substring(0, 10) + " | " + product.GetBrand().PadRight(10).Substring(0, 10) + " | " + product.GetInventory().ToString().PadRight(8) + " | " + product.GetPrice().ToString("C").PadRight(8));
                               
                            }

                            Product firstProduct = filteredProduct.First();
                            price = firstProduct.GetPrice();
                            actualInventory = firstProduct.GetInventory();
                            do
                            {
                                successfulQuantitySold = false;
                                Console.Write("\n  Quantity:");
                                quantitySoldString = Console.ReadLine();

                                if (!int.TryParse(quantitySoldString, out quantitySold))
                                {
                                    Console.Write("  Please, enter a quantity of product to sell\n");
                                }

                                if (quantitySold <= actualInventory) 
                                {
                                    successfulQuantitySold = true;
                                }
                                else
                                {
                                    Console.Write("  The quantity to be sold in excess of the current inventory\n");
                                }
                            } while(!int.TryParse(quantitySoldString, out quantitySold) || !successfulQuantitySold);

                            
                          
                            Console.WriteLine($"  Sub-Total--> {price*quantitySold}");
                            total += (price * quantitySold);
                          
                            //Adding the products sale
                            Sales sale = new Sales(now, customer, actualSaleId, productId, quantitySold, price, userNameLoggin);
                            GlobalStructures.salesList.Add(sale);

                            //Updating the inventory resting the quantity sold
                            firstProduct.SetSellInventory(quantitySold);

                            //Updating the number of saleId using SetSales()
                            firstNumber.SetSales();
                            do
                            {
                                Console.Write($"  Would you like to sell more products (Y/N):");
                                sellMoreProducts = Console.ReadLine();
                                if (string.IsNullOrEmpty(sellMoreProducts))
                                {
                                    Console.WriteLine("  Please, enter Y or N \n");
                                }

                                if (sellMoreProducts.ToLower() == "n") 
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    addMoreProduct = false;
                                    Console.WriteLine($"  The total sell is --->${total}");
                                    Console.WriteLine($"  Press any key to continue...");
                                    Console.ReadKey();
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                              
                            } while (sellMoreProducts.ToLower() != "y" && sellMoreProducts.ToLower() != "n" || string.IsNullOrEmpty(sellMoreProducts));
                            
                        }
                        else
                        {
                            Console.WriteLine("  The product's id was not founded");
                            addMoreProduct = true;
                            Console.ReadKey();
                        }
                        if (!addMoreProduct)
                        {
                            break;
                        }
                    }
                   
                } while (!int.TryParse(productIdString, out productId) || addMoreProduct);
            }
            catch (Exception ex)
            {
                Console.WriteLine("  Error ocurred during the view! " + ex.Message);
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Orders method, to register orders to the suppliers to incrense inventory in the system
        /// </summary>
        public void Orders()
        {
            try
            {
                //Declaring variables
                string supplierName="";
                int productId = 0;
                int quantityOrder = 0;
                double price;
                DateTime now = DateTime.Now;

                //Get the user loggin
                Login firstLogin = GlobalStructures.usersLogin[0];
                string userNameLoggin = firstLogin.UserName;

                //Get the actual number of orders supplier  
                Numbers firstNumber = GlobalStructures.numbersId.First();
                int actualOrderId = firstNumber.OrdersId;

                //Applying a filter to show only the products which have inventory under the reorder level    
                var filteredProductToReorderLevel = GlobalStructures.productList.Where(p => p.ManageInventoryLevel() > 0).ToList();
                var reorderamount = GlobalStructures.productList.Sum(p => p.ManageInventoryLevel() * p.GetCost());

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n  Inventory Management System - Retail Store\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("  Order's Products to suppliers:");
                Console.WriteLine("  -----------------------------");

                Console.WriteLine($"  Sale Id: {actualOrderId}");
                Console.WriteLine($"  Date   : {now}");
                Console.WriteLine($"  User   : {userNameLoggin}");

                if (filteredProductToReorderLevel.Count > 0) 
                {
                    //Asking the customer name
                    do
                    {
                        Console.Write("  Supplier name:");
                        supplierName = Console.ReadLine();

                        if (string.IsNullOrEmpty(supplierName))
                        {
                            Console.WriteLine("  Please, enter a supplier name name\n");
                        }

                    } while (string.IsNullOrEmpty(supplierName));
                }
                

                Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("     Category   Id            Decription             Brand       Cost     Inventory Reor.Level  Reorder Amount  User");
                Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");

                if (filteredProductToReorderLevel.Count > 0)
                {
                    foreach (var product in filteredProductToReorderLevel)
                    {
                            Console.WriteLine("   " + product.GetCategory().PadRight(10).Substring(0, 10) + " | " + product.GetProductId().ToString().PadRight(4) + " | " + product.GetDescription().PadRight(25).Substring(0, 25) + " | " + product.GetBrand().PadRight(10).Substring(0, 10) + " | " + product.GetCost().ToString("C").PadRight(7) + " | " + product.GetInventory().ToString().PadRight(8) + " | " + product.GetReorderLevel().ToString().PadRight(8) + " | " + (product.ManageInventoryLevel() * product.GetCost()).ToString("C").PadRight(12) + " | " + product.GetUser().PadRight(8).Substring(0, 8));
                    }
                    string saveOrders = "";

                    Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"  Total reorder amount --> {reorderamount.ToString("C")}");
                    Console.ForegroundColor = ConsoleColor.Black;

                    do
                    {
                        Console.Write($"  Would you like to save the order (Y/N):");
                        saveOrders = Console.ReadLine();
                        if (string.IsNullOrEmpty(saveOrders))
                        {
                            Console.WriteLine("  Please, enter Y or N \n");
                        }
                    } while (saveOrders.ToLower() != "y" && saveOrders.ToLower() != "n" || string.IsNullOrEmpty(saveOrders));

                    if (saveOrders.ToLower() == "y")
                    {
                        foreach (var product in filteredProductToReorderLevel)
                        {
                            productId = product.GetProductId();
                            double cost = product.GetCost();
                            quantityOrder = product.ManageInventoryLevel();

                            //Updating the inventory sum the quantity order
                            product.SetOrderInventory(quantityOrder);

                            //Adding the products orders SD
                            Orders order = new Orders(now, supplierName, actualOrderId, productId, quantityOrder, cost, userNameLoggin);
                            GlobalStructures.ordersList.Add(order);

                        }

                        //Updating the number of orderId using SetOrders()
                        firstNumber.SetOrders();

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"  The order has been saved");
                        Console.WriteLine($"  Press any key to continue...");
                        Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                }
                else
                {
                    Console.WriteLine("  There is not any product with reorder level");
                    Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"  Press any key to continue...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("  Error ocurred during the view! " + ex.Message);
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
        }//End of Reorder method

        /// <summary>
        /// Method to register discounts in products filter by category
        /// </summary>
        public void DiscountsPromotions() 
        {
            try 
            {
                //Declaring variables
                int productId;
                string categoryOptionString = "";
                int categoryOption = 0;
                bool isValidInt = false;
                string category = "";

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n  Inventory Management System - Retail Store\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("  Discounts & Promptions");
                Console.WriteLine("  -----------------------");

                do
                {
                    Console.WriteLine("  (1). Cloth  (2) Electronics (3) Toys&games");
                    Console.Write("  Choose an option: ");
                    categoryOptionString = Console.ReadLine().Trim();

                    isValidInt = int.TryParse(categoryOptionString, out categoryOption);

                    if (!isValidInt || categoryOption <= 0 || categoryOption > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write($"  Please, enter numbers 1 to 3\n");
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        isValidInt = true;
                    }
                } while (!isValidInt || categoryOption <= 0 || categoryOption > 3);

                //Evaluating option choosen by the user 
                switch (categoryOption)
                {
                    //In case that product is a Cloth
                    case 1:
                        category = "Cloth";
                        break;
                    case 2:
                        category = "Electronics";
                        break;
                    case 3:
                        category = "Toys&games";
                        break;
                }

                //Create a SD filter by category 
                var orderedProductsList = GlobalStructures.productList.Where(p => p.GetCategory() == category).ToList();

                PrintProductsToDiscounts("  Products to Discounts & Promotions", orderedProductsList);


                //Pattern to evalute a valud 
                string marginOfGainString = "";
                double marginOfGain=0.0;
                string patternMarginOfGain = @"^\d+(\.\d{1,2})?$";
                do
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("  Enter % the Margin of Gain to apply a discount and promition: ");
                    Console.ForegroundColor = ConsoleColor.Black;
                    marginOfGainString = Console.ReadLine();

                    if (!double.TryParse(marginOfGainString, out marginOfGain) && Regex.IsMatch(marginOfGainString, patternMarginOfGain))
                    {
                        Console.WriteLine("  Please, enter a margin of gain for the product\n");
                    }
                } while (!double.TryParse(marginOfGainString, out marginOfGain) && Regex.IsMatch(marginOfGainString, patternMarginOfGain));


                //Interating the SD to update the MarginOfGain
                foreach (var product in orderedProductsList)
                {
                    //Using public methods to update proteted properties
                    product.SetMarginOfGain(marginOfGain);
                    product.SetPrice(marginOfGain);

                }
                Console.WriteLine($"  The Margin of Main was updated to {marginOfGain}");

                PrintProductsToDiscounts($"  Products with updated Margin of Gain to {marginOfGain}", orderedProductsList);
                Console.WriteLine($"  Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("  Error ocurred during the view! " + ex.Message);
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
        }//End of discount and promotios method


        /// <summary>
        /// Methdd to print the products category filter by the user
        /// </summary>
        /// <param name="title"></param>
        /// <param name="orderedProductsList"></param>
        public void PrintProductsToDiscounts(string title, List<Product> orderedProductsList)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n  Inventory Management System - Retail Store");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(title);
                Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("     Category   Id            Decription             Brand       Cost    MarginOfGain  Inventory    Price     User ");
                Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");

                if (orderedProductsList.Count > 0)
                {
                    foreach (var product in orderedProductsList)
                    {
                        Console.WriteLine("   " + product.GetCategory().PadRight(10).Substring(0, 10) + " | " + product.GetProductId().ToString().PadRight(4) + " | " + product.GetDescription().PadRight(25).Substring(0, 25) + " | " + product.GetBrand().PadRight(10).Substring(0, 10) + " | " + product.GetCost().ToString("C").PadRight(7) + " | " + product.GetMarginOfGain().ToString().PadRight(10) + " | " + product.GetInventory().ToString().PadRight(7) + " | " + product.GetPrice().ToString("C").PadRight(8) + " | " + product.GetUser().PadRight(8).Substring(0, 8));
                    }
                }
                else
                {
                    Console.WriteLine("  There is not any product in any category to show");
                }
                Console.WriteLine("  -------------------------------------------------------------------------------------------------------------------");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("  Error ocurred! " + ex.Message);
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
        }
    }
}
