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
    class PaymentService:GenericService<payment>
    {
        public static int InsertPayment(payment _payment)
        {
            try
            {
                db.payments.Add(_payment);
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

        public static int UpdatePayment(payment _payment)
        {
            try
            {
                var pay = db.payments.Single(p => p.ID == _payment.ID);
                db.Entry(pay).CurrentValues.SetValues(_payment);

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

        public static int DeletePayment(payment _payment)
        {
            try                                                                                                                             
            {
                var pay= db.payments.Single(p => p.ID == _payment.ID);
                db.payments.Remove(pay);
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

        internal static PagingCollection<payment> PaymentListByLoanID(loan loan, int page)
        {

            PagingCollection<payment> pager = new PagingCollection<payment>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            List<payment> payments  = db.payments.Where(pay => pay.FK_LOAN_ID == loan.ID).ToList();

            pager.Collection = payments.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = payments.Count();
            pager.CurrentPage = page;

            return pager;
        }
        

        internal static Decimal sumPaidByLoanID(loan loan)
        {
            List<payment> payments  = db.payments.Where(pay => pay.FK_LOAN_ID == loan.ID).ToList();
            return payments.Sum(pl => pl.AMOUNT ?? 0);
        }

        internal static Decimal totalToPayByLoanID(loan loan)
        {
            List<payment> payments = db.payments.Where(pay => pay.FK_LOAN_ID == loan.ID).ToList();

            TimeSpan difference = loan.END_DATE.Value.Date - loan.START_DATE.Value.Date;
            decimal days = Convert.ToDecimal(difference.TotalDays);

            decimal d=Convert.ToDecimal(loan.INSTALLMENT) * days;
            return d;
        }

        internal static PagingCollection<payment> GetPaginatedQuickSearchedPaymentListByPage(int page, string p, DateTime _dateFrom, DateTime _dateTo)
        {
            PagingCollection<payment> pager = new PagingCollection<payment>();
            int pagesize = pager.PageSize;
            int offset = pager.PageSize * (page - 1);

            var payments = db.payments.Include("loan").Where
                (pay => (
                        pay.loan.LOAN_STATUS == true &&
                        (pay.loan.customer.area.AREA_NAME == p || p == "ALL") &&
                        pay.DATE_TIME >= _dateFrom.Date &&
                        pay.DATE_TIME <= _dateTo.Date
                      )).ToList();

            pager.Collection = payments.Skip(offset).Take(pagesize).ToList();
            pager.TotalCount = payments.Count();
            pager.CurrentPage = page;

            return pager;
        }
    }
}
