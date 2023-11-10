using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace YourDoctor.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeDialog.xaml
    /// </summary>
    public partial class AddEmployeeDialog : Window
    {
        public AddEmployeeDialog()
        {
            InitializeComponent();
            UsernameTextBox = new TextBox();
            UsernameTextBox.Name = "txtUsername";

            PasswordBox = new PasswordBox();
            PasswordBox.Name = "txtPassword";
        }

        public TextBox UsernameTextBox { get; private set; }
        public PasswordBox PasswordBox { get; private set; }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Введите имя пользователя");
                return;
            }

            if (txtPassword.Password.Length == 0)
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
