using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Logika interakcji dla klasy StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public Student student;
        public StudentWindow(Student student = null)
        {
            InitializeComponent();
            if (student != null)
            {
                tbImie.Text = student.imie;
                tbNazwisko.Text = student.nazwisko;
                tbNrAlbumu.Text = student.NrIndeksu.ToString();
                tbWydzial.Text = student.wydzial;
            }
            this.student = student ?? new Student();
        }
        public StudentWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(tbImie.Text, "\\w+") || !Regex.IsMatch(tbNazwisko.Text, "\\w+") ||
           !Regex.IsMatch(tbNrAlbumu.Text, "\\d+") || !Regex.IsMatch(tbWydzial.Text, "\\w+"))
            {
                MessageBox.Show("Podano niepoprawne dane!");
                return;
            }
            try
            {
                student.imie = tbImie.Text;
                student.nazwisko = tbNazwisko.Text;
                student.NrIndeksu = Convert.ToInt32(tbNrAlbumu.Text);
                student.wydzial = tbWydzial.Text;
                this.DialogResult = true;
            }
            catch { }
        }
    }
}
