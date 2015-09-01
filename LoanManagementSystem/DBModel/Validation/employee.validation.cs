using LoanManagementSystem.DBService.Implementions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBModel
{
    public partial class employee : IDataErrorInfo
    {
        public string FULLNAME
        {
            get { return FIRST_NAME + " " + LAST_NAME; }
        }

        public Boolean TODAY_BORROWING_STATUS
        {
            get { return Employee_CashService.Today_Borrowing_Status(this); }
        }

        public List<employee_cash> TRANSACTION_LIST
        {
            get { return Employee_CashService.TodayBorrowingListByEmpID(this); }
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
                    if (string.IsNullOrEmpty(FIRST_NAME))
                    {
                        result = "First Name cannot be empty";
                    }
                }
                if (columnName == "LAST_NAME")
                {
                    if (String.IsNullOrWhiteSpace(LAST_NAME))
                    {
                        result = "Last Name cannot be empty";
                    }
                }
                if (columnName == "EMP_ID")
                {
                    if (String.IsNullOrWhiteSpace(EMP_ID))
                    {
                        result = "Employee Code cannot be empty";
                    }
                }
                if (columnName == "DOB")
                {
                    if (String.IsNullOrWhiteSpace(DOB.ToString()))
                    {
                        result = "Date of birth cannot be empty";
                    }
                }
                if (columnName == "GENDER")
                {
                    if (String.IsNullOrWhiteSpace(GENDER))
                    {
                        // result = "First Name Cannot be Empty or have White Spaces";
                    }
                }
                if (columnName == "NATIONALITY")
                {
                    if (String.IsNullOrWhiteSpace(NATIONALITY))
                    {
                        result = "Nationality cannot be empty";
                    }
                }
                if (columnName == "CIVIL_STATUS")
                {
                    if (String.IsNullOrWhiteSpace(CIVIL_STATUS))
                    {
                        result = "Civil Status cannot be empty";
                    }
                }
                if (columnName == "ACCOUNT_TYPE")
                {
                    if (String.IsNullOrWhiteSpace(ACCOUNT_TYPE))
                    {
                        result = "Account type cannot be empty";
                    }
                }
                if (columnName == "ADDRESS")
                {
                    if (String.IsNullOrWhiteSpace(ADDRESS))
                    {
                        result = "Address cannot be empty";
                    }
                }
                if (columnName == "USERNAME")
                {
                    if (String.IsNullOrWhiteSpace(USERNAME))
                    {
                        result = "Username cannot be empty";
                    }
                }
                if (columnName == "PASSWORD")
                {
                    if (String.IsNullOrWhiteSpace(PASSWORD))
                    {
                        result = "Password cannot be empty";
                    }
                }

                return result;
            }
        }


    }
}
