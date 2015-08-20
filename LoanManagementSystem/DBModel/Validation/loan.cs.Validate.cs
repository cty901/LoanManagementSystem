using LoanManagementSystem.DBService.Implementions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBModel
{
    public partial class loan
    {
        public List<payment> PAYMENT_LIST
        {
            get { return PaymentService.PaymentListByLoanID(this); }
        }
    }
}
