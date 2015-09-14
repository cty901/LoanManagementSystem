using LoanManagementSystem.DBModel;
using System;
using System.Collections.Generic;
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

using LoanManagementSystem.Model.SMSModel;
using MahApps.Metro.Controls.Dialogs;
using LoanManagementSystem.View.WpfWindow;
using LoanManagementSystem.Util;
using LoanManagementSystem.DBService.Implementions;

namespace LoanManagementSystem.View.WpfPage.Settings.Content
{
    /// <summary>
    /// Interaction logic for AddArea.xaml
    /// </summary>
    public partial class AddArea : Page
    {
        private static AddArea _instance;
        private int _noOfErrorsOnScreen = 0;

        private AddArea()
        {
            InitializeComponent();
            GridAddArea.DataContext = new area();
        }

        public static AddArea Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AddArea();
                }
                return _instance;
            }
        }

        private bool ValidData()
        {
            ForceValidation();

            if (Validation.GetHasError(AreaCodeTextBox))
            {
                return false;
            }
            else if (Validation.GetHasError(AreaNameTextBox))
            {
                return false;
            }
            return true;
        }

        private void ForceValidation()
        {
            AreaCodeTextBox.GetBindingExpression(TextBox.TextProperty).ValidateWithoutUpdate();
            AreaNameTextBox.GetBindingExpression(TextBox.TextProperty).ValidateWithoutUpdate();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidData())
            {
                    area _area = GetAreaDetails();
                    if (AreaService.InsertArea(_area) == 1)
                    {
                        await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Area Added Success!", MessageDialogStyle.Affirmative);
                        GridAddArea.DataContext = new area();
                    }
                    else
                    {
                        await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check Deatails", MessageDialogStyle.Affirmative);
                    }
            }
            else
            {
                await MainWindow.Instance.ShowMessageAsync(Messages.TTL_MSG, "Please check errors", MessageDialogStyle.Affirmative);
            }
        }

        private area GetAreaDetails()
        {
            try
            {
                area _area = new area();
                _area.ID = IDHandller.generateID("area");

                _area.AREA_CODE = AreaCodeTextBox.Text;
                _area.AREA_NAME = AreaNameTextBox.Text;
                _area.REMARK = RemarkTextBox.Text;

                _area.STATUS = true;
                _area.INSERT_DATETIME = DateTime.Now;
                _area.INSERT_USER_ID = Session.LoggedEmployee.ID;
                _area.UPDATE_DATETIME = DateTime.Now;
                _area.UPDATE_USER_ID = Session.LoggedEmployee.ID;

                return _area;
            }
            catch (Exception)
            {
                return null;
            }
        }     
    }
}
