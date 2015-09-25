using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoanManagementSystem.View.WpfPage.Customer.Content
{
    /// <summary>
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page, INotifyPropertyChanged
    {
        private static DetailsPage _instance;
        private byte[] _imageData { get; set; }
        public IList<string> ErrorList { get; set; }
        public List<Control> ControlList { get; set; }
        public List<area> AreaCodeList { get; set; }
        private customer _customer;
        private Mode _mode;

        public customer Customer
        {
            get
            {
                return _customer;
            }
        }

        public Mode mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                OnPropertyChanged("mode");
            }
        }

        private DetailsPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public DetailsPage(Mode mode)
        {
            InitializeComponent();
            // setAreaCodeToComboBox();
            this.mode = mode;

            if (mode.Equals(Mode.EDIT))
            {
                if (Session.SelectedCustomer != null)
                {
                    _customer = Session.SelectedCustomer;
                    GridCustomerInfo.DataContext = _customer;
                }
            }
            if (mode.Equals(Mode.VIEW))
            {
                List<Control> ControlList = HandleControllers.GetLogicalChildCollection<Control>(this);
                HandleControllers.enableContent(ControlList, false, false, false, false, false);

                CustomerPage.ViewMode = Mode.VIEW;

                if (Session.SelectedCustomer != null)
                {
                    _customer = Session.SelectedCustomer;
                    GridCustomerInfo.DataContext = _customer;
                }
            }
            if (mode.Equals(Mode.NEW))
            {
                this.mode = mode;
                _customer = new customer();
                GridCustomerInfo.DataContext = _customer;
            }
            this.DataContext = this;
            _instance = this;
        }

        public void setAreaCodeToComboBox()
        {
            AreaCodeList = (List<area>)AreaService.getAreaCodes();
            AreaCodeComboBox.ItemsSource = AreaCodeList;
            AreaCodeComboBox.DisplayMemberPath = "AREA_NAME";
            AreaCodeComboBox.DataContext = this;
        }

        public static DetailsPage Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DetailsPage();
                }

                return _instance;
            }
        }

        public customer GetCustomerDetails(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.mode == Mode.NEW)
                {
                    _customer.ID = IDHandller.generateID("customer");
                    _customer.INSERT_DATETIME = DateTime.Now;
                    _customer.INSERT_USER_ID = Session.LoggedEmployee.ID;
                }
                _customer.CUSTOMER_ID = Convert.ToInt16(CusCodeTextBox.Text);               
                _customer.GENDER = getGender();
                _customer.ISACTIVE = true;
                _customer.FK_AREA_ID = ((area)getAreaCodeComboxSelectedArea()).ID;
                _customer.STATUS = true;
                _customer.UPDATE_DATETIME = DateTime.Now;
                _customer.UPDATE_USER_ID = Session.LoggedEmployee.ID;

                return _customer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void SetCustomerDetails(customer _customer)
        {
            try
            {
                GridCustomerInfo.DataContext = _customer;

                AreaCodeComboBox.SelectedItem = AreaService.GetAreaByID(_customer.FK_AREA_ID);
                
                CusCodeTextBox.Text = _customer.CUSTOMER_ID.ToString();

                CusFNameTextBox.Text=_customer.FIRST_NAME;
                CusLNameTextBox.Text=_customer.LAST_NAME;

                IDTypeComboBox.SelectedIndex = setID_Type(_customer.ID_TYPE);

                CusIDTextBox.Text=_customer.ID_NUM;
                CusBirthDayPicker.SelectedDate = _customer.DOB;
                setGender(_customer.GENDER);

                CusAddressTextBox.Text=_customer.ADDRESS;
                CusMobile1TextBox.Text=_customer.PHONE_HP1;
                CusMobile2TextBox.Text=_customer.PHONE_HP2;
                CusResidencePhoneTextBox.Text=_customer.PHONE_RECIDENCE;

                CusReligionTextBox.Text =_customer.RELIGION;
                CusCivilStatus.Text=_customer.CIVIL_STATUS;
                CusNationalityTextBox.Text=_customer.NATIONALITY ;
                _customer.ISACTIVE = true;

                _customer.STATUS = true;
                _customer.INSERT_DATETIME = DateTime.Now;
                _customer.INSERT_USER_ID = Session.LoggedEmployee.ID;
                _customer.UPDATE_DATETIME = DateTime.Now;
                _customer.UPDATE_USER_ID = Session.LoggedEmployee.ID;

                this.UpdateLayout();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private int setID_Type(string ID_TYPE)
        {
            if (ID_TYPE != null)
            {
                if (string.Equals(ID_TYPE,"nic",StringComparison.OrdinalIgnoreCase))
                {
                    return 0;
                }
                else if (string.Equals(ID_TYPE, "dl", StringComparison.OrdinalIgnoreCase))
                {
                    return 2;
                }
                else if (string.Equals(ID_TYPE, "pp", StringComparison.OrdinalIgnoreCase))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        private string getGender()
        {
            string SEX = "";

            if (CusGenderRBM.IsChecked == true)
                SEX = ((Label)CusGenderRBM.Content).Content.ToString();
            else if (CusGenderRBF.IsChecked == true)
                SEX = ((Label)CusGenderRBF.Content).Content.ToString();

            return SEX;
        }

        private void setGender(string gender)
        {
            if (String.Equals(gender, "male", StringComparison.OrdinalIgnoreCase))
                CusGenderRBM.IsChecked = true;
            else
                CusGenderRBF.IsChecked = true;
        }

        private string setIDType(string id_type)
        {
            if (id_type == "NIC")
            {
                return "nic";
            }
            else if (id_type == "Passport")
            {
                return "pp";
            }
            else
            {
                return "dl";
            }

        }

      
        public async void CustomerDetailsSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_hasValidData())
            {
                if (this.mode==Mode.NEW)
                {
                    customer cus = GetCustomerDetails(sender,e);
                    if (CustomerService.InsertCustomer(cus) == 1)
                    {
                        await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Customer Added Success!", MessageDialogStyle.Affirmative);
                        cus.NeedToSave = false;
                        clearDetailsPage();
                        QuickSearchPage.Instance.RefreshPage();
                    }
                    else
                    {
                        await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
                    }
                }

                else if (this.mode == Mode.EDIT || this.mode == Mode.VIEW)
                {
                    customer cus = GetCustomerDetails(sender,e);

                    cus.ID = Session.SelectedCustomer.ID;

                    if (CustomerService.UpdateCustomer(cus) == 1)
                    {
                        await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Customer Update Success!", MessageDialogStyle.Affirmative);
                        cus.NeedToSave = false;
                        MainWindow.Instance.setLoginDeatails();
                        QuickSearchPage.Instance.RefreshPage();
                    }
                    else
                    {
                        await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
                    }
                }
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check errors", MessageDialogStyle.Affirmative);
            }
        }

        public void clearDetailsPage()
        {
            if (mode.Equals(Mode.NEW))
            {
                _customer = new customer();
                GridCustomerInfo.DataContext = _customer;
            }
            else if (mode.Equals(Mode.EDIT))
            {
                _customer = CustomerService.RefreshCustomerByID(Session.SelectedCustomer);
                GridCustomerInfo.DataContext = new customer();
                GridCustomerInfo.DataContext = _customer;
            }
            else if (mode.Equals(Mode.VIEW))
            {
                _customer = CustomerService.RefreshCustomerByID(Session.SelectedCustomer);
                GridCustomerInfo.DataContext = _customer;
            }
        }

        private void CustomerDetailsCancelButton_Click(object sender, RoutedEventArgs e)
        {
            clearDetailsPage();
        }

        private void AreaCodeRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            setAreaCodeToComboBox();
        }

        private bool _hasValidData()
        {
            _forceValidation();

            if (Validation.GetHasError(CusFNameTextBox))
            {
                return false;
            }
            else if (Validation.GetHasError(CusLNameTextBox))
            {
                return false;
            }
            else if (Validation.GetHasError(CusIDTextBox))
            {

                return false;
            }
            else if (Validation.GetHasError(CusMobile1TextBox))
            {

                return false;
            }
            else if (Validation.GetHasError(CusAddressTextBox))
            {

                return false;
            }
            else if (Validation.GetHasError(AreaCodeComboBox))
            {

                return false;
            }
            else if (Validation.GetHasError(CusCodeTextBox))
            {
                return false;
            }
            return true;
        }

        private void _forceValidation()
        {
            CusFNameTextBox.GetBindingExpression(TextBox.TextProperty).ValidateWithoutUpdate();
            CusLNameTextBox.GetBindingExpression(TextBox.TextProperty).ValidateWithoutUpdate();
            AreaCodeComboBox.GetBindingExpression(ComboBox.SelectedValuePathProperty).ValidateWithoutUpdate();
        }

        private void AreaCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var _area=getAreaCodeComboxSelectedArea();
            List<customer> _cusList;

            if (_area != null)
            {
                area a = (area)_area;
                _cusList = CustomerService.GetCustomerListByArea(a.ID);
                int _code=1;
                if (_cusList.Count != 0)
                {
                    var maxObject = _cusList.OrderByDescending(item => item.CUSTOMER_ID).First();

                    if (maxObject != null)
                    {
                        _code = (maxObject.CUSTOMER_ID + 1);
                    }
                }
                CusCodeTextBox.Text = _code.ToString();
            }
        }

        private object getAreaCodeComboxSelectedArea()
        {
            return AreaCodeComboBox.SelectedItem;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
