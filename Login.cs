using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystemRetailStore
{
    /// <summary>
    /// Class to user that is loggin in the system
    /// </summary>
    internal class Login
    {
        public string UserName { get; set; }
        internal Login(string userName)
        {
            UserName = userName;
        }
    }
}
