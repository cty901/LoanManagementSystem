using System.ComponentModel.DataAnnotations;
using LoanManagementSystem.DBService.Implementions;

namespace LoanManagementSystem.DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class employee// : IValidatableObject, IDataErrorInfo,INotifyPropertyChanged
    {

        //public string FULLNAME
        //{
        //    get { return FIRST_NAME + " " + LAST_NAME; }
        //}

        //public Boolean TODAY_BORROWING_STATUS
        //{
        //    get { return Employee_CashService.Today_Borrowing_Status(this); }
        //}

        //public List<employee_cash> TRANSACTION_LIST
        //{
        //    get { return Employee_CashService.TodayBorrowingListByEmpID(this); }
        //}

        ////public Nullable<bool> ISRESIGN { get; set; }
        ////public Nullable<bool> STATUS { get; set; }
        ////public Nullable<int> INSERT_USER_ID { get; set; }
        ////public Nullable<int> UPDATE_USER_ID { get; set; }
        ////public Nullable<System.DateTime> INSERT_DATETIME { get; set; }
        ////public Nullable<System.DateTime> UPDATE_DATETIME { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    List<ValidationResult> results = new List<ValidationResult>();
 
        //    if(String.IsNullOrWhiteSpace(FIRST_NAME))            
        //    {
        //        results.Add(new ValidationResult("LAST Name Cannot be Empty or have White Spaces"));
        //    }
        //    if (String.IsNullOrWhiteSpace(DOB.ToString()))
        //    {
        //        results.Add(new ValidationResult("DOB Cannot be Empty or have White Spaces"));
        //    }
        //    if (String.IsNullOrWhiteSpace(GENDER))
        //    {
        //        results.Add(new ValidationResult("First Name Cannot be Empty or have White Spaces"));
        //    }
        //    if (String.IsNullOrWhiteSpace(NATIONALITY))
        //    {
        //        results.Add(new ValidationResult("Nationality Cannot be Empty or have White Spaces"));
        //    }
        //    if (String.IsNullOrWhiteSpace(CIVIL_STATUS))
        //    {
        //        results.Add(new ValidationResult("Civil Status Cannot be Empty or have White Spaces"));
        //    }
        //    if (String.IsNullOrWhiteSpace(ACCOUNT_TYPE))
        //    {
        //        results.Add(new ValidationResult("Account type Cannot be Empty or have White Spaces"));
        //    }
        //    if (String.IsNullOrWhiteSpace(ADDRESS))
        //    {
        //        results.Add(new ValidationResult("Address Cannot be Empty or have White Spaces"));
        //    }
        //    if (String.IsNullOrWhiteSpace(USERNAME))
        //    {
        //        results.Add(new ValidationResult("Username Cannot be Empty or have White Spaces"));
        //    }
        //    if (String.IsNullOrWhiteSpace(PASSWORD))
        //    {
        //        results.Add(new ValidationResult("Password Cannot be Empty or have White Spaces"));
        //    }

        //    return results;
        //}

        //public string Error
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //public string this[string columnName]
        //{
        //    get
        //    {
        //        string result = string.Empty;
        //        if (columnName == "FIRST_NAME")
        //        {
        //            if (this.FIRST_NAME == "")
        //                result = "First Name can not be empty";
        //        }
        //        return result;
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged(String propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}


    }
}