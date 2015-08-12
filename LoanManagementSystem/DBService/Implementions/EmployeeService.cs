using LoanManagementSystem.DBModel;
using LoanManagementSystem.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBService.Implementions
{
    class EmployeeService : GenericService<employee>
    {
        public static List<employee> GetLoggedEmployeeByUserNamePassword(string userName, string password)
        {
            try
            {
                return db.employees.Where(m => m.USERNAME.Equals(userName) && m.PASSWORD.Equals(password)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static PagingCollection<employee> GetPaginatedQuickSearchedEmployeeListByPage(string _searchText, int page)
        {
            PagingCollection<employee> pager = new PagingCollection<employee>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);
            int id = 0;

            try
            {
                id = int.Parse(_searchText);
            }
            catch (Exception)
            {
            }

            var employees = db.employees.Where
                (e =>
                    (
                        e.FIRST_NAME == _searchText ||
                        e.LAST_NAME == _searchText ||
                        e.ID == id//|| e.hrm_contacts.Where(c => c.HOME == searche.hrm_contacts.Single().HOME)
                    )).ToList();

            pager.Collection = employees.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = employees.Count();
            pager.CurrentPage = page;

            return pager;
        }

        internal static PagingCollection<employee> GetPaginatedEmployeeListByPage(int page)
        {
            PagingCollection<employee> pager = new PagingCollection<employee>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            var employees = db.employees.Where(e => e.ISRESIGN == false).ToList();

            pager.Collection = employees.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = employees.Count();
            pager.CurrentPage = page;

            return pager;
        }
        public static int InsertEmployee(employee employee)
        {
            try
            {
                db.employees.Add(employee);
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

        public static int UpdateEmployee(employee employee)
        {
            try
            {
                var query = db.employees.Single(e => e.ID == employee.ID);
                db.Entry(query).CurrentValues.SetValues(employee);
                
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
        
    }
}
