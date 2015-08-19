using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Util
{
    class IDHandller
    {
        public static string generateID(string TableName)
        {
            string TableNumber = "00";

            if (TableName == "employee")
            {
                TableNumber = "01";
            }
            else if (TableName == "employee_cash")
            {
                TableNumber = "02";
            }
            else if (TableName == "customer")
            {
                TableNumber = "03";
            }
            else if (TableName == "loan")
            {
                TableNumber = "04";
            }
            
            string ID = String.Format("{0:d15}", (DateTime.Now.Ticks));
            ID = TableNumber + ID;

            return ID;
        }
    }
}
