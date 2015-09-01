using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBModel
{
    public partial class loan
    {
        public PagingCollection<payment> PAYMENT_LIST(int page)
        {
            return PaymentService.PaymentListByLoanID(this,page); 
        }

        public decimal sumPaidByLoanID()
        {
            return PaymentService.sumPaidByLoanID(this);
        }

        public decimal totalToPayByLoanID()
        {
            return PaymentService.totalToPayByLoanID(this);
        }

        public string LSTATUS
        {
            get
            {
                if (LOAN_STATUS == true)
                {
                    return "Active Loan";
                }
                else
                {
                    return "Not Active";
                }
            }
        }
    }
}
