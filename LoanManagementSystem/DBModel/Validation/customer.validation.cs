using LoanManagementSystem.DBService.Implementions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.DBModel
{
    public partial class customer : IDataErrorInfo,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _id;
        private int _customerID;
        private string _idType="nic";
        private string _idNum;
        private string _firstName;
        private string _lastName;
        private DateTime _dob=System.DateTime.Now;
        private string _gender="male";
        private string _nationality;
        private string _religion;
        private string _civilStatus;
        private string _address;
        private string _email;
        private string _phoneHP1;
        private string _phoneHP2;
        private string _phoneRecidence;
        private string _remark;
        private bool? _isActive;
        private bool? _status;
        private string _insertUserId;
        private string _updateUserId;
        private DateTime? _insertDateTime;
        private DateTime? _updateDateTime;
        private List<DBModel.area> _areaList;
        private DBModel.area _areaSelected;

        private int _eventCount;
        private string _fk_area_id;

        public string FullCustomerCode
        {
            get
            {
                return this.AREA_SELECTED.AREA_NAME + " / " + this.CUSTOMER_ID.ToString();
            }

        }

        public Boolean NeedToSave
        {
            get
            {
                if (_eventCount > 0)
                {
                    return true;
                }
                return false;
            }
            set
            {
                if (value == false)
                {
                    _eventCount = 0;
                }
            }
        }

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
        public List<area> AREA_LIST
        {
            get
            {
                _areaList=(List<area>)AreaService.getAreaCodes();
                return _areaList;
            }
            set
            {
                _areaList = value;
                OnPropertyChanged("AREA_LIST");
            }
        }
        public area AREA_SELECTED
        {
            get
            {
                if (_fk_area_id != null)
                {
                    _areaSelected = AreaService.GetAreaByID(_fk_area_id);
                }
                return _areaSelected;
            }
            set
            {
                _areaSelected = value;
                OnPropertyChanged("AREA_SELECTED");
            }
        }
        public int CUSTOMER_ID
        {
            get
            {
                return _customerID;
            }
            set
            {
                _customerID = value;
                OnPropertyChanged("CUSTOMER_ID");
            }
        }
        public string ID_TYPE
        {
            get
            {
                return _idType;
            }
            set
            {
                _idType = value;
                OnPropertyChanged("ID_TYPE");
            }
        }
        public string ID_NUM
        {
            get
            {
                return _idNum;
            }
            set
            {
                _idNum = value;
                OnPropertyChanged("ID_NUM");
            }
        }
        public string FIRST_NAME
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged("FIRST_NAME");
            }
        }
        public string LAST_NAME
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged("LAST_NAME");
            }
        }
        public System.DateTime DOB
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
                OnPropertyChanged("DOB");
            }
        }
        public string GENDER
        {
            get
            {
                return _gender;
            }
            set
            {
                    _gender = value;
                    OnPropertyChanged("GENDER");
            }
        }
        public string NATIONALITY
        {
            get
            {
                return _nationality;
            }
            set
            {
                _nationality = value;
                OnPropertyChanged("NATIONALITY");
            }
        }
        public string RELIGION
        {
            get
            {
                return _religion;
            }
            set
            {
                _religion = value;
                OnPropertyChanged("RELIGION");
            }
        }
        public string CIVIL_STATUS
        {
            get
            {
                return _civilStatus;
            }
            set
            {
                _civilStatus = value;
                OnPropertyChanged("CIVIL_STATUS");
            }
        }
        public string ADDRESS
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged("ADDRESS");
            }
        }
        public string EMAIL
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("EMAIL");
            }
        }
        public string PHONE_HP1
        {
            get
            {
                return _phoneHP1;
            }
            set
            {
                _phoneHP1 = value;
                OnPropertyChanged("PHONE_HP1");
            }
        }
        public string PHONE_HP2
        {
            get
            {
                return _phoneHP2;
            }
            set
            {
                _phoneHP2 = value;
                OnPropertyChanged("PHONE_HP2");
            }
        }
        public string PHONE_RECIDENCE
        {
            get
            {
                return _phoneRecidence;
            }
            set
            {
                _phoneRecidence = value;
                OnPropertyChanged("PHONE_RECIDENCE");
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
        public Nullable<bool> ISACTIVE
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                OnPropertyChanged("ISACTIVE");
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

        public string FK_AREA_ID
        {
            get
            {
                return _fk_area_id;
            }
            set
            {
                _fk_area_id = value;
                OnPropertyChanged("FK_AREA_ID");
            }
        }

        public string FULLNAME
        {
            get { return FIRST_NAME + " " + LAST_NAME; }
        }
        public string CITY
        {
            get { return ADDRESS; }
        }
        public string CURRENT_STATUS
        {
            get
            {
                if (Convert.ToBoolean(ISACTIVE))
                {
                    return "Active";
                }
                else
                {
                    return "Disabled";
                }
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
                if (columnName == "FIRST_NAME")
                {
                    if (string.IsNullOrWhiteSpace(FIRST_NAME))
                    {
                        result = "First Name cannot be empty";
                    }
                }
                if (columnName == "LAST_NAME")
                {
                    if (string.IsNullOrWhiteSpace(LAST_NAME))
                    {
                        result = "Last Name cannot be empty";
                    }
                }
                if (columnName == "ID_NUM")
                {
                    if (string.IsNullOrEmpty(ID_NUM))
                    {
                        result = "ID cannot be empty";
                    }
                }
                if (columnName == "PHONE_HP1")
                {
                    if (string.IsNullOrEmpty(PHONE_HP1))
                    {
                        result = "Phone cannot be empty";
                    }
                }
                if (columnName == "ADDRESS")
                {
                    if (string.IsNullOrEmpty(ADDRESS))
                    {
                        result = "Address cannot be empty";
                    }
                }
                if (columnName == "AREA_SELECTED")
                {
                    if (AREA_SELECTED == null)
                    {
                        result = "Please Select an Area";
                    }
                }
                if (columnName == "CUSTOMER_ID")
                {
                    if (CUSTOMER_ID == 0 || CUSTOMER_ID == null)
                    {
                        result = "Cutomer ID cannot be empty";
                    }
                }

                return result;
            }
        }        

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
                _eventCount++;
            }
        }
    }
}
