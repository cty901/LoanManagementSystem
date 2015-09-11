using System.ComponentModel;
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

namespace LoanManagementSystem.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public int RatingValue
        {
            get { return (int)GetValue(RatingValueProperty); }
            set { SetValue(RatingValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RatingValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatingValueProperty =
            DependencyProperty.Register("RatingValue", typeof(int), typeof(UserControl1), new UIPropertyMetadata(0));

        //public event PropertyChangedEventHandler PropertyChanged;
        //void SetValueDp(DependencyProperty property, object value, [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        //{
        //    var handler = PropertyChanged;
        //    SetValue(property, value);
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(p));
        //}
    }
    public class RatingConverter : IValueConverter
    {
        public Brush OnBrush { get; set; }
        public Brush OffBrush { get; set; }
        Boolean no_signal = false;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int rating = 0;
            int number = 0;
            if (int.TryParse(value.ToString(), out rating) && int.TryParse(parameter.ToString(), out number))
            {

                if (rating >= number)
                {
                    if (number == 5 || no_signal)
                    {
                        if (!no_signal)
                        {
                            no_signal = true;
                            ImageBrush imgBrush = new ImageBrush();
                            imgBrush.ImageSource = new BitmapImage(new Uri(@"../../images/no_signal.png", UriKind.Relative));
                            return imgBrush;
                        }
                        else
                        {
                            return Brushes.Transparent;
                        }
                    }
                    return OnBrush;
                }
                return OffBrush;
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
