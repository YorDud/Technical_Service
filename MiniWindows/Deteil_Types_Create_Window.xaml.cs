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
	/// Логика взаимодействия для Deteil_Types_Create_Window.xaml
	/// </summary>
	public partial class Deteil_Types_Create_Window : Window
	{
		private MainWindow mainWindow;
		public Deteil_Types_Create_Window(MainWindow mainWindow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow;
		}

		private void Deteil_Types_Dobav_Click(object sender, RoutedEventArgs e)
		{
			var Deteil_TypesV = Deteil_Types.Text;


			// SQL-запрос на добавление
			string query = "INSERT INTO [Deteil_Types] " +
			   "([Deteil_Types]) " +
			   "VALUES (@Deteil_Types)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.AddWithValue("@Deteil_Types", Deteil_TypesV);
						
						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						//CustomMessageBoxService.Show($"Пользователь, {FIOv} добавлен!", "");

						mainWindow.LoadData_Deteil_Types();
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
