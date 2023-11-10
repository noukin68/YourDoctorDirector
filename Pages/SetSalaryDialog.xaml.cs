using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для SetSalaryDialog.xaml
    /// </summary>
    public partial class SetSalaryDialog : Window
    {
        public SetSalaryDialog()
        {
            InitializeComponent();
            FillUserComboBox();
        }

        private void FillUserComboBox()
        {
            // Очистить предыдущие элементы списка
            cboUsers.Items.Clear();

            // Получение списка имен пользователей из базы данных
            string connectionString = ConfigurationManager.ConnectionStrings["yourDoctor"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT username FROM users";

                using (MySqlCommand cmd = new MySqlCommand(selectQuery, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string username = reader["username"].ToString();
                            cboUsers.Items.Add(username);
                        }
                    }
                }
            }
        }

        private void btnSetSalary_Click(object sender, RoutedEventArgs e)
        {

            // Получение выбранного пользователя из выпадающего списка
            string selectedUsername = cboUsers.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedUsername))
            {
                MessageBox.Show("Пожалуйста, выберите пользователя из списка.");
                return;
            }

            // Получение данных из текстовых полей
            decimal salary;
            if (!decimal.TryParse(txtSalary.Text, out salary))
            {
                MessageBox.Show("Некорректная зарплата.");
                return;
            }

            decimal bonus;
            if (!decimal.TryParse(txtBonus.Text, out bonus))
            {
                MessageBox.Show("Некорректная премия.");
                return;
            }

            // Обновление данных в базе данных
            string connectionString = ConfigurationManager.ConnectionStrings["yourDoctor"].ConnectionString;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Подготовка SQL-запроса для обновления данных пользователя
                string updateQuery = "UPDATE users SET salary = @salary, bonus = @bonus WHERE username = @username";

                using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@salary", salary);
                    cmd.Parameters.AddWithValue("@bonus", bonus);
                    cmd.Parameters.AddWithValue("@username", selectedUsername);

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Зарплата и премия успешно установлены для пользователя {selectedUsername}.");
                        }
                        else
                        {
                            MessageBox.Show($"Не удалось обновить данные пользователя {selectedUsername}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
                    }
                }
            }
        }
    }
}
