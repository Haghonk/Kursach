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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MilitaryDataBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection = null;
        public MainWindow()
        {
            InitializeComponent();
            //MainWindow_Load();
        }
        private void MainWindow_Load(object sender, RoutedEventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MilitaryDB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Closed)
            {
                MessageBox.Show("Нету подключения", "Проверка подключения к базе данных");
            }           
        }

        private void Button_Click_Enter(object sender, RoutedEventArgs e)
        {
            EnterDataBase Enter = new EnterDataBase();
            Enter.Show();
            this.Close();
        }
        private void Button_Click_Registration(object sender, RoutedEventArgs e)
        {
            RegistrationDataBase Registration = new RegistrationDataBase();
            Registration.Show();
            this.Close();
        }

        private void Button_Click_Info(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Это приложение все еще в разработке и будет доделаыватся на протяжении всего 3 курса", "Информация о приложении и его функционале");
        }
    }
}
