using LoanManagementSystem.DBModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBService.Implementions
{
    class Employee_CashService: GenericService<employee_cash>
    {

        public static int InsertEmployee_cash(employee_cash employee_cash)
        {
            try
            {
                db.employee_cash.Add(employee_cash);
                return db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return 0;
            }
        }

        public static int UpdateEmployee_cash(employee_cash employee_cash)
        {
            try
            {
                var query = db.employee_cash.Single(e => e.ID == employee_cash.ID);
                db.Entry(query).CurrentValues.SetValues(employee_cash);

                return db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return 0;
            }
        }

        public static int DeleteEmployee_cash(employee_cash employee_cash)
        {
            try
            {
                var query = db.employee_cash.Single(e => e.ID == employee_cash.ID);
                db.employee_cash.Remove(query);
                return db.SaveChanges();  
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return 0;
            }
        }

        public static int DeleteEmployee_cash_ByID(string ID)
        {
            try
            {
                var query = db.employee_cash.Single(e => e.ID == ID);
                db.employee_cash.Remove(query);
                return db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return 0;
            }
        }

        public static Boolean Today_Borrowing_Status(employee employee)
        {
            List<employee_cash> transaction_list=TodayBorrowingListByEmpID(employee);
            int count = transaction_list.Count;

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static List<employee_cash> TodayBorrowingListByEmpID(employee employee)
        {
            var transaction_list = db.employee_cash.Where
                (e =>
                    (
                       e.FK_EMPLOYEE_ID == employee.ID &&
                       (e.TRANSACTION_DATE_TIME.Value.Year == System.DateTime.Now.Year &&
                        e.TRANSACTION_DATE_TIME.Value.Month == System.DateTime.Now.Month &&
                        e.TRANSACTION_DATE_TIME.Value.Day == System.DateTime.Now.Day)
                    )).ToList();

            return transaction_list;
        }
    }
}
