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

namespace MilitaryDataBase
{
    /// <summary>
    /// Логика взаимодействия для ArchiveTable.xaml
    /// </summary>
    public partial class ArchiveTable : Window
    {
        private SqlConnection sqlConnection = null;
        public ArchiveTable()
        {
            InitializeComponent();
        }
        private void Button_Click_Main(object sender, RoutedEventArgs e)
        {
            MainMenu Main = new MainMenu();
            Main.Show();
            this.Close();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MilitaryDB"].ConnectionString);

            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [Archive]", sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<Name> Equipment = new List<Name>();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID"]);
                string nname = Convert.ToString(reader["Name"]);
                string amount = Convert.ToString(reader["Amount"]);
                string number = Convert.ToString(reader["SerialNumber"]);
                DateTime date = Convert.ToDateTime(reader["ArrivalDate"]);
                DateTime ddate = Convert.ToDateTime(reader["EndDate"]);
                Equipment.Add(new Name() { ID = id, Название = nname, Количество = amount, Номер = number, ДатаПрибытия = date.ToShortDateString(), ДатаСписания = ddate.ToShortDateString() });
            }
            MilDB.ItemsSource = Equipment;
        }
    }
}
