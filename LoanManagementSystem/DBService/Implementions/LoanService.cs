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
            try
            {
                var query = db.loans.Single(ln => ln.ID == _loan.ID);
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
        }

        internal static PagingCollection<loan> GetPaginatedQuickSearchedLoanListByPage(string _searchText, int page, Boolean _loanStatusActive)
        {
            PagingCollection<loan> pager = new PagingCollection<loan>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            var loans = db.loans.Where
                (e =>
                    (
                        e.LOAN_ID.Contains(_searchText)
                    )).ToList();
            loans = loans.OrderByDescending(ln=> ln.START_DATE).ToList();
            loans = loans.Where(e => (e.LOAN_STATUS == _loanStatusActive)).ToList();

            if (_searchText == "")
            {
                loans = db.loans.ToList();
                loans = loans.OrderByDescending(ln => ln.START_DATE).ToList();
                loans = loans.Where(e => (e.LOAN_STATUS == _loanStatusActive)).ToList();
            }

            //var lns = from ln in db.loans
            //            join cus in db.customers on ln.FK_CUSTOMER_ID equals cus.ID
            //            join emp in db.employees on ln.FK_EMPLOYEE_ID equals emp.ID
            //            join brch in db.branches on ln.FK_BRANCH_ID equals brch.ID
            //            join ltype in db.loan_type on ln.FK_LOAN_TYPE_ID equals ltype.ID
            //            select ln;

            //List<loan> loans = lns.ToList();

            //var lns = db.loans
            //    .Join(db.customers
            //        , c => c.FK_CUSTOMER_ID
            //        , cm => cm.ID
            //        , (c, cm) => new { c, cm })
            //    .Join(db.employees
            //        , ccm => ccm.c.FK_EMPLOYEE_ID
            //        , t => t.ID
            //        , (ccm, t) => new {ccm,t})
            //    .Join(db.branches
            //        ,ccmt=> ccmt.ccm.c.FK_BRANCH_ID
            //        ,b => b.ID
            //        ,(ccmt,b) =>new {ccmt,b})
            //    .Join(db.loan_type
            //        ,ccmtb=> ccmtb.ccmt.ccm.c.FK_LOAN_TYPE_ID
            //        ,lty=>lty.ID
            //        ,(ccmtb,lty) =>new
            //          {
            //              LOAN_ID=ccmtb.ccmt.ccm.c.LOAN_ID,
            //              BRANCH_NAME = ccmtb.b.ADDRESS,
            //              LOAN_TYPE_NAME = lty.LOAN_TYPE_ID,
            //              STATUS=ccmtb.ccmt.ccm.c.STATUS,
            //          })
            //        .Where(a => a.LOAN_ID == _searchText).ToList();

            //dynamic loans = (dynamic)lns;

            pager.Collection = loans.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = loans.Count();
            pager.CurrentPage = page;

            return pager;
        }

        internal static PagingCollection<loan> GetPaginatedLoanListByPage(int page)
        {
            PagingCollection<loan> pager = new PagingCollection<loan>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            var loans = db.loans.Where(e => e.LOAN_STATUS == true).ToList();
            loans = loans.OrderByDescending(ln => ln.START_DATE).ToList();

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
    }
}
