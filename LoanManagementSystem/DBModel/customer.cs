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
    
    public partial class customer
    {
        public customer()
        {
            this.loans = new HashSet<loan>();
            this.sms = new HashSet<sm>();
        }
    
        public string ID { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string ID_TYPE { get; set; }
        public string ID_NUM { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public System.DateTime DOB { get; set; }
        public string GENDER { get; set; }
        public string NATIONALITY { get; set; }
        public string CIVIL_STATUS { get; set; }
        public string ADDRESS { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_HP1 { get; set; }
        public string PHONE_HP2 { get; set; }
        public string PHONE_RECIDENCE { get; set; }
        public string REMARK { get; set; }
        public Nullable<bool> ISACTIVE { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public string INSERT_USER_ID { get; set; }
        public string UPDATE_USER_ID { get; set; }
        public Nullable<System.DateTime> INSERT_DATETIME { get; set; }
        public Nullable<System.DateTime> UPDATE_DATETIME { get; set; }
    
        public virtual ICollection<loan> loans { get; set; }
        public virtual ICollection<sm> sms { get; set; }
    }
}