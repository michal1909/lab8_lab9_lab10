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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string result = "";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            result = (Double.Parse(la.Text) + Double.Parse(lb.Text)).ToString();
            lresult.Content = result;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            result = (Double.Parse(la.Text) - Double.Parse(lb.Text)).ToString();
            lresult.Content = result;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            result = (Double.Parse(la.Text) * Double.Parse(lb.Text)).ToString();
            lresult.Content = result;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (lb.Text.Equals("0"))
                MessageBox.Show("Nie można dzielić przez zero!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            else
            {
                result = (Double.Parse(la.Text) / Double.Parse(lb.Text)).ToString();
                lresult.Content = result;
            }
        }
    }
}
