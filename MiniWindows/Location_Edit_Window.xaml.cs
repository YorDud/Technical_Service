using System;
using System.Collections.Generic;
using System.Data;
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
using ControlzEx.Standard;


namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для Location_Edit_Window.xaml
	/// </summary>
	public partial class Location_Edit_Window : Window
	{
		private MainWindow mainWindow;
		private DataRowView _dataRow;
		public Location_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow;
			_dataRow = dataRow;

			Location.Text = _dataRow["Location"].ToString();
		}

		

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Location_Delete_Click(object sender, RoutedEventArgs e)
		{
			// Получаем ID пользователя из _dataRow
			var id = _dataRow["ID"]; // Предполагается, что ID хранится в столбце "ID"

			// SQL-запрос на удаление
			string query = "DELETE FROM [Technical_Service].[dbo].[Location] WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавляем ID как параметр
						command.Parameters.AddWithValue("@ID", id);

						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();

						// Проверка, было ли что-то удалено
						if (rowsAffected > 0)
						{
							//CustomMessageBoxService.Show($"Пользователь удалён.", "");

							mainWindow.LoadData_Location();
							this.Close();
						}
						else
						{
							CustomMessageBoxService.Show($"Ошибка!", "");
						}
					}
				}


			}
			catch (Exception ex)
			{
				// Обработка исключений
				CustomMessageBoxService.Show($"Ошибка: {ex.Message}", "");
			}
		}

		private void Location_Update_Click(object sender, RoutedEventArgs e)
		{
			var deviceTypeId = _dataRow["ID"]; // Получаем идентификатор из текстбокса (предположительно, отдельное поле для ID)
			var deviceType = Location.Text; // Получаем новое значение типа устройства

			// SQL-запрос на обновление
			string query = "UPDATE [Location] SET [Location] = @Device_Type WHERE [ID] = @Device_Type_Id";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.Add("@Device_Type", SqlDbType.NVarChar).Value = deviceType;
						command.Parameters.Add("@Device_Type_Id", SqlDbType.Int).Value = deviceTypeId;

						// Открытие соединения
						connection.Open();

						// Выполнение команды и проверка обновленного количества строк
						int rowsAffected = command.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							//MessageBox.Show("Тип устройства успешно обновлен.");
						}
						else
						{
							MessageBox.Show("Ошибка: не удалось найти запись с указанным ID.");
						}
					}
				}

				// После обновления данных обновляем таблицу и закрываем окно
				mainWindow.LoadData_Location();
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

	}

}
