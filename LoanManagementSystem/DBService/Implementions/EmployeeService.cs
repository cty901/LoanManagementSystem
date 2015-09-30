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
                return db.employees.Where(m => m.USERNAME.Equals(userName) && m.PASSWORD.Equals(password) && m.ISRESIGN == false && m.STATUS == true).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal static PagingCollection<employee> GetPaginatedQuickSearchedEmployeeListByPage(string _searchText, int page)
        {
            PagingCollection<employee> pager = new PagingCollection<employee>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            List<employee> employees = null;

            if (_searchText != "")
            {
                employees = db.employees.Where
                    (e =>
                        (
                            e.FIRST_NAME == _searchText ||
                            e.LAST_NAME == _searchText
                            //|| e.hrm_contacts.Where(c => c.HOME == searche.hrm_contacts.Single().HOME)
                        ) && e.STATUS == true).ToList();
            }
            else
            {
                employees = db.employees.Where(e => e.STATUS == true).ToList();
            }
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

            var employees = db.employees.Where(e => e.ISRESIGN == false && e.STATUS == true).ToList();

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

        public static employee getEmployeeByID(string id)
        {
            var employee = db.employees.Where(e => e.ID == id && e.STATUS == true).SingleOrDefault();

            if (employee != null)
            {
                return employee;
            }
            else
            {
                return null;
            }
        }


        public static int DeleteEmployee(employee employee)
        {

            var query=db.employees.Single(c => c.ID == employee.ID);
            try
            {
                db.employees.Remove(query);
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
            catch (Exception ex)
            {
                db.Entry(query).Reload();
                return 0;
            }
        }
    }
}
