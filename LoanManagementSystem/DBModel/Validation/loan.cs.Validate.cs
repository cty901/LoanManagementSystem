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

        public string FullLoanCode
        {
            get
            {
                return this.FK_CUSTOMER.AREA_SELECTED.AREA_NAME+ " / " + this.LOAN_ID;
            }

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
                    return "Inactive Loan";
                }
            }
        }

        public customer FK_CUSTOMER
        {
            get
            {
                return CustomerService.GetCustomerByID(this.FK_CUSTOMER_ID);
            }
        }

        public employee FK_EMPLOYEE
        {
            get
            {
                return EmployeeService.getEmployeeByID(this.FK_EMPLOYEE_ID);
            }
        }

        public string FK_EMPLOYEE_LASTNAME
        {
            get
            {
                return FK_EMPLOYEE.LAST_NAME;
            }
        }

        public string FK_EMPLOYEE_IDNUM
        {
            get
            {
                return FK_EMPLOYEE.ID_NUM;
            }
        }
        public string FK_CUSTOMER_IDNUM
        {
            get
            {
                return FK_CUSTOMER.ID_NUM;
            }
        }
        public string AMOUNT_in_string
        {
            get
            {
                return AMOUNT.ToString();
            }
        }
        public string INSTALLMENT_in_string
        {
            get
            {
                return INSTALLMENT.ToString();
            }
        }
        public string StartDate_in_string
        {
            get
            {
                return START_DATE.Value.Date.ToString("d");
            }
        }
    }
}
