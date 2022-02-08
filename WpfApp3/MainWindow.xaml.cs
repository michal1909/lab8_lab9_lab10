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
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Xml.Serialization;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Student> ListaStudentow { get; set; }
        public MainWindow()
        {
            ListaStudentow = new List<Student>()
            {
                new Student("Jan", "Kowalski", 1033, "WIMiI"),
                new Student("Michał", "Nowak", 1013, "WIMiI"),
                new Student("Jacek", "Makieta", 1021, "WIMiI")
            };

            InitializeComponent();

            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Imię", Binding = new Binding("imie") });
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Nazwisko", Binding = new Binding("nazwisko") });
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Nr indeksu", Binding = new Binding("NrIndeksu") });
            dgStudent.Columns.Add(new DataGridTextColumn() { Header = "Wydzial", Binding = new Binding("wydzial") });

            dgStudent.AutoGenerateColumns = false;
            dgStudent.ItemsSource = ListaStudentow;
        }

        private void bAddStudent_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new StudentWindow();
            if (dialog.ShowDialog() != true) return;
            ListaStudentow.Add(dialog.student);
            dgStudent.Items.Refresh();
        }

        private void bRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudent.SelectedItem is Student)
                ListaStudentow.Remove((Student)dgStudent.SelectedItem);
            dgStudent.Items.Refresh();
        }

        private void bSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("data.txt",FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (var student in ListaStudentow)
            {
                sw.WriteLine("[[Student]]");
                sw.WriteLine("[FirstName]");
                sw.WriteLine(student.imie);
                sw.WriteLine("[Surname]");
                sw.WriteLine(student.nazwisko);
                sw.WriteLine("[StudentNo]");
                sw.WriteLine(student.NrIndeksu);
                sw.WriteLine("[Faculty]");
                sw.WriteLine(student.wydzial);
            }
            sw.Close();
            MessageBox.Show("Zapisano pomyślnie!");
        }

        private void LoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("data.txt", FileMode.Open);
            ListaStudentow.Clear();
            StreamReader sr = new StreamReader(fs);
            string? line = null;
            while (!sr.EndOfStream)
            {
                var stud = new Student();
                while (line != "[[Student]]" && !sr.EndOfStream)
                    line = sr.ReadLine();   
                line = sr.ReadLine();  
                if (line == "[FirstName]")
                    stud.imie = sr.ReadLine();
                else
                {
                    WrongDataError();
                    return;
                }
                line = sr.ReadLine();   
                if (line == "[Surname]")
                    stud.nazwisko = sr.ReadLine();
                else
                {
                    WrongDataError();
                    return;
                }
                line = sr.ReadLine();   
                if (line == "[StudentNo]")
                    stud.NrIndeksu = Convert.ToInt32(sr.ReadLine());
                else
                {
                    WrongDataError();
                    return;
                }
                line = sr.ReadLine();   
                if (line == "[Faculty]")
                    stud.wydzial = sr.ReadLine();
                else
                {
                    WrongDataError();
                    return;
                }
                ListaStudentow.Add(stud);
            }
            dgStudent.Items.Refresh();
            sr.Close();
            MessageBox.Show("Wczytano pomyślnie!");
        }
        private void WrongDataError()
        {
            MessageBox.Show("Błędny format pliku!");
        }
        private static void Save<T>(T ob, StreamWriter sw)
        {
            Type t = ob.GetType();
            sw.WriteLine($"[[{t.FullName}]]");
            foreach (var propertyInfo in t.GetProperties())
            {
                sw.WriteLine($"[{propertyInfo.Name}]");
                sw.WriteLine(propertyInfo.GetValue(ob));
            }
            sw.WriteLine("[[]]");
        }

        private static T Load<T>(StreamReader sr) where T : new()
        {
            var ob = default(T);
            Type tob = null;
            PropertyInfo property = null;
            while (!sr.EndOfStream)
            {
                string? ln = sr.ReadLine();
                if (ln == "[[]]")
                    return ob;
                if (ln.StartsWith("[["))
                {
                    tob = Type.GetType(ln.Trim('[', ']'));
                    if (typeof(T).IsAssignableFrom(tob))
                        ob = (T)Activator.CreateInstance(tob);
                }
                else if (ln.StartsWith("[") && ob != null)
                    property = tob.GetProperty(ln.Trim('[', ']'));
                else if (ob != null && property != null)
                    property.SetValue(ob, Convert.ChangeType(ln, property.PropertyType));
                else throw new InvalidDataException();
            }

            return default(T);
        }

        private void bSaveToXML_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("data.xml", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            var xs = new XmlSerializer(typeof(List<Student>));
            try
            {
                xs.Serialize(fs, ListaStudentow);
                MessageBox.Show("Zapisano pomyślnie!");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Błędny zapis pliku!");
            }
            fs.Close();
        }

        private void bLoadFromXML_Click(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("data.xml", FileMode.Open);
            var xs = new XmlSerializer(typeof(List<Student>));
            try
            {
                ListaStudentow = (List<Student>)xs.Deserialize(fs);
                dgStudent.ItemsSource = ListaStudentow;
                MessageBox.Show("Odczytano pomyślnie!");
            }
            catch (InvalidOperationException ex)
            {
                WrongDataError();
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
