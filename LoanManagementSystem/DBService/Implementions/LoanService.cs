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
    class LoanService : GenericService<loan>
    {
        public static int InsertLoan(loan _loan)
        {
            try
            {
                db.loans.Add(_loan);
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

        public static int UpdateLoan(loan _loan)
        {
            try
            {
                var lon = db.loans.Single(ln => ln.ID == _loan.ID);
                db.Entry(lon).CurrentValues.SetValues(_loan);

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

        public static int DeleteLoan(loan _loan)
        {
            var query = db.loans.Single(ln => ln.ID == _loan.ID);
            try
            {
                db.loans.Remove(query);
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
            catch(Exception ex)
            {
                db.Entry(query).Reload();
                return 0;
            }
        }

        internal static PagingCollection<loan> GetPaginatedQuickSearchedLoanListByPage(string _searchText, int page, Boolean _loanStatusActive,string areaName)
        {
            PagingCollection<loan> pager = new PagingCollection<loan>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            List<loan> loans = null;
            if (areaName == "ALL")
            {
                loans = db.loans.Include("customer").Include("employee").Where(e => (e.LOAN_STATUS == _loanStatusActive)).ToList();
            }
            else
            {
                loans = db.loans.Include("customer").Include("employee").Where(e => (e.LOAN_STATUS == _loanStatusActive && e.customer.area.AREA_NAME == areaName)).ToList();
            }

            loans = loans.Where
                (e =>
                    (
                        e.LOAN_ID.Contains(_searchText)
                    )).ToList();
            loans = loans.OrderByDescending(ln=> ln.START_DATE).ToList();
            loans = loans.Where(e => (e.LOAN_STATUS == _loanStatusActive)).ToList();

            if (_searchText == "")
            {
                loans = loans.ToList();
                loans = loans.OrderByDescending(ln => ln.START_DATE).ToList();
                loans = loans.Where(e => (e.LOAN_STATUS == _loanStatusActive)).ToList();
            }
            
            pager.Collection = loans.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = loans.Count();
            pager.CurrentPage = page;

            return pager;
        }

        internal static PagingCollection<loan> GetPaginatedLoanListByPage(int page, string areaName)
        {
            PagingCollection<loan> pager = new PagingCollection<loan>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            List<loan> loans = null;
            if (areaName == "ALL")
            {
                loans = db.loans.Include("customer").Include("employee").Where(e => (e.LOAN_STATUS == true)).ToList();
            }
            else
            {
                loans = db.loans.Include("customer").Include("employee").Where(e => (e.LOAN_STATUS == true && e.customer.area.AREA_NAME == areaName)).ToList();
            }

           // var query = db.loans.Join(db.customers, l => l.FK_CUSTOMER_ID, c => c.ID, (l, c) => new { loan = l, customer = c }).ToList();

           //         var query = database.Posts    // your starting point - table in the "from" statement
           //.Join(database.Post_Metas, // the source table of the inner join
           //   post => post.ID,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
           //   meta => meta.Post_ID,   // Select the foreign key (the second part of the "on" clause)
           //   (post, meta) => new { Post = post, Meta = meta }) // selection
           //.Where(postAndMeta => postAndMeta.Post.ID == id);    // where statement


            //var loans = db.loans.Where(e => e.LOAN_STATUS == true).ToList();
           
            //var loans = (from l in db.loans
            //             join c in db.customers on l.FK_CUSTOMER_ID equals c.ID into cus
            //             join e in db.employees on l.FK_EMPLOYEE_ID equals e.ID into emp
            //             select new loan()
            //             {
            //                 ID = l.ID
            //             }).ToList();
            
            loans = loans.OrderByDescending(q=>q.START_DATE).ToList();

            pager.Collection = loans.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = loans.Count();
            pager.CurrentPage = page;

            return pager;
        }

        internal static loan getLoanByID(string id)
        {
            try
            {
                return db.loans.Single(ln => ln.ID == id);
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
                return null;
            }

        }


        internal static void ReloadLoanEntity()
        {
            db.Entry(Session.SelectedLoan).Reload();
        }

        internal static PagingCollection<loan> GetPaginatedQuickSearchedLoanListByPage(int page, string p, DateTime _dateFrom, DateTime _dateTo)
        {
            PagingCollection<loan> pager = new PagingCollection<loan>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            var loans = db.loans.Include("customer").Include("employee").Where
                (ln =>( 
                        ln.LOAN_STATUS==true &&
                        (ln.customer.area.AREA_NAME==p || p=="ALL") &&
                        ln.START_DATE >= _dateFrom.Date &&
                        ln.START_DATE <= _dateTo.Date                        
                      )).ToList();

            pager.Collection = loans.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = loans.Count();
            pager.CurrentPage = page;

            return pager;
        }
    }
}
