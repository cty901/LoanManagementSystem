using LoanManagementSystem.DBService.Implementions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBModel
{
    public partial class payment : INotifyPropertyChanged, IDataErrorInfo
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private decimal? _amount;
        private DateTime? _date_Time;

        public Nullable<decimal> AMOUNT
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged("AMOUNT");
            }
        }

        public Nullable<System.DateTime> DATE_TIME
        {
            get
            {
                return _date_Time;
            }
            set
            {
                _date_Time = value;
                OnPropertyChanged("DATE_TIME");
            }
        }

        public string DATE_TIME_in_String
        {
            get
            {
                return DATE_TIME.ToString();
            }
        }

        public loan FK_LOAN
        {
            get
            {
                return LoanService.getLoanByID(this.FK_LOAN_ID);
            }
        }

        public string CUSTOMER_NAME
        {
            get
            {
                return this.FK_LOAN.FK_CUSTOMER.FULLNAME;
            }
        }

        public string CUSTOMER_PHONE
        {
            get
            {
                return this.FK_LOAN.FK_CUSTOMER.PHONE_HP1;
            }
        }

        public string Full_Loan_Code
        {
            get
            {
                return FK_LOAN.FullLoanCode;
            }

        }

        public string AMOUNT_in_String
        {
            get
            {
                return AMOUNT.ToString();
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public string Error
        {
            get { return ""; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "AMOUNT")
                {
                    string AMOUNTinString=AMOUNT.ToString();
                    decimal d;
                    if (!decimal.TryParse(AMOUNTinString,out d))
                    {
                        result = "Please Enter Currency Value";
                    }
                }
                return result;
            }
        }
    }
}
