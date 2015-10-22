using LoanManagementSystem.DBModel;
using LoanManagementSystem.Util;
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

        internal static Util.PagingCollection<employee_cash> GetPaginatedQuickSearchedEmployeePaymentListByPage(int page, DateTime _dateFrom, DateTime _dateTo)
        {
            _dateTo = _dateTo.AddSeconds(86399);

            PagingCollection<employee_cash> pager = new PagingCollection<employee_cash>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            var emp_paymentList = db.employee_cash.Where
                (emPay=>
                    (
                        emPay.TRANSACTION_DATE_TIME >= _dateFrom.Date &&
                        emPay.TRANSACTION_DATE_TIME<=_dateTo.Date

                    )).ToList();

            emp_paymentList = emp_paymentList.GroupBy(emp => new { FK_EMPLOYEE_ID = emp.FK_EMPLOYEE_ID, TYPE = emp.TYPE,TRANSACTION_DATE_TIME=emp.TRANSACTION_DATE_TIME.Value.Date }).Select(pay => new employee_cash
            {
                TRANSACTION_DATE_TIME=pay.Key.TRANSACTION_DATE_TIME,
                FK_EMPLOYEE_ID = pay.Key.FK_EMPLOYEE_ID,
                AMOUNT = pay.Sum(x => x.AMOUNT),
                TYPE=pay.Key.TYPE
            }).OrderBy(x=>(x.TRANSACTION_DATE_TIME.Value.Date)).ToList();

            //emp_paymentList = (from p in emp_paymentList
            //                  group p by new{ TRANSACTION_DATE_TIME = p.TRANSACTION_DATE_TIME, FK_EMPLOYEE_ID=p.FK_EMPLOYEE_ID});    

            pager.Collection = emp_paymentList.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = emp_paymentList.Count();
            pager.CurrentPage = page;

            return pager;
        }
    }
}
