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

namespace WPFUI
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
        List<Person> persons = new List<Person>();
        private void btn_SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            AddToPersons();
        }
        private void AddToPersons()
        {
            Person person = new Person();
            person.FirstName = txt_firstname.Text;
            person.LastName = txt_LastName.Text;
            persons.Add(person);
            txt_firstname.Text = "";
            txt_LastName.Text = "";
            MessageBox.Show("done");
        }

        private void btn_showList_Click(object sender, RoutedEventArgs e)
        {
            foreach (Person p in persons)
            {
                List_persons.Items.Add(p.FirstName + " " + p.LastName);
            }
        }
    }
}
