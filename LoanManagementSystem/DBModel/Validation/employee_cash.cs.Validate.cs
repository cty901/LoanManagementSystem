using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBModel
{
    using LoanManagementSystem.DBService.Implementions;
    using System;
    using System.Collections.Generic;

    public partial class employee_cash
    {
        //public string ID { get; set; }
        //public Nullable<System.DateTime> TRANSACTION_DATE_TIME { get; set; }
        //public string TYPE { get; set; }
        //public Nullable<decimal> AMOUNT { get; set; }
        //public string REMARK { get; set; }
        //public Nullable<bool> STATUS { get; set; }
        //public string INSERT_USER_ID { get; set; }
        //public string UPDATE_USER_ID { get; set; }
        //public Nullable<System.DateTime> INSERT_DATETIME { get; set; }
        //public Nullable<System.DateTime> UPDATE_DATETIME { get; set; }
        //public string FK_EMPLOYEE_ID { get; set; }

        //public virtual employee employee { get; set; }

        public employee FK_EMPLOYEE
        {
            get
            {
                return EmployeeService.getEmployeeByID(this.FK_EMPLOYEE_ID);
            }
        }
        public Nullable<decimal> Borrow { get; set; }
        public Nullable<decimal> Return { get; set; }
    }
}