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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private SqlConnection sqlConnection;
        public MainMenu()
        {
            InitializeComponent();
        }
        private void Button_Click_Enter_DataBse(object sender, RoutedEventArgs e)
        {
            DataBase Based = new DataBase();
            Based.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Recording rec = new Recording();
            rec.Show();
            this.Close();
        }
        private void FillArchive()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MilitaryDB"].ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Equipment]", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DateTime a = DateTime.Now;
                DateTime enddate = Convert.ToDateTime(reader["ArrivalDate"]);
                enddate = enddate.AddDays(3650);
                if (DateTime.Compare(enddate, a) < 0)
                {
                    string name = Convert.ToString(reader["Name"]);                 
                    string amount = Convert.ToString(reader["Amount"]);
                    string seralnumber = Convert.ToString(reader["SerialNumber"]);
                    DateTime arrivaldate = Convert.ToDateTime(reader["ArrivalDate"]);
                    FillUserArchive(name, amount, seralnumber, arrivaldate, enddate);
                }
            }
            connection.Close();
        }
        private void Button_Click_Enter_Archive(object sender, RoutedEventArgs e)
        {
            ArchiveTable Based = new ArchiveTable();
            Based.Show();
            this.Close();
        }
        
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FillArchive();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MilitaryDB"].ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Equipment]", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int ID = Convert.ToInt32(reader["Id"]);
                DateTime enddate = Convert.ToDateTime(reader["ArrivalDate"]);
                enddate = enddate.AddDays(3650);
                DateTime a = DateTime.Now;

                if (DateTime.Compare(enddate, a) < 0)
                {
                    DeleteThis(ID);
                }
            }
            connection.Close();
        }
        private void DeleteThis(int ID)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MilitaryDB"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand($"DELETE FROM [Equipment] WHERE  Id={ID};", connection);
            command.ExecuteNonQuery().ToString();
            connection.Close();
        }
        private void FillUserArchive(string name, string amount, string serialnumber, DateTime arrivaldate,DateTime enddate)
        {
            try
            {
                enddate=enddate.AddDays(3650);
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MilitaryDB"].ConnectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [Archive] (Name, Amount, SerialNumber, ArrivalDate, EndDate) VALUES (@Name, @Amount, @SerialNumber, @ArrivalDate, @EndDate)", connection);
                command.Parameters.AddWithValue("Name", name);
                command.Parameters.AddWithValue("Amount", amount);
                command.Parameters.AddWithValue("SerialNumber", serialnumber);
                command.Parameters.AddWithValue("ArrivalDate", arrivaldate);
                command.Parameters.AddWithValue("EndDate", enddate);

                if (command.ExecuteNonQuery().ToString() == "1")
                {
                    MessageBox.Show("Запись в архив проведена успешно");
                }
                else
                {
                    MessageBox.Show("Запись в архив провалена");
                }
                connection.Close();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
