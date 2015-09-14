using LoanManagementSystem.DBService.Implementions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBModel
{
    public partial class customer : IDataErrorInfo
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

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "FIRST_NAME")
                {
                    if (string.IsNullOrWhiteSpace(FIRST_NAME))
                    {
                        result = "First Name cannot be empty";
                    }
                }
                if (columnName == "LAST_NAME")
                {
                    if (string.IsNullOrWhiteSpace(LAST_NAME))
                    {
                        result = "Last Name cannot be empty";
                    }
                }
                if (columnName == "FK_AREA_ID")
                {
                    if (string.IsNullOrEmpty(FK_AREA_ID))
                    {
                        result = "Please Select an Area";
                    }
                }

                return result;
            }
        }


    }
}
