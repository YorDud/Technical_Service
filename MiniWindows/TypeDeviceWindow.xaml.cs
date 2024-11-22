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
	/// Логика взаимодействия для TypeDeviceWindow.xaml
	/// </summary>
	public partial class TypeDeviceWindow : Window
	{
		private MainWindow mainWindow;
		public TypeDeviceWindow(MainWindow mainWindow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow;
		}

		private void Device_Type_Dobav_Click(object sender, RoutedEventArgs e)
		{
			var Device_Type = Device_Types.Text;


			// SQL-запрос на добавление
			string query = "INSERT INTO [Devices_Types] " +
			   "([Device_Type]) " +
			   "VALUES (@Device_Type)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.AddWithValue("@Device_Type", Device_Type);
						
						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						//CustomMessageBoxService.Show($"Пользователь, {FIOv} добавлен!", "");

						mainWindow.LoadData_Device_Types();
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
