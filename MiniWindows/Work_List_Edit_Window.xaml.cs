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
	/// Логика взаимодействия для Work_List_Edit_Window.xaml
	/// </summary>
	public partial class Work_List_Edit_Window : Window
	{
		private MainWindow mainWindow;
		private DataRowView _dataRow;
		public Work_List_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow;
			_dataRow = dataRow;

			Work_List.Text = _dataRow["Work_List"].ToString();
			DeviceType.Text = _dataRow["Device_Type"].ToString();
			NormHour.Text = _dataRow["Norm_Hour"].ToString();

			LoadDevice_Type();
		}

		private void LoadDevice_Type()
		{
			string query = "SELECT Device_Type FROM [Technical_Service].[dbo].[Devices_Types]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> typesTO = new List<string>();
							while (reader.Read())
							{
								typesTO.Add(reader["Device_Type"].ToString());
							}
							DeviceType.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
						}
					}
				}

				// Подписка на событие выбора элемента
				//Device_Type.SelectionChanged += Device_Type_SelectionChanged;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Work_List_Delete_Click(object sender, RoutedEventArgs e)
		{
			// Получаем ID пользователя из _dataRow
			var id = _dataRow["ID"]; // Предполагается, что ID хранится в столбце "ID"

			// SQL-запрос на удаление
			string query = "DELETE FROM [Technical_Service].[dbo].[Work_List] WHERE [ID] = @ID";

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

							mainWindow.LoadData_Work_List();
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

		private void Work_List_Update_Click(object sender, RoutedEventArgs e)
		{
			var Id = _dataRow["ID"]; // Получаем идентификатор из текстбокса (предположительно, отдельное поле для ID)
			var Work_ListV = Work_List.Text; 
			var Device_TypeV = DeviceType.Text;
			var Norm_HourV = NormHour.Text;

			// SQL-запрос на обновление
			string query = "UPDATE [Work_List] \r\n     SET \r\n         [Work_List] = @Work_List, \r\n         [Device_Type] = @Device_Type, \r\n         [Norm_Hour] = @Norm_Hour\r\n     WHERE [ID] = @Id";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.AddWithValue("@Work_List", Work_ListV);
						command.Parameters.AddWithValue("@Device_Type", Device_TypeV);
						command.Parameters.AddWithValue("@Norm_Hour", Norm_HourV);

						command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;


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
				mainWindow.LoadData_Work_List();
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

	}

}
