using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBModel
{
    public partial class customer
    {
        public string FULLNAME
        {
            get { return FIRST_NAME + " " + LAST_NAME; }
        }
        public string CITY
        {
            get { return ADDRESS; }
        }
        public string CURRENT_STATUS
        {
            get
            {
                if (Convert.ToBoolean(ISACTIVE))
                {
                    return "Active";
                }
                else
                {
                    return "Disabled";
                }
            }
        }
    }
}
