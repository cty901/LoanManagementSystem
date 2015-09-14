using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfWindow;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
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
    public partial class DetailsPage : Page
    {
        private static DetailsPage _instance;
        private byte[] _imageData { get; set; }
        public IList<string> ErrorList { get; set; }
        public List<Control> ControlList { get; set; }
        Mode mode;

        private DetailsPage()
        {
            InitializeComponent();
        }

        public DetailsPage(Mode mode)
        {
            InitializeComponent();
            this.mode = mode;

            if (mode.Equals(Mode.EDIT))
            {
                if (Session.SelectedCustomer != null)
                {
                    SetCustomerDetails(Session.SelectedCustomer);
                }
            }
            if (mode.Equals(Mode.VIEW))
            {
                List<Control> ControlList = HandleControllers.GetLogicalChildCollection<Control>(this);
                HandleControllers.enableContent(ControlList, false, false, false, false, false);

                CustomerPage.ViewMode = Mode.VIEW;

                if (Session.SelectedCustomer != null)
                {
                    SetCustomerDetails(Session.SelectedCustomer);
                }
            }
            if (mode.Equals(Mode.NEW))
            {
            }
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

        public customer GetCustomerDetails()
        {
            try
            {
                customer _customer = new customer();

                _customer.ID=IDHandller.generateID("customer");
                //_customer.CUSTOMER_ID = CusCodeTextBox.Text;

                _customer.FIRST_NAME = CusFNameTextBox.Text;
                _customer.LAST_NAME = CusLNameTextBox.Text;
                _customer.ID_TYPE=setIDType(IDTypeComboBox.Text);
                _customer.ID_NUM = CusIDTextBox.Text;
                _customer.DOB = Convert.ToDateTime(CusBirthDayPicker.SelectedDate);                
                _customer.GENDER = getGender();

                _customer.ADDRESS = CusAddressTextBox.Text;
                _customer.PHONE_HP1 = CusMobile1TextBox.Text;
                _customer.PHONE_HP2 =CusMobile2TextBox.Text;
                _customer.PHONE_RECIDENCE = CusResidencePhoneTextBox.Text;

                _customer.RELIGION = CusReligionTextBox.Text;
                _customer.CIVIL_STATUS = CusCivilStatus.Text;                
                _customer.NATIONALITY = CusNationalityTextBox.Text;

                _customer.ISACTIVE = true;

                _customer.STATUS = true;
                _customer.INSERT_DATETIME = DateTime.Now;
                _customer.INSERT_USER_ID = Session.LoggedEmployee.ID;
                _customer.UPDATE_DATETIME = DateTime.Now;
                _customer.UPDATE_USER_ID = Session.LoggedEmployee.ID;

                return _customer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetCustomerDetails(customer _customer)
        {
            try
            {
                //CusCodeTextBox.Text = _customer.CUSTOMER_ID;

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
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
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

        //private  async void LoadImageButton_Click(object sender, RoutedEventArgs e)
        //{
        //    bool error = false;

        //    try
        //    {
        //        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

        //        dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

        //        Nullable<bool> result = dlg.ShowDialog();

        //        if (result == true)
        //        {
        //            string filename = dlg.FileName;
        //            FileStream fs;
        //            BinaryReader br;

        //            fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
        //            br = new BinaryReader(fs);
        //            _imageData = br.ReadBytes((int)fs.Length);

        //            ProfPicBox.ImageSource = new BitmapImage(new Uri(filename)); //Image.FromFile(newFileName);
        //        }
        //    }
        //    catch
        //    {
        //        error = true;
        //    }
        //    if (error)
        //    {
        //        await MainWindow.Instance.ShowMessageAsync("Image Uploding Error","Image Content is Corrupted",MessageDialogStyle.Affirmative);
        //    }
        //}


        private async void EmployeeDetailsSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.mode==Mode.NEW)
            {
                customer cus = GetCustomerDetails();
                if (CustomerService.InsertCustomer(cus) == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync("Customer Insert Success", "Customer Added Success!", MessageDialogStyle.Affirmative);
                    clearDetailsPage();
                    QuickSearchPage.Instance.RefreshPage();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync("Customer Insert Error", "Please check Deatails", MessageDialogStyle.Affirmative);
                }
            }

            else if (this.mode == Mode.EDIT)
            {
                customer cus = GetCustomerDetails();
                cus.ID = Session.SelectedCustomer.ID;

                if (CustomerService.UpdateCustomer(cus) == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync("Customer Update Success", "Customer Added Success!", MessageDialogStyle.Affirmative);
                    MainWindow.Instance.setLoginDeatails();
                    QuickSearchPage.Instance.RefreshPage();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync("Customer Update Error", "Please check Deatails", MessageDialogStyle.Affirmative);
                }
            }
        }

        private void clearDetailsPage()
        {
            CusFNameTextBox.Clear();
            CusLNameTextBox.Clear();
            CusIDTextBox.Clear();
            IDTypeComboBox.SelectedIndex=0;
            setGender("male");
            CusBirthDayPicker.SelectedDate = null;
            CusNationalityTextBox.Clear();
            CusReligionTextBox.Clear();
            CusCivilStatus.Clear();
            CusMobile1TextBox.Clear();
            CusMobile2TextBox.Clear();
            CusResidencePhoneTextBox.Clear();
            CusAddressTextBox.Clear();
            CusCodeTextBox.Clear();
        }

        private void CustoerDetailsCancelButton_Click(object sender, RoutedEventArgs e)
        {
            clearDetailsPage();
        }

        private void CustomerCodeGenButton_Click(object sender, RoutedEventArgs e)
        {
            // CusCodeTextBox.Text = IDHandller.generateCode("customer");
        }        
    }
}
