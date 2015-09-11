using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace LoanManagementSystem.View.WpfWindow.ContainerWindows
{
    /// <summary>
    /// Interaction logic for AreaCodeWindow.xaml
    /// </summary>
    public partial class AreaCodeWindow : MetroWindow
    {
        private static AreaCodeWindow _instance;

        private AreaCodeWindow()
        {
            InitializeComponent();
        }

        public static AreaCodeWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AreaCodeWindow();
                }
                return _instance;
            }
        }
    }
}
