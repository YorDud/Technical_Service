using System;
using System.Collections.Generic;
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


namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для Location_Create_Window.xaml
	/// </summary>
	public partial class Location_Create_Window : Window
	{
		private MainWindow mainWindow;
		public Location_Create_Window(MainWindow mainWindow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow;
		}

		private void Location_Dobav_Click(object sender, RoutedEventArgs e)
		{
			var LocationV = Location.Text;


			// SQL-запрос на добавление
			string query = "INSERT INTO [Location] " +
			   "([Location]) " +
			   "VALUES (@Location)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.AddWithValue("@Location", LocationV);
						
						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						//CustomMessageBoxService.Show($"Пользователь, {FIOv} добавлен!", "");

						mainWindow.LoadData_Location();
						this.Close();

					}
				}


			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		
	}
    
}
