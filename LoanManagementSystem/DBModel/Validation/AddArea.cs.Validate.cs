using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoanManagementSystem.DBModel
{
    public partial class area : IDataErrorInfo
    {
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "AREA_CODE")
                {
                    if (string.IsNullOrEmpty(AREA_CODE))
                    {
                        result = "Area Code cannot be empty";
                    }
                }
                if (columnName == "AREA_NAME")
                {
                    if (String.IsNullOrWhiteSpace(AREA_NAME))
                    {
                        result = "Area Name cannot be empty";
                    }
                }                

                return result;
            }
        }
    }
}
