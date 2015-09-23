using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoanManagementSystem.DBModel
{
    public partial class area : IDataErrorInfo,INotifyPropertyChanged
    {
        private string _id;
        private string _areaCode;
        private string _areaName;
        private string _remark;
        private bool? _status;
        private string _insertUserId;
        private string _updateUserId;
        private DateTime? _insertDateTime;
        private DateTime? _updateDateTime;

        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }
        public string AREA_CODE
        {
            get
            {
                return _areaCode;
            }
            set
            {
                _areaCode = value;
                OnPropertyChanged("AREA_CODE");
            }
        }
        public string AREA_NAME
        {
            get
            {
                return _areaName;
            }
            set
            {
                _areaName = value;
                OnPropertyChanged("AREA_NAME");
            }
        }
        public string REMARK 
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
                OnPropertyChanged("REMARK");
            }
        }
        public Nullable<bool> STATUS
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("STATUS");
            }
        }
        public string INSERT_USER_ID
        {
            get
            {
                return _insertUserId;
            }
            set
            {
                _insertUserId = value;
                OnPropertyChanged("INSERT_USER_ID");
            }
        }
        public string UPDATE_USER_ID
        {
            get
            {
                return _updateUserId;
            }
            set
            {
                _updateUserId = value;
                OnPropertyChanged("UPDATE_USER_ID");
            }
        }
        public Nullable<System.DateTime> INSERT_DATETIME
        {
            get
            {
                return _insertDateTime;
            }
            set
            {
                _insertDateTime = value;
                OnPropertyChanged("INSERT_DATETIME");
            }
        }
        public Nullable<System.DateTime> UPDATE_DATETIME
        {
            get
            {
                return _updateDateTime;
            }
            set
            {
                _updateDateTime = value;
                OnPropertyChanged("UPDATE_DATETIME");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

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
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "AREA_CODE")
                {
                    if (string.IsNullOrEmpty(AREA_CODE))
                    {
                        result = "Area Code cannot be empty";
                    }
                }
                if (columnName == "AREA_NAME")
                {
                    if (String.IsNullOrWhiteSpace(AREA_NAME))
                    {
                        result = "Area Name cannot be empty";
                    }
                }
                if (columnName == "ID")
                {
                    if (string.IsNullOrWhiteSpace(ID))
                    {
                        result = "Area Cannot be Empty";
                    }
                }

                return result;
            }
        }

    }
}
