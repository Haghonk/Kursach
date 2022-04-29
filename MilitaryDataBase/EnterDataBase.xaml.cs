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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MilitaryDataBase
{
    /// <summary>
    /// Логика взаимодействия для EnterDataBase.xaml
    /// </summary>
    public partial class EnterDataBase : Window
    {
        private SqlConnection sqlConnection;
        public EnterDataBase()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MilitaryDB"].ConnectionString);

            sqlConnection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM [Authorization]", sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    string login = Convert.ToString(reader["Login"]);
                    string password = Convert.ToString(reader["PassWord"]);
                    string Post = Convert.ToString(reader["Post"]);
                    if (login == LoginTextBox.Text.ToString() && password == PasswordTextBox.Text.ToString() && Post == PostComboBox.Text.ToString())
                    {
                        MainMenu Main = new MainMenu();
                        this.Close();
                        Main.Show();
                    }
                    else
                    {
                        //MessageBox.Show("Неправильный логин или пароль", "Ошибка");
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Неправильно введены данные", "Ошибка");
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Полностью заполните поля", "Ошибка");
                }
            }

        }
        private void NameTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            LoginTextBox.Clear();
        }

        private void PasswordTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PasswordTextBox.Clear();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.Show();
            this.Close();
        }
    }
}
