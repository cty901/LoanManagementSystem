using LoanManagementSystem.DBModel;
using LoanManagementSystem.DBService.Implementions;
using LoanManagementSystem.Util;
using LoanManagementSystem.View.WpfWindow;
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

namespace LoanManagementSystem.View.WpfPage.Staff
{
    /// <summary>
    /// Interaction logic for StaffInfo.xaml
    /// </summary>
    public partial class StaffInfo : Page
    {
        private static StaffInfo _instance;
        private byte[] _imageData { get; set; }
        public IList<string> ErrorList { get; set; }
        public List<Control> ControlList { get; set; }

        private StaffInfo()
        {
            InitializeComponent();
        }

        public static StaffInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StaffInfo();
                }

                return _instance;
            }
        }

        public employee GetEmployeeDetails()
        {
            try
            {
                employee employee = new employee();

                employee.EMP_ID = "CheckID";
                employee.FIRST_NAME = EmpFNameTextBox.Text;
                employee.LAST_NAME = EmpLNameTextBox.Text;
                employee.ID_TYPE = IDTypeComboBox.SelectedValue.ToString();
                employee.ID_NUM = IDNumberTextBox.Text;
                employee.DOB = Convert.ToDateTime(EmpBirthDayPicker.SelectedDate);                
                employee.GENDER = getGender();
                employee.ADDRESS = EmpAddressTextBox.Text;

               // employee.RELIGION = EmpReligionTextBox.Text;
                employee.CIVIL_STATUS = EmpCivilStateTextBox.Text;                
                employee.NATIONALITY = EmpNationalityTextBox.Text;

                employee.PROFPIC = _imageData;

                employee.ACCOUNT_TYPE = AccountTypeComboBox.SelectedValue.ToString();
                employee.PASSWORD = PasswordTextBox.Text;
                employee.USERNAME = UserNameTextBox.Text;

                employee.ISRESIGN = false;

                employee.STATUS = true;
                employee.INSERT_DATETIME = DateTime.Now;
                employee.INSERT_USER_ID = Session.LoggedEmployee.ID;
                employee.UPDATE_DATETIME = DateTime.Now;
                employee.UPDATE_USER_ID = Session.LoggedEmployee.ID;

                return employee;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetEmployeeDetails(employee employee)
        {
            try
            {
                //employee.EMP_ID = "CheckID";
                EmpFNameTextBox.Text=employee.FIRST_NAME;
                EmpLNameTextBox.Text=employee.LAST_NAME;
                if (employee.ID_TYPE != null)
                {
                    string ID_TYPE = employee.ID_TYPE;
                    if (ID_TYPE == "nic")
                    {
                        IDTypeComboBox.SelectedIndex = 0;
                    }
                    else if (ID_TYPE == "dl")
                    {
                        IDTypeComboBox.SelectedIndex=2;
                    }
                    else if (ID_TYPE == "pp")
                    {
                        IDTypeComboBox.SelectedIndex=1;
                    }
                }

                IDNumberTextBox.Text=employee.ID_NUM;
                EmpBirthDayPicker.SelectedDate = employee.DOB;
                setGender(employee.GENDER);     

                // employee.RELIGION = EmpReligionTextBox.Text;
                EmpCivilStateTextBox.Text=employee.CIVIL_STATUS;
                EmpNationalityTextBox.Text=employee.NATIONALITY ;

                _imageData=employee.PROFPIC;

                if (employee.ACCOUNT_TYPE != null)
                {
                    string ACCOUNT_TYPE = employee.ACCOUNT_TYPE;
                    if (ACCOUNT_TYPE == "admin")
                    {
                        AccountTypeComboBox.SelectedIndex = 0;
                    }
                    else if (ACCOUNT_TYPE == "staff")
                    {
                        AccountTypeComboBox.SelectedIndex = 1;
                    }
                    else if (ACCOUNT_TYPE == "other")
                    {
                        AccountTypeComboBox.SelectedIndex = 2;
                    }
                }
                PasswordTextBox.Text=employee.PASSWORD;
                UserNameTextBox.Text=employee.USERNAME;

                employee.ISRESIGN = false;

                employee.STATUS = true;
                employee.INSERT_DATETIME = DateTime.Now;
                employee.INSERT_USER_ID = Session.LoggedEmployee.ID;
                employee.UPDATE_DATETIME = DateTime.Now;
                employee.UPDATE_USER_ID = Session.LoggedEmployee.ID;
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private string getGender()
        {
            string SEX = "";

            if (GenderRBM.IsChecked == true)
                SEX = ((Label)GenderRBM.Content).Content.ToString();
            else if (GenderRBF.IsChecked == true)
                SEX = ((Label)GenderRBF.Content).Content.ToString();

            return SEX;
        }

        private void setGender(string gender)
        {
            if (gender == "male")
                GenderRBM.IsChecked = true;
            else
                GenderRBF.IsChecked = true;
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                FileStream fs;
                BinaryReader br;

                fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                _imageData = br.ReadBytes((int)fs.Length);

                ProfPicBox.ImageSource = new BitmapImage(new Uri(filename)); //Image.FromFile(newFileName);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void EmployeeDetailsSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.mode==Mode.NEW)
            {
                employee emp = GetEmployeeDetails();
                if (EmployeeService.InsertEmployee(emp) == 1)
                {
                    await MainWindow.Instance.ShowMessageAsync("Employe Insert Success", "Employee Added Success!", MessageDialogStyle.Affirmative);
                    QuickSearchPageStaff.Instance.RefreshPage();
                }
                else
                {
                    await MainWindow.Instance.ShowMessageAsync("Employe Insert Error", "Please check Deatails", MessageDialogStyle.Affirmative);
                }
            }
        }
    }
}
