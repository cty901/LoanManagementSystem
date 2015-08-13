//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoanManagementSystem.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class loan
    {
        public loan()
        {
            this.payments = new HashSet<payment>();
        }
    
        public string ID { get; set; }
        public string LOAN_ID { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public string END_DATE { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public Nullable<decimal> INSTALLMENT { get; set; }
        public Nullable<bool> LOAN_STATUS { get; set; }
        public string REMARK { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public string INSERT_USER_ID { get; set; }
        public string UPDATE_USER_ID { get; set; }
        public Nullable<System.DateTime> INSERT_DATETIME { get; set; }
        public Nullable<System.DateTime> UPDATE_DATETIME { get; set; }
        public string FK_EMPLOYEE_ID { get; set; }
        public string FK_CUSTOMER_ID { get; set; }
        public string FK_LOAN_TYPE_ID { get; set; }
        public string FK_BRANCH_ID { get; set; }
    
        public virtual branch branch { get; set; }
        public virtual customer customer { get; set; }
        public virtual employee employee { get; set; }
        public virtual loan_type loan_type { get; set; }
        public virtual ICollection<payment> payments { get; set; }
    }
}
