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
            else if (TableName == "payment")
            {
                TableNumber = "05";
            }
            
            string ID = String.Format("{0:d15}", (DateTime.Now.Ticks));
            ID = TableNumber + ID;

            return ID;
        }

        public static string generateCode(string Type)
        {
            Random r = new Random();
            string Code = "NAE";

            if (Type == "employee")
            {
                Code = "EMP";
            }
            else if (Type == "customer")
            {
                Code = "CUS";
            }
            else if (Type== "loan")
            {
                Code = "LON";
            }
            else if (Type == "payment")
            {
                Code = "PAY";
            }

            string ID =  (r.Next()%100000).ToString("00000");
            ID = String.Concat(Code,ID);

            return ID;
        }
    }
}
