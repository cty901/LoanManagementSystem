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
        public PagingCollection<payment> PAYMENT_LIST
        {
            get { return PaymentService.PaymentListByLoanID(this,1); }
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
